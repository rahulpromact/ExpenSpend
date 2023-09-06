using System.ComponentModel.DataAnnotations;
using ExpenSpend.Domain.Shared.User;

namespace ExpenSpend.Core.User;

public class UpdateUserDto
{
    [RegularExpression(UserConsts.UserNameRegex, ErrorMessage = UserConsts.UserNameRegexErrorMessage)]
    public string? UserName { get; set; }

    [RegularExpression(UserConsts.FirstNameRegex, ErrorMessage = UserConsts.FirstNameRegexErrorMessage)]
    public string? FirstName { get; set; }

    [RegularExpression(UserConsts.LastNameRegex, ErrorMessage = UserConsts.LastNameRegexErrorMessage)]
    public string? LastName { get; set; }

    [EmailAddress(ErrorMessage = UserConsts.EmailErrorMessage)]
    public string? Email { get; set; }

    [RegularExpression(UserConsts.PhoneNumberRegex, ErrorMessage = UserConsts.PhoneNumberRegexErrorMessage)]
    public string? PhoneNumber { get; set; }
}