using System;

namespace Employees.Services
{
    public class LoginEventArgs : EventArgs
    {
        public string User { get; private set; }
        public bool IsAuthorized { get; private set; }

        public LoginEventArgs(string user, bool isAuthorized)
        {
            this.User = user;
            this.IsAuthorized = isAuthorized;
        }
    }
}
