using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase
{
    public class ConnectionString
    {
        private readonly string userID;
        private readonly string dataBase;
        private readonly string server;
        private readonly string password;
        public string StringOfConnection { get; private set; }

        public ConnectionString()
        {
            server = "localhost";
            userID = "root";
            dataBase = "aliss_db";
            password = "";

            StringOfConnection = $"server = {server}; uid = {userID}; database = {dataBase}; password = {password}";
        }
    }
}
