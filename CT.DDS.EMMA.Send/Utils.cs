
using System;
using System.Collections.Generic;
using System.Text;

namespace CT.DDS.EMMA.Send
{
    public class Utils 
    {
        public static string ResolveTemplate(string template,Dictionary<string,string> valDict){
           
                foreach (KeyValuePair<string,string>pair in valDict)
                {
                template = template.Replace("{" + pair.Key + "}", pair.Value);
                }

                //Is this return even required if c# is by ref. Every method could be void
            return template;
        }

        public static string[] SplitAddresses(string addresses)
        {
            string[] addArray = addresses.Split(";", StringSplitOptions.RemoveEmptyEntries);
            return addArray;
        }
    }
}
