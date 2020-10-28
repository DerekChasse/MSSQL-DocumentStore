using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SqlDocStore.Model
{
    internal static class DocumentBackedEntityExtensions
    {
        public static void IsDocumentBacked<T,U>(this EntityTypeBuilder<T> builder)
            where T : DocumentBacked<U>
            where U : class, new()
        {
            builder.Property(e => e.Document).HasField(DocumentBacked<U>.FieldName);
        }
    }
}
