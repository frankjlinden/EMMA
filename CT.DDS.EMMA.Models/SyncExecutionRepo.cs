using EDennis.AspNetCore.Base;
using EDennis.AspNetCore.Base.EntityFramework;
using CT.DDS.EMMA.Models;

namespace CT.DDS.EMMA.Models
{
    public class SyncExecutionRepo 
        : WriteableRepo<SyncExecution, EmmaContext>
    {
        public SyncExecutionRepo(EmmaContext context, ScopeProperties scopeProperties)
            : base(context,  scopeProperties) { }
    }

}
