using CT.DDS.EMMA.Con.Data;
using CT.DDS.EMMA.Con.Models;
using Xunit;


namespace CT.DDS.EMMA.Con.Tests
{
    public class SQLClientTests
    {
        [Fact]
        public void GetDbRows()
        {
            var sqlClientDAL = new SQLClientAdo();

            JobConfig cfg = new JobConfig()
            {
                ConnectionString = "Server=(localdb)\\MSSQLLocalDB;Database=Camris;Trusted_Connection=True;MultipleActiveResultSets=true",
                 JobName = "MedicaidCoverageChange",
                SenderAddress = "frankjlinden@gmail.com",
                ViewName = "vwMedicaidCoverageChange",
                SenderName = "Frank Linden"
            };
            //var actual = sqlClientDAL.GetDbRows(
            //    cfg);



        //    var expected = new List<DbViewRow>().FromJsonPath("SQLClientDAL\\GetMessages\\expected1.json").ToList();

        //    Assert.True(actual.IsEqual(expected));


        }
    }
}
