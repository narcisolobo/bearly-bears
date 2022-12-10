using TheWall.Context;
using System.ComponentModel.DataAnnotations;
namespace TheWall.Validators;

public class UniqueEmailAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return new ValidationResult("Please enter your email.");
        }

        TheWallContext? _db = (TheWallContext?)validationContext.GetService(typeof(TheWallContext));

        if (_db is not null)
        {
             if (_db.Users.Any(user => user.Email == value.ToString()))
            {
                return new ValidationResult("Email in use. Please login.");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
        return new ValidationResult("Server error. DB connection not established.");
    }
}
