using EDennis.AspNetCore.Base;
using EDennis.AspNetCore.Base.EntityFramework;
using CT.DDS.EMMA.Send.Models;
using System.Linq;

namespace CT.DDS.EMMA.Send.Data
{
    public class SmtpConfigRepo 
        : WriteableTemporalRepo<JobConfig, EmmaContext, EmmaHistoryContext>
    {
        public SmtpConfigRepo(EmmaContext context, EmmaHistoryContext historyContext, ScopeProperties scopeProperties)
            : base(context, historyContext, scopeProperties) {}

        public SmtpConfig GetByConfigName(string configName)
        {
            var smtpConfig = Context.SmtpConfig.Where(c => c.ConfigName == configName).FirstOrDefault();
            return smtpConfig;
        }   
    }
}
