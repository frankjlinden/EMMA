using EDennis.AspNetCore.Base;
using EDennis.AspNetCore.Base.EntityFramework;

using System.Linq;

namespace CT.DDS.EMMA.Models
{
    public class SyncConfigRepo 
        : WriteableTemporalRepo<SyncConfig, EmmaContext, EmmaHistoryContext>
    {
        public SyncConfigRepo(EmmaContext context, EmmaHistoryContext historyContext, ScopeProperties scopeProperties)
            : base(context, historyContext, scopeProperties) {}


        public SyncConfig GetByConfigName(string configName)
        {
            var jobConfig = Context.SyncConfigs.Where(c => c.ConfigName == configName).FirstOrDefault();
            return jobConfig;
        }

       
    }

}
