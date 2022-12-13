using System;

namespace Models
{
    public class Employee 
    {
        private string passportData;

        public string PassportData
        {
            get { return passportData; }
            set => passportData = value;
        }

        private string lastName;

        public string LastName
        {
            get { return lastName; }
            set => lastName = value;
        }

        private string firstName;

        public string FirstName
        {
            get { return firstName; }
            set => firstName = value;
        }

        private string patronymic;

        public string Patronymic
        {
            get { return patronymic; }
            set => patronymic = value;
        }

        private string phoneNumber;

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set => phoneNumber = value;
        }

        private string imagePath;
        public string ImagePath
        {
            get { return imagePath; }
            set => imagePath = value;
        }
    }
}
