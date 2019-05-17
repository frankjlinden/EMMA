using EDennis.AspNetCore.Base;
using EDennis.AspNetCore.Base.EntityFramework;
using System.Linq;
using System.Collections.Generic;

namespace CT.DDS.EMMA.Models
{
    public class MessageRepo 
        : WriteableTemporalRepo<Message, EmmaContext, EmmaHistoryContext>
    {
        public MessageRepo(EmmaContext context, EmmaHistoryContext historyContext, ScopeProperties scopeProperties)
            : base(context, historyContext, scopeProperties) { }

        public ICollection<Message> GetR(SyncConfig syncConfig)
        {
            //return messages where...
            IEnumerable<Message> msgs = Context.Messages;
            // Record Date > period in days



            // messages for this job config only
            msgs = msgs.Where(m => m.JobConfigId == syncConfig.Id);
            // status is not "sent"
            msgs = msgs.Where(m => m.Status != MessageStatus.Sent);
            // retires have not been consumed
            msgs = msgs.Where(m => m.AttemptCount <= syncConfig.MessageResendLimit).OrderByDescending(m=>m.AttemptCount);
            //order by attempt count descending so oldest messages go first.

            return msgs.ToList();
        }

        

    }
}
