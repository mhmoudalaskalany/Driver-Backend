using System.Diagnostics.CodeAnalysis;

namespace Driver.Common.DTO.Driver
{
    [ExcludeFromCodeCoverage]
    public class DriverDto 
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
