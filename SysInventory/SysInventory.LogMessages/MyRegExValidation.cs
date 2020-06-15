using System.Text.RegularExpressions;

namespace SysInventory.LogMessages
{
    public class MyRegExValidations
    {
        public bool ValidateCustomerNo(string number)
        {
            if (number == null) return false;
            var regex = new Regex(@"^CU\d{5}$");
            return regex.IsMatch(Regex.Escape(number));
        }
        public bool ValidateEMail(string email)
        {
            if (email == null) return false;
            var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            return regex.IsMatch(email);
        }

        public bool ValidateUrl(string url)
        {
            if (url == null) return false;
            var regex = new Regex(@"^(http:\/\/www\.|https:\/\/www\.|http:\/\/|https:\/\/)?[a-z0-9]+([\-\.]{1}[a-z0-9]+)*\.[a-z]{2,5}(:[0-9]{1,5})?(\/.*)?$");
            return regex.IsMatch(url);
        }
        public bool ValidatePassword(string password)
        {
            if (password == null) return false;
            // Mit Sonderzeichen:
            var regex = new Regex(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{8,32}$");
            // ohne Sonderzeichen: ^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])[a-zA-Z0-9]{8,32}$
            return regex.IsMatch(password);
        }
    }
}
