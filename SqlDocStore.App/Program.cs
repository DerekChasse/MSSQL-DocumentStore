using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;

using Bogus;

using Microsoft.EntityFrameworkCore;

using SqlDocStore.App.Fakers;
using SqlDocStore.Model;

namespace SqlDocStore.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var blogPostFaker = new BlogPostFaker();

            SqlDocContext context = new SqlDocContext();

            IEnumerable<BlogPostDocument> blogPosts = new List<BlogPostDocument>();

            int blogsToGenerate = 2;

            TimeThis($"Generating {blogsToGenerate} Blogs", () =>
            {
                blogPosts = blogPostFaker.Generate(blogsToGenerate).Select(bp => new BlogPostDocument { Document = bp });
            });

            TimeThis("Adding Blogs To Context", () =>
            {
                context.BlogPosts.AddRange(blogPosts);
            });

            TimeThis("Saving Changes", () =>
            {
                context.SaveChanges();
            });

            var count = context.BlogPosts.Count();
            Console.WriteLine($"There are now {count} blogs.");

            IEnumerable<BlogPostDocument> blogPostDocuments = new List<BlogPostDocument>();

            TimeThis($"Fetching {blogsToGenerate} blogs", () =>
            {
                blogPostDocuments = context.BlogPosts.Take(blogsToGenerate);
            });

            Console.WriteLine($"Here are {blogsToGenerate} blog authors and the number of comments");
            Console.WriteLine(JsonSerializer.Serialize(blogPostDocuments.Select(bp => new { Author = bp.Document.Author.Name, Comments = bp.Document.Comments.Count }), new JsonSerializerOptions { WriteIndented = true }));

            string query;

            Console.WriteLine("Enter a search term:");

            query = Console.ReadLine();
            IQueryable<BlogPostDocument> searchResults = null;
            
            TimeThis("Performing Full Text Search", () => 
            {
                searchResults = from bp in context.BlogPosts
                        where EF.Functions.FreeText(nameof(bp.Document), query)
                        select bp;

                //searchResults = context.BlogPosts.Where(bp => EF.Functions.FreeText(nameof(bp.Document), query));
            });

            Console.WriteLine($"Found {searchResults.Count()} blogs with that search term.");
        }

        static void TimeThis(string message, Action action)
        {
            Stopwatch sw = Stopwatch.StartNew();
            action.Invoke();
            Console.WriteLine($"{message} : {sw.Elapsed}");
        }
    }
}
