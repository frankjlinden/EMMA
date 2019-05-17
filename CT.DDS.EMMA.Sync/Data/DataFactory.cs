using CT.DDS.EMMA.Sync.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CT.DDS.EMMA.Sync.Data
{
    public static partial class EmmaContextDataFactory
    {
        public static JobConfig[] dbo_JobConfigRecords { get; set; }
        = new JobConfig[]{
            new JobConfig{
                Id = 1,
                SysStart = new DateTime(2018,1,1,0,0,0),
                SysEnd = new DateTime(9999,12,31,23,59,59),
                JobName = "MedicaidCoverageChange",
                ConnectionString = "Server=(localdb)\\MSSQLLocalDB;Database=Camris;Trusted_Connection=True;MultipleActiveResultSets=true",
                ViewName = "vwMedicaidCoverageChange",
                SenderAddress = "frankjlindenct@gmail.com",
                SenderName = "Frank's test account",
                BodyTemplate = "The following message in being sent in regard to Individual: {ddsnum}\n {Message}",
                SubjectTemplate = "{Subject}",
                DataRateDays = 1,
                MessageResendLimit = 2
        },
            new JobConfig
            {
                Id = 2,
                SysStart = new DateTime(2018,1,1,0,0,0),
                SysEnd = new DateTime(9999,12,31,23,59,59),
                JobName = "SecurityMentorTrainingDue",
                ConnectionString = "Server=(localdb)\\MSSQLLocalDB;Database=Camris;Trusted_Connection=True;MultipleActiveResultSets=true",
                ViewName = "vwSecurityMentorDue",
                SenderAddress = "frankjlindenct@gmail.com",
                SenderName = "Frank's test account",
                BodyTemplate = "{EmployeeName} has not completed the required training: {trainingCourse} by Due Date: {DueDate}",
                SubjectTemplate = "{Subject}",
                DataRateDays = 1,
                MessageResendLimit = 2
            }
        };

        public static JobConfig[] dbo_history_JobConfigRecords { get; set; }
         = new JobConfig[]
         {
             new JobConfig
             {

             }
         };

        public static Message[] dbo_MesssageRecords { get; set; }
        = new Message[]
        {
            new Message()
            {
                To = "frankjlinden@yahoo.com",
                From = "frankjlindenct@gmail.com",
                JobConfigId = 1,
                Status = MessageStatus.NewUnsent,
                Subject = "New Email Test",
                Body = "This is a test. This is only a test."
            },
            new Message()
            {
                To = "frankjlinden@yahoo.com",
                From = "frankjlindenct@gmail.com",
                JobConfigId = 1,
                Status = MessageStatus.NewUnsent,
                Subject = "New Email Test",
                Body = "This is a test. This is only a test."
            },
            new Message()
            {
                To = "frankjlinden@yahoo.com",
                From = "frankjlindenct@gmail.com",
                JobConfigId = 1,
                Status = MessageStatus.NewUnsent,
                Subject = "New Email Test",
                Body = "This is a test. This is only a test."
            }

        };

        public static Message[] dbo_history_MessageRecords { get; set; } =
            new Message[] {

            };
    }
}
