using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace SqlDocStore.Model
{
    public class BlogPostDocument : DocumentBacked<BlogPost>, IEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
