using CT.DDS.EMMA.Models;
using CT.DDS.EMMA.Sync.Data;
using CT.DDS.EMMA.Sync.Models;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace CT.DDS.EMMA.Sync
{
    public class Messenger
    {
        //Messenger configures the job, 
        //
        //gets the messages, sends the messages, records results

        public SyncExecution Execution;
        ICollection<DbRow> dbRows;
        IConfiguration _config;
        SyncConfigRepo _syncConfigRepo;
   
        MessageRepo _msgRepo;
        SyncExecutionRepo _execRepo;
        SQLClientAdo _sqlClient;
        string execStatus;

        public Messenger(IConfiguration config, SyncConfigRepo configRepo, MessageRepo msgRepo, SyncExecutionRepo execRepo)
        {
            _config = config;
            _syncConfigRepo = configRepo;
            _msgRepo = msgRepo;
            _execRepo = execRepo;
            
            _sqlClient = new SQLClientAdo();
            
            string configToExecute = _config["ExecuteOptions:JobName"];
            string smtpConfigName = _config["ExecuteOptions:SmtpConfigName"];

            var syncConfig = _syncConfigRepo.GetByConfigName(configToExecute);


            //Create execution object to run job
            Execution = new SyncExecution()
            {
                SysUser = _config["ExecuteOptions:Caller"],
                StartTime = DateTime.Now,
                Config = syncConfig
            };

        }


        public void Execute()
        {
            string statusText;
            try
            {
               
            
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
                
      

        private void ProcessNew()
        {
            var sqlClientAdo = new SQLClientAdo();
            //Rows with name/value pairs for template matching
            dbRows = sqlClientAdo.GetDbRows(Execution.JobConfig);
            foreach (DbRow row in dbRows)
            {
                Message msg = row.GetMessage(Execution.Config.Sender);

                if (msg.Status != MessageStatus.Sent)
                {
                    Execution.NewSendFails++;
                }

                _msgRepo.Create(msg);

            }
        }


        private void CompleteExecution(string resultText) {
                Execution.EndTime = DateTime.Now;
            //update execution
           
            _execRepo.Create(Execution);
            Environment.Exit((int)Execution.Status);

        }

      

    }
}
