using EDennis.AspNetCore.Base.EntityFramework;
using System;

namespace CT.DDS.EMMA.Models
{
    public class Message :  BaseEntity, IHasIntegerId, IEFCoreTemporalModel
    {
        public Message()
        {
        }
        public string To { get; set; }
        public string From { get; set; }
        public string Cc { get; set; }
        public string Bcc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public int AttemptCount { get; set; }
        public int JobConfigId { get; set; }
        public SyncConfig SyncConfig { get; set; }
        public int SendExecutionId { get; set; }
        public string ErrorText { get; set; }
        //Status Enum
        private int _messageStatusId;
        public MessageStatus Status
        {
            get => (MessageStatus)_messageStatusId;
            set => _messageStatusId = (int)value;
        }
    }
}