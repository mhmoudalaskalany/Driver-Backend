using System;
using System.Diagnostics.CodeAnalysis;

namespace Driver.Domain.Entities.Base
{
    [ExcludeFromCodeCoverage]
    public class BaseEntity<TKey>
    {
        public TKey Id { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedDate { get; set; } = DateTime.UtcNow;
    }
}