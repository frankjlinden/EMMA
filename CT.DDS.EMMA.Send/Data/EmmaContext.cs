using Microsoft.EntityFrameworkCore;

using EDennis.AspNetCore.Base.EntityFramework;
using CT.DDS.EMMA.Send.Models;

namespace CT.DDS.EMMA.Send.Data
{
    public class EmmaContextDesignTimeFactory :
        MigrationsExtensionsDbContextDesignTimeFactory<EmmaContext>
    { }


    public class EmmaHistoryContextDesignTimeFactory :
        MigrationsExtensionsDbContextDesignTimeFactory<EmmaHistoryContext>
    { }


    public class EmmaContext : EmmaContextBase
    {
        public EmmaContext(DbContextOptions<EmmaContext> options) : base(options) { }
        // Executions DBSet is real time only
        public DbSet<Execution> Executions { get; set; }
        public DbSet<SmtpConfig> SmtpConfig { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Execution>()
                .ToTable("Execution", "dbo");
            modelBuilder.Entity<Execution>()
                .Property(e => e.Status)
                .HasConversion<int>();

            modelBuilder.Entity<Message>()
                .ToTable("Message", "dbo");
            modelBuilder.Entity<Message>()
                .Property(m => m.Status)
                .HasConversion<int>();

            modelBuilder.Entity<JobConfig>()
              .ToTable("JobConfig", "dbo");


            modelBuilder.Entity<SmtpConfig>()
               .Property(s => s.SecureSocketOptions)
               .HasConversion<int>();
            //if (Database.IsInMemory())
            //{
            //    modelBuilder.Entity<JobConfig>().HasData(EmmaContextDataFactory.JobConfigRecordsFromRetriever);
            //}
        }
    }


    public class EmmaHistoryContext : EmmaContextBase
    {
        public EmmaHistoryContext(DbContextOptions<EmmaHistoryContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {  base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<JobConfig>()
                .ToTable("JobConfig", "dbo_history")
                .HasKey(e=> new {e.Id,e.SysStart });
            modelBuilder.Entity<Message>()
                .ToTable("Message", "dbo_history")
                .HasKey(e => new { e.Id, e.SysStart }); ;
            modelBuilder.IgnoreNavigationProperties();
        }
    }


    public abstract class EmmaContextBase : DbContext
    {
        public EmmaContextBase(DbContextOptions options) : base(options) { }
        // JobConfigs and Messages are RealTime and History
        public DbSet<JobConfig> JobConfigs { get; set; }
        public DbSet<Message> Messages { get; set; }
    }



}
