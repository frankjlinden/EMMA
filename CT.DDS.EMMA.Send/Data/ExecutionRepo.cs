using EDennis.AspNetCore.Base;
using EDennis.AspNetCore.Base.EntityFramework;
using CT.DDS.EMMA.Send.Models;

namespace CT.DDS.EMMA.Send.Data
{
    public class ExecutionRepo 
        : WriteableRepo<Execution, EmmaContext>
    {
        public ExecutionRepo(EmmaContext context, ScopeProperties scopeProperties)
            : base(context,  scopeProperties) { }
    }

}
