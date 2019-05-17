using EDennis.AspNetCore.Base;
using EDennis.AspNetCore.Base.EntityFramework;
using CT.DDS.EMMA.Models;

namespace CT.DDS.EMMA.Send.Data
{
    public class SendExecutionRepo 
        : WriteableRepo<SendExecution, EmmaContext>
    {
        public SendExecutionRepo(EmmaContext context, ScopeProperties scopeProperties)
            : base(context,  scopeProperties) { }
    }

}
