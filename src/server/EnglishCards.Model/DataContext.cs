using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using EnglishCards.Model.Data;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EnglishCards.Model
{
    public class DataContext : DbContext
    {
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupSet> GroupSets { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<ProgressData> ProgressDatas { get; set; }
        public DbSet<ProgressDataWord> ProgressDataWords { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Word> Words { get; set; }
        public DbSet<WordInGroupSet> WordInGroupSets { get; set; }
        public DbSet<WordTranslation> WordTranslations { get; set; }
        public DbSet<SysSetting> SysSettings { get; set; }
        public DbSet<UserInGroup> UserInGroups { get; set; }

        private string _connectionString;

        public readonly ILoggerFactory loggerFactory;

        public DataContext(string connectionString, Action<DataContext> onCreateAction = null)
        {
             loggerFactory = LoggerFactory.Create(builder => {
                 builder.AddFilter("Microsoft", LogLevel.Information)
                        .AddFilter("System", LogLevel.Warning)
                        .AddFilter("SampleApp.Program", LogLevel.Debug)
                        .AddConsole();
                        });
            _connectionString = connectionString;

            onCreateAction?.Invoke(this);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLoggerFactory(loggerFactory)
                .EnableSensitiveDataLogging()
                .UseMySQL(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.SetTableName(entityType.DisplayName());
                foreach (var property in entityType.GetProperties())
                {
                    if (property.Name == "CreatedOn")
                    {
                        property.SetDefaultValueSql("CURRENT_TIMESTAMP");
                    }
                }
            }

            modelBuilder.Entity<UserInGroup>()
                .HasKey(t => new { t.UserId, t.GroupId });

            modelBuilder.Entity<UserInGroup>()
                .HasOne(sc => sc.User)
                .WithMany(s => s.UserInGroup)
                .HasForeignKey(sc => sc.UserId);

            modelBuilder.Entity<UserInGroup>()
                .HasOne(sc => sc.Group)
                .WithMany(c => c.UserInGroup)
                .HasForeignKey(sc => sc.GroupId);

            modelBuilder.Entity<WordInGroupSet>()
                .HasKey(t => new { t.WordId, t.GroupSetId });

            modelBuilder.Entity<WordInGroupSet>()
                .HasOne(sc => sc.Word)
                .WithMany(s => s.WordInGroupSets)
                .HasForeignKey(sc => sc.WordId);

            modelBuilder.Entity<WordInGroupSet>()
                .HasOne(sc => sc.GroupSet)
                .WithMany(c => c.WordInGroupSets)
                .HasForeignKey(sc => sc.GroupSetId);
        }
    }
}
