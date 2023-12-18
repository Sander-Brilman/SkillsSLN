using EducationAssingments.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace EducationAssingments.Data;
// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = "";

    public List<AssingmentAnswer> Answers { get; set; } = [];
}

