using System;
using System.Collections.Generic;
using System.Text.Json;

namespace SqlDocStore.Model
{
    public class BlogPostDocument : DocumentBacked<BlogPost>, IEntity
    {
        public int Id { get; set; }
    }
}
