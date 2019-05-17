using System;
using System.Collections.Generic;
using System.Text;
using CT.DDS.EMMA.Send.Models;

namespace CT.DDS.EMMA.Send.Data
{
    public class DbRowRepo 
    {
        SQLClientAdo _sqlClient;
        JobConfig _jobConfig;

        public DbRowRepo(SQLClientAdo sqlClient, JobConfig jobConfig)
        {
            _sqlClient = sqlClient;
            _jobConfig = jobConfig;
        }
        public IEnumerable<DbRow> GetDbRows()
        {
            return _sqlClient.GetDbRows(_jobConfig);
        }
        public void Dispose()
        {
            
        }
    }
}