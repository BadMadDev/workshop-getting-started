using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebSite.Models;

namespace WebSite.Data
{
    public class ToDoListSqlDbContext : ToDoListDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = "sqldb";
            builder.UserID = "sa";
            builder.Password = "P@ssw0rd123!";
            builder.InitialCatalog = "todo-db";
            optionsBuilder.UseSqlServer(builder.ConnectionString);
        }
    }
}