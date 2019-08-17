using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Renetdux.Infrastructure.ExtensionMethods
{
    public static class IsValidEmailExtensionMethod
    {
        private static readonly Regex rxNormalizeEmail = new Regex(@"(@)(.+)$", RegexOptions.Compiled, TimeSpan.FromMilliseconds(200));

        private static readonly Regex rxEmail = new Regex(@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.Compiled | RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));

        public static bool IsValidEmail(this string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = rxNormalizeEmail.Replace(email, DomainMapper);
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                return rxEmail.Match(email).Success;
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        /// <summary>
        /// Examines the domain part of the email and normalizes it.
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        private static string DomainMapper(Match match)
        {
            // Use IdnMapping class to convert Unicode domain names.
            var idn = new IdnMapping();

            // Pull out and process domain name (throws ArgumentException on invalid)
            var domainName = idn.GetAscii(match.Groups[2].Value);

            return match.Groups[1].Value + domainName;
        }
    }
}
