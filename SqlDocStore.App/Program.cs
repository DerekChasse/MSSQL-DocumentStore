using System;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;

using Bogus;

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

            var blogPosts = blogPostFaker.Generate(100).Select(bp => new BlogPostDocument { Document = bp });

            context.BlogPosts.AddRange(blogPosts);

            context.SaveChanges();

            var count = context.BlogPosts.Count();
            Console.WriteLine($"There are now {count} blogs.");
            
            var randomDocumentId = new Randomizer().Number(1, count);
            Console.WriteLine($"Picking a random blog post : {randomDocumentId}");

            var randomDocument = context.BlogPosts.FirstOrDefault(bp => bp.Id == randomDocumentId);
            Console.WriteLine(JsonSerializer.Serialize(randomDocument, new JsonSerializerOptions { WriteIndented = true }));
        }
    }
}
