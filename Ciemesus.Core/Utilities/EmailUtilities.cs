using System.Net.Mail;

namespace Ciemesus.Core.Utilities
{
    public static class EmailUtilities
    {
        public static bool IsEmail(string emailaddress)
        {
            try
            {
                var mailAddress = new MailAddress(emailaddress);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
