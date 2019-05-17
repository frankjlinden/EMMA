
using CT.DDS.EMMA.Send.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace CT.DDS.EMMA.Send.Data
{

    public interface ISQLClientAdo
        {
             Collection<DbRow> GetDbRows(JobConfig jobConfig);

        }

    public class SQLClientAdo : ISQLClientAdo
    {
        public Collection<DbRow> GetDbRows(JobConfig jobConfig ) 
        {
            Collection<DbRow> colRows = new Collection<DbRow>();
            try
            {
                using (SqlConnection con = new SqlConnection(jobConfig.ConnectionString))
                {
                    string sqlQuery = $"SELECT * from {jobConfig.ViewName} ";
                    SqlCommand cmd = new SqlCommand(sqlQuery, con);

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();     
                        
                        while (rdr.Read())
                        {
                            var row = new DbRow();
                                for (int i=0;i<rdr.FieldCount; i++)
                                {
                            string key =  rdr.GetName(i).ToString();
                            string val = rdr[i].ToString();

                            var colVal = new KeyValuePair<string, string>(key,val);    
                              
                            row.ValuesDictionary.Add(key,val);
                                }
                            colRows.Add(row);
                        }
                    
                    con.Close();
                }
            }
            catch (Exception e)
            {
                //TODO: Improve Error handling
                throw (e);
            }

            return colRows;
        }


    }
}
