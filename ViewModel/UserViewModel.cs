using System;
using Models;
using DataBase;
using MySqlConnector;
using System.Windows.Controls;
using System.Security;
using System.Runtime.InteropServices;
using System.Windows;

namespace ViewModels
{
    public class UserViewModel
    {
        private ConnectionString connectionString;
        private Main main;
        private User user;

        //private string login;

        //public string Login
        //{
        //    get { return login; }
        //    set { login = value; }
        //}

        public UserViewModel()
        {
            connectionString = new ConnectionString();
            user = new User();
        }

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
            bool check = false;

            var passwordContainer = (parameter as PasswordBox).SecurePassword;
            user.Password = passwordContainer != null ? convertToUnsecureString(passwordContainer) : user.Password = null;

            string sqlInquiryString = $"SELECT * FROM users where phone_number = '{user.PhoneNumber}' AND password = '{user.Password}'";
            using var connection = new MySqlConnection(connectionString.StringOfConnection);
            connection.Open();

            MySqlCommand command = new MySqlCommand(sqlInquiryString, connection);

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    if(reader.Read())
                    {
                        check = true; 
                    }
                    //users.Add(new User
                    //{
                    //    PhoneNumber = reader["phone_number"].ToString(),
                    //    Password = reader["password"].ToString()
                    //});
                }
            }

            if (check)
            {
                main = new Main();
                System.Windows.Application.Current.MainWindow.Close();
                main.Show();
            }
            else
                MessageBox.Show("Your password or login is not correct :D " + user.Password, "Display");


            //if (users.Any(u => u.Password.Equals(Password)))
            //{
            //    informationWindow = new InformationWindow(Login);
            //    System.Windows.Application.Current.MainWindow.Close();
            //    informationWindow.Show();
            //}
            //else
            //    MessageBox.Show("Your password or login is not correct :D " + Password, "Display");
        }
    }
}
