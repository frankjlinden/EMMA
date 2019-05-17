using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace CT.DDS.EMMA.Tests
{
    public class TestHelper
    {
        public static IConfiguration GetTestConfig(string directory,string file) {
            return new ConfigurationBuilder()
                .SetBasePath(directory)
                .AddJsonFile(file, optional: true)
                .Build();
        }
        

    }
}
