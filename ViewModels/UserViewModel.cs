using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;

namespace ViewModels
{
    public class UserViewModel
    {
        private string convertToUnsecureString(SecureString securePassword)
        {
            if (securePassword == null)
            {
                return string.Empty;
            }

            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
        private void checkUser(object parameter)
        {
            List<User> users = new List<User>();
            string sqlInquiryString = $"SELECT * FROM users_authorization where login = '{Login}'";
            using var connection = new MySqlConnection(createConnection.ConnectionString());
            connection.Open();

            MySqlCommand command = new MySqlCommand(sqlInquiryString, connection);

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    users.Add(new User
                    {
                        Login = reader["login"].ToString(),
                        Password = reader["password"].ToString()
                    });
                }
            }
        }
}
