using EDennis.AspNetCore.Base;
using EDennis.AspNetCore.Base.EntityFramework;
using CT.DDS.EMMA.Send.Models;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System;

namespace CT.DDS.EMMA.Send.Data
{
    public class MessageRepo 
        : WriteableTemporalRepo<Message, EmmaContext, EmmaHistoryContext>
    {
        public MessageRepo(EmmaContext context, EmmaHistoryContext historyContext, ScopeProperties scopeProperties)
            : base(context, historyContext, scopeProperties) { }

        public ICollection<Message> GetResends(JobConfig jobConfig)
        {
            //return messages where...
            IEnumerable<Message> msgs = Context.Messages;

            // messages for this job config only
            msgs = msgs.Where(m => m.JobConfigId == jobConfig.Id);
            // status is not "sent"
            msgs = msgs.Where(m => m.Status != MessageStatus.Sent);
            // retires have not been consumed
            msgs = msgs.Where(m => m.AttemptCount <= jobConfig.MessageResendLimit).OrderByDescending(m=>m.AttemptCount);
            //order by attempt count descending so oldest messages go first.

            return msgs.ToList();
        }

        

    }
}
