using StudentCurdApp.Enums;
using StudentCurdApp.Models;

namespace StudentCurdApp.Data.Seed
{
    public static class DataSeeder
    {
        private static readonly UserRole[] Roles = { UserRole.IT, UserRole.HR, UserRole.PO, UserRole.Challan, UserRole.Accounts, UserRole.User, UserRole.Admin };
        private static readonly Random random = new();

        public static List<User> GenerateUsers(int count)
        {
            var users = new List<User>();

            for (int i = 1; i <= count; i++)
            {
                users.Add(new User
                {
                    Name = $"User{i}",
                    Role = Roles[random.Next(Roles.Length)],
                    Password = $"pass{i}"
                });
            }

            return users;
        }
    }
}
