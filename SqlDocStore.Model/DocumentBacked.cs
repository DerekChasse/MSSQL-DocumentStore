using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace SqlDocStore.Model
{
    public abstract class DocumentBacked<T> 
        where T : class, new()
    {
        protected string _document;

        [NotMapped]
        public T Document 
        {
            get => this.Deserialize(this._document);
            set => this._document = this.Serialize(value);
        }

        protected virtual string Serialize(T item)
        {
            return JsonSerializer.Serialize(item, new JsonSerializerOptions { IgnoreNullValues = true });
        }

        protected virtual T Deserialize(string item)
        {
            return JsonSerializer.Deserialize<T>(item, new JsonSerializerOptions { IgnoreNullValues = true });
        }
    }
}
