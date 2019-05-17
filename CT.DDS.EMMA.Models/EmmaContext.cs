using Microsoft.EntityFrameworkCore;

using EDennis.AspNetCore.Base.EntityFramework;

namespace CT.DDS.EMMA.Models
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
        public DbSet<SyncExecution> SyncExecutions { get; set; }
        public DbSet<SendConfig> SendConfigs { get; set; }
        public DbSet<SyncConfig> SyncConfigs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SyncExecution>()
                .ToTable("SyncExecution", "dbo");
            modelBuilder.Entity<SyncExecution>()
                .Property(e => e.Status)
                .HasConversion<int>();

            modelBuilder.Entity<SendExecution>()
                 .ToTable("SendExecution", "dbo");
            modelBuilder.Entity<SendExecution>()
                .Property(e => e.Status)
                .HasConversion<int>();

            modelBuilder.Entity<Message>()
                .ToTable("Message", "dbo");
            modelBuilder.Entity<Message>()
                .Property(m => m.Status)
                .HasConversion<int>();

            modelBuilder.Entity<SyncConfig>()
              .ToTable("SyncConfig", "dbo");

            modelBuilder.Entity<SyncConfig>()
             .HasIndex(c => c.ConfigName).IsUnique();


            modelBuilder.Entity<SendConfig>()
             .ToTable("SendConfig", "dbo");

            modelBuilder.Entity<SendConfig>()
 .          HasIndex(c => c.ConfigName).IsUnique();

            modelBuilder.Entity<SendConfig>()
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

        public DbSet<Message> Messages { get; set; }
    }



}
