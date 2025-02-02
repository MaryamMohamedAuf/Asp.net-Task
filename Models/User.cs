using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Task.Models
{
   [Index(nameof(Email), IsUnique = true)]

    public class User
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50, MinimumLength = 3)]

        public required string FullName { get; set; }
        [EmailAddress]
        public  string Email { get; set; }
        [StringLength(15, MinimumLength = 10)]
        [Phone]
        public string PhoneNumber { get; set; }
        [Range(18, 99)]

        public int Age { get; set; }

    }
}
