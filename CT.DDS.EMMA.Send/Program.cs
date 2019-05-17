using CT.DDS.Configuration;
using CT.DDS.EMMA.Send.Data;
using CT.DDS.EMMA.Send.Models;
using EDennis.AspNetCore.Base.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CT.DDS.EMMA.Send
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
                    ApplicationName = "CT.DDS.EMMA.Send",
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
                serviceCollection.AddRepos<JobConfigRepo, MessageRepo, ExecutionRepo,SmtpConfigRepo>();
                serviceProvider = serviceCollection.BuildServiceProvider();

                var msgRepo = serviceProvider.GetRequiredService<MessageRepo>();
                var jobRepo = serviceProvider.GetRequiredService<JobConfigRepo>();
                var execRepo = serviceProvider.GetRequiredService<ExecutionRepo>();
                var smtpConfigRepo = serviceProvider.GetRequiredService<SmtpConfigRepo>();

                // Create and configure Messenger object 
                Messenger messenger = new Messenger(Configuration,jobRepo,msgRepo,execRepo,smtpConfigRepo);

                // Execute Job
                Console.WriteLine($"JobName: {Configuration["ExecuteOptions:JobName"]}, Caller:{Configuration["ExecuteOptions:Caller"]}, Start time: {messenger.Execution.StartTime}, SMTP configuration: {Configuration["ExecuteOptions:SmtpConfigName"]}");

                messenger.Execute();

                //// Write execution details text back to caller - add application logging?
                Console.WriteLine($"Job completed at: {messenger.Execution.EndTime}");
                Console.WriteLine($"Job result: {messenger.Execution.ResultText}");
                Console.WriteLine($"Job exit code: {messenger.Execution.Status}");

            Environment.Exit((int)messenger.Execution.Status);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception: {ExecutionStatus.ExecuteError.GetDescription()}");
                Environment.Exit((int)ExecutionStatus.ExecuteError);
            }

        }
    }
}
