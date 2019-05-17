using EDennis.AspNetCore.Base;
using EDennis.AspNetCore.Base.EntityFramework;
using CT.DDS.EMMA.Send.Models;
using System.Linq;

namespace CT.DDS.EMMA.Send.Data
{
    public class JobConfigRepo 
        : WriteableTemporalRepo<JobConfig, EmmaContext, EmmaHistoryContext>
    {
        public JobConfigRepo(EmmaContext context, EmmaHistoryContext historyContext, ScopeProperties scopeProperties)
            : base(context, historyContext, scopeProperties) {}


        public JobConfig GetByJobName(string jobName)
        {
            var jobConfig = Context.JobConfigs.Where(c => c.JobName == jobName).FirstOrDefault();
            return jobConfig;
        }

       
    }

}
