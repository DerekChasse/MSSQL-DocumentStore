using System;
using System.Collections.Generic;
using System.Text.Json;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SqlDocStore.Model
{
    internal static class DocumentBackedEntityExtensions
    {
        public static void IsDocumentBacked<T,U>(this EntityTypeBuilder<T> builder)
            where T : DocumentBacked<U>
            where U : class, new()
            {
            builder
                .Property(x => x.Document)
                .HasConversion(new ValueConverter<U, string>(
                    item => JsonSerializer.Serialize(item, new JsonSerializerOptions { IgnoreNullValues = true }),
                    str => JsonSerializer.Deserialize<U>(str, new JsonSerializerOptions { IgnoreNullValues = true })
                    
                ))
                .UsePropertyAccessMode(PropertyAccessMode.PreferFieldDuringConstruction);
        }
    }
}
