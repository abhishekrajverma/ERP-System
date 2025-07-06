using System.ComponentModel.DataAnnotations;

namespace StudentCrudApp.Models
{
    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty; // Initialize with a default value to avoid CS8618
        public string Email { get; set; } = string.Empty;
        public string Course { get; set; } = string.Empty; // Initialize with a default value to avoid CS8618 
    }
}
