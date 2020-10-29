using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlDocStore.Model
{
    public class SqlDocContext : DbContext
    {
        public SqlDocContext()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogPostDocument>().IsDocumentBacked<BlogPostDocument, BlogPost>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=localhost;Initial Catalog=SqlDocStore;Integrated Security=True");
        }

        public DbSet<BlogPostDocument> BlogPosts { get; set; }
    }
}
