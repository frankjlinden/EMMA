using CT.DDS.EMMA.Models;
using EDennis.AspNetCore.Base.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CT.DDS.EMMA.Models
{
    public class SyncExecution :BaseEntity,IHasIntegerId, IEFCoreTemporalModel,IHasSysUser
    {
        private int _syncExecuteStatus_Id;
        public int Id { get; set; }
        public SyncConfig Config { get; set; }
        public int SyncConfigId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public int RecordsTransferred { get; set; }
        public string StatusText { get; set; }
        public  SyncExecStatus Status{
            get => (SyncExecStatus)_syncExecuteStatus_Id;
            set => _syncExecuteStatus_Id = (int)value;
        }
        public string SysUser { get;set ; }
    }
}
