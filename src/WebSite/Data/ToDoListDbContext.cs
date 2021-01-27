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

        public ToDoListDbContext(DbContextOptions<ToDoListDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDoListItem>().ToTable("ToDos");
        }
    }
}
