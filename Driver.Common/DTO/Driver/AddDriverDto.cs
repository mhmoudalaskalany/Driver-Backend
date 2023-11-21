using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Driver.Common.DTO.Driver
{
    [ExcludeFromCodeCoverage]
    public class AddDriverDto
    {
        [Required]
        [MaxLength(250)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(250)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(10)]
        public string PhoneNumber { get; set; }
    }
}
