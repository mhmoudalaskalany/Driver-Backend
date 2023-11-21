using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Driver.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class Driver 
    {
        [Key]
        public int Id { get; set; }

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