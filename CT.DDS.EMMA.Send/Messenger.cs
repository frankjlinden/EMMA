using CT.DDS.EMMA.Send.Data;
using CT.DDS.EMMA.Send.Models;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace CT.DDS.EMMA.Send
{
    public class Messenger
    {
        //Messenger configures the job, 
        //
        //gets the messages, sends the messages, records results

        public Execution Execution;
        ICollection<DbRow> dbRows;
        IConfiguration _config;
        JobConfigRepo _jobRepo;
        SmtpConfigRepo _smtpConfigRepo;
        MessageRepo _msgRepo;
        ExecutionRepo _execRepo;
        SQLClientAdo _sqlClient;
        EmailSender _emailSender;
        string execStatus;

        public Messenger(IConfiguration config, JobConfigRepo jobRepo, MessageRepo msgRepo, ExecutionRepo execRepo, SmtpConfigRepo smtpConfigRepo)
        {
            _config = config;
            _jobRepo = jobRepo;
            _msgRepo = msgRepo;
            _execRepo = execRepo;
            _smtpConfigRepo = smtpConfigRepo;
            
            _sqlClient = new SQLClientAdo();
            
            string configToExecute = _config["ExecuteOptions:JobName"];
            string smtpConfigName = _config["ExecuteOptions:SmtpConfigName"];

            var jobConfig = _jobRepo.GetByJobName(configToExecute);
            var smtpConfig = _smtpConfigRepo.GetByConfigName(smtpConfigName);

            //Create execution object to run job
            Execution = new Execution()
            {
                SysUser = _config["ExecuteOptions:Caller"],
                StartTime = DateTime.Now,
                JobConfig = jobConfig,
                SmtpConfig = smtpConfig
            };

            _emailSender = new EmailSender(Execution.SmtpConfig);
        }


        public void Execute()
        {
            string statusText;
            try
            {
                //Verify that 
                // Connect to SMTP Server
                statusText = _emailSender.Connect();

                if (statusText != "Connected")
                {
                    //SMTP Connect Failure
                    Execution.Status = ExecutionStatus.ConnectFail;
                    CompleteExecution(statusText);
                }

                //Authenticate to SMTP if configured to do so
                if (Execution.SmtpConfig.UseAuthentication)
                {
                    if (_emailSender.Authenticate(Execution.SmtpConfig.UserName, Execution.SmtpConfig.Password) != "Authenticated")
                    {
                        //SMTP Authentication Failure
                        Execution.Status = ExecutionStatus.AuthFail;
                        CompleteExecution(statusText);
                    }
                }

                // If no errors have occured, process messages
                ProcessResends();
                ProcessNew();
                
                if ((Execution.NewSendAttempts > 0) || (Execution.ResendAttempts > 0))
                {
                    if (Execution.NewSendFails > 0 || Execution.NewSendFails>0)
                    {
                        Execution.Status = ExecutionStatus.CompletedWithErrors;

                    }
                    else
                    {
                        Execution.Status = ExecutionStatus.Success;
                    }
                }
                else
                {
                    Execution.Status = ExecutionStatus.NoResult;
                    statusText = "No messages sent or resent";
                }
            }


            catch (Exception e)
            {
                Execution.Status = ExecutionStatus.ExecuteError;
                statusText = e.Message;
             }


        }
                
        private void ProcessResends()
        {
            var resends = _msgRepo.GetResends(Execution.JobConfig);

            foreach (var msg in resends)
            {
                msg.ExecutionId = Execution.Id;
                Execution.ResendAttempts++;
                msg.SysStart = DateTime.Now;

                SendMail(msg);
                if (msg.Status != MessageStatus.Sent)
                {
                    Execution.ResendFails++;
                }

                _msgRepo.Update(msg, msg.Id);
            }   
        }


        private void ProcessNew()
        {
            var sqlClientAdo = new SQLClientAdo();
            //Rows with name/value pairs for template matching
            dbRows = sqlClientAdo.GetDbRows(Execution.JobConfig);
            foreach (DbRow row in dbRows)
            {
                Message msg = row.GetMessage(Execution.Sender);
                msg.ExecutionId = Execution.Id;
                Execution.NewSendAttempts++;
                SendMail(msg);

                if (msg.Status != MessageStatus.Sent)
                {
                    Execution.NewSendFails++;
                }

                _msgRepo.Create(msg);

            }
        }


        private void SendMail(Message msg)
        {
            msg.AttemptCount++;
            msg.SysUser = Execution.SysUser;
            _emailSender.Send(msg);
            msg.SysStart = DateTime.Now;
        }


        private void CompleteExecution(string resultText) {
                Execution.EndTime = DateTime.Now;
            //update execution
           
            _execRepo.Create(Execution);
            Environment.Exit((int)Execution.Status);

        }
            


    }
}
