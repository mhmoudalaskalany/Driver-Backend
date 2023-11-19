using System;
using System.Diagnostics.CodeAnalysis;
using Driver.Domain.Entities.Base;

namespace Driver.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class Driver : BaseEntity<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}