
using StudentCurdApp.Enums;
using System.ComponentModel.DataAnnotations;

namespace StudentCurdApp.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty; // Default value to avoid null reference errors

        public UserRole Role { get; set; } = UserRole.User; // Default role is User


        [Required]
        [StringLength(20, MinimumLength =4)]
        [DataType(DataType.Password)] // Ensures the password is treated as sensitive data
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{4,20}$", 
            ErrorMessage = "Password must be 4-20 characters long, contain at least one uppercase letter, one lowercase letter, and one digit.")]
        public string Password { get; set; } = string.Empty;
    }
}
