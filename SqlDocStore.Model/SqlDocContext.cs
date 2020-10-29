using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlDocStore.Model
{
    public class SqlDocContext : DbContext
    {
        public static readonly ILoggerFactory loggerFactory = 
            LoggerFactory.Create(builder => 
            {
                builder.SetMinimumLevel(LogLevel.Trace);
                builder.AddConsole(); 
            });

        public SqlDocContext()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogPostDocument>().IsDocumentBacked<BlogPostDocument, BlogPost>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(loggerFactory);
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseSqlServer(@"Data Source=localhost;Initial Catalog=SqlDocStore;Integrated Security=True");
        }

        public DbSet<BlogPostDocument> BlogPosts { get; set; }
    }
}
