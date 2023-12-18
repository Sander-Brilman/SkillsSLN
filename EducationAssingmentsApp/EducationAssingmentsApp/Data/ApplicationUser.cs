using EducationAssingmentsApp.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EducationAssingmentsApp.Data;
// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    [MaxLength(100)]
    public string FullName { get; set; } = "";

    public List<AssingmentAnswer> Answers { get; set; } = [];

}

