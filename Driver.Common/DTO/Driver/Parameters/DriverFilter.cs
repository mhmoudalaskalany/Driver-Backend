using System.Diagnostics.CodeAnalysis;

namespace Driver.Common.DTO.Driver.Parameters
{
    [ExcludeFromCodeCoverage]
    public class DriverFilter
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
