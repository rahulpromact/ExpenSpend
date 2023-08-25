using System.ComponentModel.DataAnnotations;
using ExpenSpend.Domain.Shared.User;

namespace ExpenSpend.Core.User;

public class UpdateUserDto
{
    [RegularExpression(UserConsts.UserNameRegex, ErrorMessage = UserConsts.UserNameRegexErrorMessage)]
    public required string UserName { get; set; }

    [RegularExpression(UserConsts.FirstNameRegex, ErrorMessage = UserConsts.FirstNameRegexErrorMessage)]
    public required string FirstName { get; set; }

    [RegularExpression(UserConsts.LastNameRegex, ErrorMessage = UserConsts.LastNameRegexErrorMessage)]
    public required string LastName { get; set; }

    [EmailAddress(ErrorMessage = UserConsts.EmailErrorMessage)]
    public required string Email { get; set; }

    [RegularExpression(UserConsts.PhoneNumberRegex, ErrorMessage = UserConsts.PhoneNumberRegexErrorMessage)]
    public required string PhoneNumber { get; set; }
}