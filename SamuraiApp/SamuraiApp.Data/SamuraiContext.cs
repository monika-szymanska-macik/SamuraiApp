using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SamuraiApp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SamuraiApp.Data
{
    public class SamuraiContext : DbContext
    {
        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Clan> Clans { get; set; }

        public static readonly ILoggerFactory ConsolLoggerFactory = LoggerFactory.Create(
            builder =>
            {
                builder
                .AddFilter((category, level) =>
                category == DbLoggerCategory.Database.Command.Name
                && level == LogLevel.Information)
                .AddConsole();
            });

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLoggerFactory(ConsolLoggerFactory).EnableSensitiveDataLogging()
                .UseSqlServer(
                "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = SamuraiAppData");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SamuraiBattle>().HasKey(s => new { s.SamuraiId, s.BattleId });
            modelBuilder.Entity<Horse>().ToTable("Horses");
        }
    }
}
