using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using EnglishCards.Model.Data;
using Microsoft.EntityFrameworkCore.Metadata;

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
        public DbSet<WordTranslation> WordTranslations { get; set; }
        public DbSet<SysSetting> SysSettings { get; set; }
        public DbSet<UserInGroup> UserInGroups { get; set; }

        public DataContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(Environment.GetEnvironmentVariable("ECDB_CONNECTION"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.DisplayName());
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
