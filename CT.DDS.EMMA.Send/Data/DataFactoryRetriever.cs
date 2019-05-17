using CT.DDS.EMMA.Send.Models;
using EDennis.AspNetCore.Base.Testing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CT.DDS.EMMA.Send.Data
{
    public static partial class EmmaContextDataFactory
    {
        public static JobConfig[] JobConfigRecordsFromRetriever { get; set; }
            = DataRetriever.Retrieve<JobConfig>("Emma", "JobConfig");

        //public static JobConfig[] JobConfigHistoryRecordsFromRetriever { get; set; }
        //    = DataRetriever.Retrieve<JobConfig>("Emma", "dbo_history.JobConfig");

        //public static Message[] MessageRecordsFromRetriever { get; set; }
        //   = DataRetriever.Retrieve<Message>("Emma", "Message");

        //public static Message[] MessageHistoryRecordsFromRetriever { get; set; }
        //    = DataRetriever.Retrieve<Message>("Emma", "dbo_history.Message");
    }
}
