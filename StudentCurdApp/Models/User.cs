﻿using StudentCurdApp.Enums;

namespace StudentCurdApp.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public UserRole Role { get; set; } = UserRole.User;
    }
}
