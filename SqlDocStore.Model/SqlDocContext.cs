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

            /*
             * I'm trying to make IsDocumentBacked only callable when configuring entities
             * which ARE document backed, to prevent accidentally calling this on a "normal" entity.
             * 
             * I'm also trying to be clever to avoid magic strings. Don't judge, or do, whatever.
             */

            // This is jank
            modelBuilder.Entity<BlogPostDocument>().IsDocumentBacked<BlogPostDocument,BlogPost>();

            // This is what I want
            //modelBuilder.Entity<BlogPostDocument>().IsDocumentBacked();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=SqlDocStore;Trusted_Connection=True;");
        }

        DbSet<BlogPostDocument> BlogPosts { get; set; }
    }
}
