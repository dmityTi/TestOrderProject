using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Data.Context
{
    public class DesignTimeDbContextFactory<DBContext> : IDesignTimeDbContextFactory<DBContext>
        where DBContext : DbContext
    {
        private const string ConnectionString = "server=localhost;Database=TestOrderDB;Trusted_Connection=True;";
       
        public DBContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DBContext>();
            builder.UseSqlServer(ConnectionString);
            var context = (DBContext)Activator.CreateInstance(typeof(DBContext), builder.Options);
            return context;
        }
    }

}