using Employees.ViewModels.Base;
using System.Windows.Input;
using DataBase;
using MySqlConnector;
using Employees.Services.DialogService;
using System;
using Employees.Services;
using Employees.Commands;

namespace Employees.ViewModels
{
    public class ManageEmployeeViewModel : BaseViewModel
    {
        #region fields
        private ConnectionString connectionString;
        private DefaultDialogService dialogService;
        public event EventHandler<ChangeThemeEventArgs> OnSelectedTheme;
        #endregion

        #region properties

        private string fullName;
        public string FullName
        {
            get => fullName;
            set
            {
                if (!value.Equals(string.Empty))
                    fullName = value;
                OnPropertyChanged(nameof(FullName));
            }
        }

        private string imagePath;
        public string ImagePath
        {
            get => imagePath;
            set
            {
                if (value != null)
                    imagePath = value;
                OnPropertyChanged(nameof(ImagePath));
            }
        }

        private string phoneNumber;
        public string PhoneNumber
        {
            get => phoneNumber;
            set
            {
                phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

        private string profession;
        public string Profession 
        {
            get => profession;
            set
            {
                profession = value;
                OnPropertyChanged(nameof(Profession));
            }
        }

        private string workingHours;
        public string WorkingHours
        {
            get => workingHours;
            set
            {
                workingHours = value + " часов";
                OnPropertyChanged(nameof(workingHours));
            }
        }
        #endregion

        #region commands

        private readonly ICommand aboutCommand;
        public ICommand AboutCommand => aboutCommand;

        private readonly ICommand darkThemeCommand;
        public ICommand DarkThemeCommand => darkThemeCommand;

        private readonly ICommand lightThemeCommand;
        public ICommand LightThemeCommand => lightThemeCommand;

        private readonly ICommand addEmployeeCommand;
        public ICommand AddEmployeeCommand => addEmployeeCommand;

        #endregion

        #region constructor
        public ManageEmployeeViewModel(object user)
        {
            aboutCommand = new RelayCommand(p => showAboutInformation());
            getUserInformation(user);  
            dialogService = new DefaultDialogService();

            darkThemeCommand = new RelayCommand(p => changeTheme("Dark"), p => true);
            lightThemeCommand = new RelayCommand(p => changeTheme("Light"), p => true);

            addEmployeeCommand = new AddEmployeeCommand();
        }

        #endregion

        #region methods
        private void changeTheme(string theme)
        {
            OnSelectedTheme?.Invoke(this, new ChangeThemeEventArgs(true, theme));
        }
        /// <summary>
        /// getting user information for application settings.
        /// </summary>
        /// <param name="user"></param>
        private void getUserInformation(object user)
        {
            connectionString = new ConnectionString();

            string checkMonthAndYear = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString("d2") + "%";

            using (MySqlConnection connection = new MySqlConnection(connectionString.StringOfConnection))
            {
                connection.Open();
                string sqlInquiryString = "get_user";

                MySqlCommand command = new MySqlCommand(sqlInquiryString, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add("@login", MySqlDbType.VarChar).Value = (string)user;
                command.Parameters.Add("@check_date", MySqlDbType.VarChar).Value = checkMonthAndYear;

                using(MySqlDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        this.FullName = reader["last_name"].ToString() + " " 
                            + reader["first_name"].ToString() + " " 
                            + reader["patronymic"].ToString();

                        this.ImagePath = reader["image_path"].ToString();
                        this.PhoneNumber = reader["phone_number"].ToString();
                        this.Profession = reader["name"].ToString();
                        this.WorkingHours = reader["working_hours"].ToString();
                    }
                }
            }   
        }
        private void showAboutInformation()
        {
            dialogService.ShowMessage("This is Johny!!!", "About");
        }

        #endregion
    }
}
