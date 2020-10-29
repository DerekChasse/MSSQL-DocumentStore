using Bogus;
using Bogus.Extensions;

using SqlDocStore.App.Fakers;
using SqlDocStore.Model;

using System;
using System.Collections.Generic;
using System.Text;

namespace SqlDocStore.App.Fakers
{
    internal class BlogPostFaker : Faker<BlogPost>
    {
        public BlogPostFaker()
        {
            AuthorFaker authorFaker = new AuthorFaker();
            CommentFaker commentFaker = new CommentFaker();

            RuleFor(bp => bp.Author, f => authorFaker.Generate());
            RuleFor(bp => bp.Comments, f => commentFaker.GenerateBetween(15, 20));
            RuleFor(bp => bp.Content, f => f.Lorem.Paragraphs(4, 8));
        }
    }


}
