using System;
using System.Collections.Generic;
using System.Text;

namespace SqlDocStore.Model
{
    public class BlogPost
    {
        public Author Author { get; set; }

        public string Content { get; set; }

        public List<Comment> Comments { get; set; }
    }
}
