using System.Text.RegularExpressions;

namespace WindowAppMain.Classes.WindowAuthClasses
{
    class CheckValidateForm
    {
        public bool IsValidateEmail(string emailText)
        {
            string patternEmail = @"^[-a-z0-9!#$%&'*+/=?^_`{|}~]+(?:\.[-a-z0-9!#$%&'*+/=?^_`{|}~]+)*@(?:[a-z0-9]([-a-z0-9]{0,61}[a-z0-9])?\.)*(?:aero|arpa|asia|biz|cat|com|coop|edu|gov|info|int|jobs|mil|mobi|museum|name|net|org|pro|tel|travel|[a-z][a-z])$";
            Match isMatch = Regex.Match(emailText, patternEmail, RegexOptions.IgnoreCase);
            return isMatch.Success;
        }

        public bool IsValidatePassword(string passwordOrigin, string passwordCopy)
        {
            bool check = false;
            if (passwordOrigin == passwordCopy)
            {
                string patternPassword = @"(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z]{8,}";
                Match isMatch = Regex.Match(passwordOrigin, patternPassword, RegexOptions.IgnoreCase);
                check = isMatch.Success;
                return check;
            }
            else
            {
                return check = false;
            }
        }

    }
}
