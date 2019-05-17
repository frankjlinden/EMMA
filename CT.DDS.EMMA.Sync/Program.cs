using CT.DDS.Configuration;
using CT.DDS.EMMA.Sync.Data;
using CT.DDS.EMMA.Models;
using EDennis.AspNetCore.Base.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CT.DDS.EMMA.Sync
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration Configuration;
            IHostingEnvironment _env;
           
            try       
            {
                var serviceCollection = new ServiceCollection();
                
                //make hosting Environment, this is required by DDSConfigurationBuilder
                var hostingEnv = new HostingEnvironment
                {
                    ApplicationName = "CT.DDS.EMMA.Sync",
                    ContentRootPath = Environment.CurrentDirectory,
                    EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
                };
                serviceCollection.AddSingleton<IHostingEnvironment>(hostingEnv); 

                var serviceProvider = serviceCollection.BuildServiceProvider();
                _env = serviceProvider.GetRequiredService<IHostingEnvironment>();

                //Get DDSConfiguration
                Configuration = serviceCollection.GetDDSConfiguration(_env);

                //Get Repos
                serviceCollection.AddDbContexts<EmmaContext, EmmaHistoryContext>(Configuration, _env);
                serviceProvider = serviceCollection.BuildServiceProvider();
                serviceCollection.AddRepos<SyncConfigRepo, MessageRepo, SyncExecutionRepo>();
                serviceProvider = serviceCollection.BuildServiceProvider();

                var msgRepo = serviceProvider.GetRequiredService<MessageRepo>();
                var syncConfigRepo = serviceProvider.GetRequiredService<SyncConfigRepo>();
                var execRepo = serviceProvider.GetRequiredService<SyncExecutionRepo>();
         

                // Create and configure Messenger object 
                Messenger messenger = new Messenger(Configuration,syncConfigRepo,msgRepo,execRepo);

                // Execute Job
                Console.WriteLine($"JobName: {Configuration["ExecuteOptions:JobName"]}, Caller:{Configuration["ExecuteOptions:Caller"]}, Start time: {messenger.Execution.StartTime}, SMTP configuration: {Configuration["ExecuteOptions:SmtpConfigName"]}");

                messenger.Execute();

                //// Write execution details text back to caller - add application logging?
                Console.WriteLine($"Job completed at: {messenger.Execution.EndTime}");
                Console.WriteLine($"Job result: {messenger.Execution.StatusText}");
                Console.WriteLine($"Job exit code: {messenger.Execution.Status}");

            Environment.Exit((int)messenger.Execution.Status);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception: {SyncExecStatus.ExecuteError.GetDescription()}");
                Environment.Exit((int)SyncExecStatus.ExecuteError);
            }

        }
    }
}
