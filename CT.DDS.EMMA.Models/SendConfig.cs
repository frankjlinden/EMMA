using EDennis.AspNetCore.Base.EntityFramework;
using MailKit.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace CT.DDS.EMMA.Models
{
    public class SendConfig: BaseEntity,IEFCoreTemporalModel
    {
        public int Id { get; set; }
        public string ConfigName { get; set; }
        public string Host { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public bool UseAuthentication { get; set; }
        public SecureSocketOptions SecureSocketOptions { get; set; }
    }
}
