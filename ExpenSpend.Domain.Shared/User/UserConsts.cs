using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenSpend.Domain.Shared.User;

public class UserConsts
{
    public const string UserNameRegex = @"^[a-zA-Z0-9_]+$";

    public const string UserNameRegexErrorMessage = "Username can only contain letters, numbers, and underscores.";

    public const string FirstNameRegex = @"^[a-zA-Z]+$";

    public const string FirstNameRegexErrorMessage = "First name can only contain letters.";

    public const string LastNameRegex = @"^[a-zA-Z]+$";

    public const string LastNameRegexErrorMessage = "Last name can only contain letters.";

    public const string EmailErrorMessage = "Invalid email address.";

    public const string PhoneNumberRegex = @"^\d{10}$";

    public const string PhoneNumberRegexErrorMessage = "Phone number must be a 10-digit number.";

    public const string PasswordRegex = @"^(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$";

    public const string PasswordRegexErrorMessage = "Password must be 8 characters including one uppercase letter, one special character and alphanumeric characters";
}
