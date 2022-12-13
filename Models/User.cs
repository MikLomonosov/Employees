using System;
using System.ComponentModel;

namespace Models
{
    public class User
    {
        private string phoneNumber;
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set => phoneNumber = value;
        }

        private string password;
        public string Password
        {
            get { return password; }
            set => password = value;
        }
    }
}
