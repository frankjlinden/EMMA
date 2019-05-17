using System;
using System.Collections.Generic;
using System.Text;

namespace CT.DDS.EMMA.Models
{ /// <summary>
        /// A notification model is used querying when querying results from the SQL view.
        /// Notifications will be transformed into messages by the Messenger during the creation of an Execution
        /// </summary>
    public class DbRow
    {
        public Dictionary<string,string> ValuesDictionary { get; set; }

        public DbRow()
        {
            ValuesDictionary = new Dictionary<string,string>();
        }
    }
}
