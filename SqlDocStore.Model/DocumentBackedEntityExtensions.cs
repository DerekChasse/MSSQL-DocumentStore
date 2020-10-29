using System;
using System.Collections.Generic;
using System.Text.Json;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SqlDocStore.Model
{
    internal static class DocumentBackedEntityExtensions
    {
        public static void IsDocumentBacked<T, U>(this EntityTypeBuilder<T> builder)
            where T : DocumentBacked<U>
            where U : class, new()
        {
            ValueConverter<U, string> converter = new ValueConverter<U, string>
            (
                v => JsonSerializer.Serialize(v, null),
                v => JsonSerializer.Deserialize<U>(v, null) ?? new U()
            );

            ValueComparer<U> comparer = new ValueComparer<U>
            (
                (l, r) => JsonSerializer.Serialize(l, null) == JsonSerializer.Serialize(r, null),
                v => v == null ? 0 : JsonSerializer.Serialize(v, null).GetHashCode(),
                v => JsonSerializer.Deserialize<U>(JsonSerializer.Serialize(v, null), null)
            );

            var propertyBuilder = builder.Property(db => db.Document);

            propertyBuilder.HasConversion(converter);
            propertyBuilder.Metadata.SetValueConverter(converter);
            propertyBuilder.Metadata.SetValueComparer(comparer);
            propertyBuilder.HasColumnType("nvarchar(max)");
        }
    }
}
