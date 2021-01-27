using System;
using System.IO;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using WebSite.Models;

namespace WebSite.Data
{
    public class ToDoListDbContext : DbContext
    {
        public DbSet<ToDoListItem> ToDos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "DataSource=ToDoList.db";

            Directory.CreateDirectory(@"/var/data");
            var builder = new SqliteConnectionStringBuilder(connectionString);
            builder.DataSource = Path.Combine(@"/var/data",
                builder.DataSource);
            optionsBuilder.UseSqlite(builder.ToString());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDoListItem>().ToTable("ToDos");
        }
    }
}
