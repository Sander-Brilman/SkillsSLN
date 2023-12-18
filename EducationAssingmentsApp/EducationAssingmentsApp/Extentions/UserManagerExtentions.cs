using EducationAssingmentsApp.Data;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

namespace EducationAssingmentsApp.Extentions;

public static class UserManagerExtentions
{

    /// <summary>
    /// Extention method for creating an initial teacher user in the database for easy testing purposes.
    /// 
    /// Not to be used in a production senario
    /// </summary>
    /// <param name="userManager"></param>
    /// <returns></returns>
    public static async Task EnsureInitialTeacherExistsAsync(this UserManager<ApplicationUser> userManager)
    {
        const string initialUserEmail = "admin@admin.com";
        const string initalUserPassword = "x&ysan3@PmMJ!p!ZAVME39Aj76t#B5CEk%Zx*cre";

        ApplicationUser? initialUser = await userManager.FindByEmailAsync(initialUserEmail);

        if (initialUser is not null)
        {
            return;
        }

        initialUser = new ApplicationUser()
        {
            Email = initialUserEmail,
            UserName = initialUserEmail,
            FullName = "John Doe",
        };

        await userManager.CreateAsync(initialUser, initalUserPassword);

        await userManager.AddToRoleAsync(initialUser, "Admin");
    }

    /// <summary>
    /// provides a cryptographically random password
    /// </summary>
    /// <returns></returns>
    private static string GetRandomPassword()
    {
        return RandomNumberGenerator.GetString("QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890!+_#$@%", 20) +
            RandomNumberGenerator.GetString("1234567890", 3) +
            RandomNumberGenerator.GetString("!@#$%^&*", 3);
    }

    /// <summary>
    /// Creates a new student from the student's email and full name.
    /// </summary>
    /// <param name="email">The email of the student. this will be the value used to log in</param>
    /// <param name="fullName">The full name of the student for display purposes</param>
    /// <returns>
    /// either a combination of true + the new random password of the student in case of success
    /// or false + null if the operation failed
    /// </returns>
    public static async Task<(bool success, string? password)> CreateStudentAsync(this UserManager<ApplicationUser> userManager, ApplicationUser newStudent)
    {
        string newStudentPassword = GetRandomPassword();

        var createResult = await userManager.CreateAsync(newStudent, newStudentPassword);

        if (createResult.Succeeded)
        {
            return (true, newStudentPassword);
        }

        await userManager.AddToRoleAsync(newStudent, "Student");

        return (false, null);
    }

    public static async Task<string> ResetPasswordToNewRandomAsync(this UserManager<ApplicationUser> userManager, ApplicationUser user)
    {
        string newPassword = GetRandomPassword();

        string resetToken = await userManager.GeneratePasswordResetTokenAsync(user);
        await userManager.ResetPasswordAsync(user, resetToken, newPassword);

        return newPassword;
    }



}
