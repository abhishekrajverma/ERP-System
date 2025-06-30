using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace StudentCurdApp.Areas.Identity.Data;

// Add profile data for application users by adding properties to the StudentCurdAppUser class
public class StudentCurdAppUser : IdentityUser
{
    public DateTime CreatedAt { get; set; }
}

