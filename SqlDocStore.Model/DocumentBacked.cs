using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace SqlDocStore.Model
{
    public abstract class DocumentBacked<T> 
        where T : class, new()
    {
        public T Document { get; set; }
    }
}
