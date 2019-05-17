using EDennis.AspNetCore.Base;
using EDennis.AspNetCore.Base.EntityFramework;
using CT.DDS.EMMA.Models;
using System.Linq;
using CT.DDS.EMMA.Models;

namespace CT.DDS.EMMA.Models
{
    public class SendConfigRepo 
        : WriteableTemporalRepo<SendConfig, EmmaContext, EmmaHistoryContext>
    {
        public SendConfigRepo(EmmaContext context, EmmaHistoryContext historyContext, ScopeProperties scopeProperties)
            : base(context, historyContext, scopeProperties) {}

        public SendConfig GetByConfigName(string configName)
        {
            var smtpConfig = Context.SendConfigs.Where(c => c.ConfigName == configName).FirstOrDefault();
            return smtpConfig;
        }   
    }
}
