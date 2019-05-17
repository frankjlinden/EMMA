using EDennis.AspNetCore.Base.EntityFramework;
using System;
using System.Collections.ObjectModel;

namespace CT.DDS.EMMA.Models
{
    public class SendExecution :IHasSysUser
    {
        private int _syncExecuteStatus_Id;
        public int Id { get; set; }
        public SendConfig Config;
        public int SendConfigId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public String SysUser { get; set; }
        public Collection<Message> MessageQueue { get; set; }
        public int NewSendAttempts { get; set; }
        public int NewSendFails { get; set; }
        public int ResendAttempts { get; set; }
        public int ResendFails { get; set; }
        public SyncExecStatus Status
        {
            get => (SyncExecStatus)_syncExecuteStatus_Id;
            set => _syncExecuteStatus_Id = (int)value;
        }

    }

}
