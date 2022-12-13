using DataBase;
using Employees.Services;
using Employees.Services.DialogService;
using Employees.ViewModels.Base;
using Models;
using MySqlConnector;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace Employees.ViewModels
{
    public class AddEmployeeWindowViewModel : BaseViewModel
    {
        #region fields
        
        DefaultDialogService dialogService;
        IFileService fileService;
        private string imagePath;
        public event EventHandler<ChangeThemeEventArgs> OnCheckedDriveLicense;
        private ConnectionString connectionString;
        private string drivingLicense;
        #endregion

        #region properties
        private string lastName;

        public string LastName
        {
            get => lastName;
            set 
            {
                if (value.Length <= 100)
                {
                    lastName = value;
                }
                else if (value.Length > 100)
                    dialogService.ShowMessage("Длина Фамилии не должна превышать 100 символов!", "Ошибка");
                else if (value.Equals(null))
                    dialogService.ShowMessage("Поле 'Фамилия' должно быть заполнено", "Ошибка");
                OnPropertyChanged("LastName");
            }
        }

        private string firstName;
        public string FirstName
        {
            get => firstName;
            set
            {
                if (value.Length <= 70)
                {
                    firstName = value;
                }
                else if (value.Length > 70)
                    dialogService.ShowMessage("Длина Имени не должна превышать 70 символов!", "Ошибка");
                else if (value.Equals(null))
                    dialogService.ShowMessage("Поле 'Имя' должно быть заполнено", "Ошибка");
                OnPropertyChanged("FirstName");
            }
        }

        private string secondName;
        public string SecondName
        {
            get => secondName;
            set
            {
                if (value.Length <= 100)
                {
                    secondName = value;
                }
                else if (value.Length > 100)
                    dialogService.ShowMessage("Длина Отчества не должна превышать 100 символов!", "Ошибка");
                OnPropertyChanged("SecondName");
            }
        }

        private string _SNILS;
        public string SNILS
        {
            get => _SNILS;
            set
            {
                if (value.Length == 11)
                {
                    bool checkValue = ulong.TryParse(value, out ulong result);

                    if (checkValue)
                        _SNILS = value;
                    else
                        dialogService.ShowMessage("СНИЛС должен содержать только цифры ", "Ошибка");
                }
                else if (value.Length < 11)
                    dialogService.ShowMessage("Длина СНИЛС должна составлять 11 символов", "Ошибка");
                else if (value.Equals(null))
                    dialogService.ShowMessage("Поле 'СНИЛС' должно быть заполнено", "Ошибка");
                OnPropertyChanged("SNILS");
            }
        }

        private string _INN;
        public string INN
        {
            get => _INN;
            set
            {
                if (value.Length == 12)
                {
                    bool checkValue = ulong.TryParse(value, out ulong result);
                    if (checkValue)
                        _INN = value;
                    else
                        dialogService.ShowMessage("ИНН должен содержать только цифры", "Ошибка");
                }
                else if (value.Length < 12)
                    dialogService.ShowMessage("Длина ИНН должна составлять 12 символов", "Ошибка");
                else if (value.Equals(null))
                    dialogService.ShowMessage("Поле 'ИНН' должно быть заполнено", "Ошибка");
                OnPropertyChanged("INN");
            }
        }

        private string phoneNumber;
        public string PhoneNumber
        {
            get => phoneNumber;
            set
            {

                if (value.Length == 10)
                {
                    bool checkValue = ulong.TryParse(value, out ulong result);
                    if (checkValue)
                        phoneNumber = value;
                    else
                        dialogService.ShowMessage("Номер телефона должен содержать только цифры", "Ошибка");
                }
                else if (value.Length < 10)
                    dialogService.ShowMessage("Длина Номера телефона должна составлять 10 символов", "Ошибка");
                else if (value.Equals(null))
                    dialogService.ShowMessage("Поле 'Номер телефона' должно быть заполнено", "Ошибка");
                OnPropertyChanged("PhoneNumber");
            }
        }

        private string address;
        public string Address 
        {
            get => address;
            set
            {
                if (value.Length <= 200)
                {
                    address = value;
                }
                else if (value.Length > 200)
                    dialogService.ShowMessage("Длина Адресса не должна превышать 200 символов", "Ошибка");
                else if (value.Equals(null))
                    dialogService.ShowMessage("Поле 'Адресс регистрации' должно быть заполнено", "Ошибка");
                OnPropertyChanged("Address");
            }
        }

        private string passport;
        public string Passport
        {
            get => passport;
            set
            {
                if (value.Length == 10)
                {
                    bool checkValue = ulong.TryParse(value, out ulong result);

                    if (checkValue)
                        passport = value;
                    else
                        dialogService.ShowMessage("Паспорт должен содержать только цифры", "Ошибка");
                }
                else if (value.Length < 10)
                    dialogService.ShowMessage("Длина Паспорта должна составлять 10 символов", "Ошибка");
                else if (value.Equals(null))
                    dialogService.ShowMessage("Поле 'Паспор' должно быть заполнено", "Ошибка");
                OnPropertyChanged("Passport");
            }
        }

        private string gettingPlace;
        public string GettingPlace
        {
            get => gettingPlace;
            set
            {
                if (value.Length <= 200)
                {
                    gettingPlace = value;
                }
                else if (value.Equals(null))
                    dialogService.ShowMessage("Поле 'Где был выдан' должно быть заполнено");
                OnPropertyChanged("GettingPlace");
            }
        }
        
        private DateTime gettingDate = DateTime.Now;
        public DateTime GettingDate 
        {
            get => gettingDate;
            set
            {
                gettingDate = value;
                OnPropertyChanged("GettingDate");
            }
        }
        public ObservableCollection<string> Professions { get; private set; }

        private int selectedProfession;
        public int SelectedProfession
        {
            get => selectedProfession;
            set
            {
                if(!value.Equals(null))
                {
                    selectedProfession = value + 1;
                    
                }
                
                OnPropertyChanged("SelectedProfession");
            }
        }
        private DateTime birthDate = DateTime.Now;
        public DateTime BirthDate
        {
            get => birthDate;
            set
            {
                birthDate = value;
                OnPropertyChanged("BirthDate");
            }
        }
        
        private ObservableCollection<DrivingLicense> drivingLicenses;
        public ObservableCollection<DrivingLicense> DrivingLicenses
        {
            get => drivingLicenses;
            set
            {
                drivingLicenses = value;
                OnPropertyChanged("DrivingLicense");
            }
        }
        private ObservableCollection<string> maritalStatuses;
        public ObservableCollection<string> MaritalStatuses
        {
            get => maritalStatuses;
            set
            {
                maritalStatuses = value;
                OnPropertyChanged("MaritalStatuses");
            }
        }

        private string selectedMaritalStatus;
        public string SelectedMaritalStatus
        {
            get => selectedMaritalStatus;
            set
            {
                selectedMaritalStatus = value;
                OnPropertyChanged("SelectedMaritalStatus");
            }
        }

        private int selectedWorkYearExperience;
        public int SelectedWorkYearExperience
        {
            get => selectedWorkYearExperience;
            set
            {
                selectedWorkYearExperience = value;
                OnPropertyChanged("SelectedWorkYearExperience");
            }
        }

        private int selectedWorkMonthExperience;
        public int SelectedWorkMonthExperience
        {
            get => selectedWorkMonthExperience;
            set
            {
                selectedWorkMonthExperience = value;
                OnPropertyChanged("SelectedWorkMonthExperience");
            }
        }
        public ObservableCollection<int> WorkYears { get; private set; }
        public ObservableCollection<int> WorkMonths { get; private set; }
        #endregion

        #region commands
        private readonly RelayCommand openFileCommand;
        public RelayCommand OpenFileCommand => openFileCommand;

        private readonly RelayCommand radioButtonCommand;
        public RelayCommand RadioButtonCommand => radioButtonCommand;

        private readonly RelayCommand registrateCommand;
        public RelayCommand RegistrateCommand => registrateCommand;

        #endregion

        #region constructor
        public AddEmployeeWindowViewModel()
        {
            dialogService = new DefaultDialogService();

            openFileCommand = new RelayCommand(c => openFile(), c => true);

            setWorkYears();
            setWorkMonths();

            setProfessions();

            //-------must change 
            MaritalStatuses = new ObservableCollection<string>();
            addMaritalStatuses();

            DrivingLicenses = new ObservableCollection<DrivingLicense>();

            radioButtonCommand = new RelayCommand(radioButtonChosen, p => true);

            registrateCommand = new RelayCommand(p => executeEmployeeRegistration());
        }
        #endregion

        #region methods
        /// <summary>
        /// add marital statuses to combobox for choice
        /// </summary>
        private void addMaritalStatuses()
        {
            if(maritalStatuses.Count < 1)
            {
                maritalStatuses.Add("Не замужем/Не женат");
                maritalStatuses.Add("Замужем/Женат");
            }
        }
    
        /// <summary>
        /// add options of driving licenses after chosen "I have driving license" and "I don't have"
        /// </summary>
        /// <param name="parameter"></param>
        private void radioButtonChosen(object parameter)
        {
            if((string)parameter == "DoNotHaveLicense")
            {
                if(drivingLicenses.Count < 1)
                {
                    DrivingLicenses.Add(new DrivingLicense
                    {
                        Text = "Нет",
                        IsSelected = false
                    });
                }
                else if(drivingLicenses.Count > 1)
                {
                    drivingLicenses.Clear();

                    drivingLicenses.Add(new DrivingLicense
                    {
                        Text = "Нет",
                        IsSelected = false
                    });
                }
                //OnCheckedDriveLicense?.Invoke(this, new ChangeThemeEventArgs(true));
            }
            else if((string)parameter == "HaveLicense")
            {
                if (drivingLicenses.Count < 1)
                {
                    DrivingLicenses.Add(new DrivingLicense
                    {
                        Text = "B",
                        IsSelected = false
                    });
                    DrivingLicenses.Add(new DrivingLicense
                    {
                        Text = "BE",
                        IsSelected = false
                    });
                    DrivingLicenses.Add(new DrivingLicense
                    {
                        Text = "C",
                        IsSelected = false
                    });
                    DrivingLicenses.Add(new DrivingLicense
                    {
                        Text = "CE",
                        IsSelected = false
                    });
                    DrivingLicenses.Add(new DrivingLicense
                    {
                        Text = "D",
                        IsSelected = false
                    });
                    DrivingLicenses.Add(new DrivingLicense
                    {
                        Text = "DE",
                        IsSelected = false
                    });
                }
                else if (drivingLicenses.Count == 1)
                {
                    drivingLicenses.Clear();

                    DrivingLicenses.Add(new DrivingLicense
                    {
                        Text = "B",
                        IsSelected = false
                    });
                    DrivingLicenses.Add(new DrivingLicense
                    {
                        Text = "BE",
                        IsSelected = false
                    });
                    DrivingLicenses.Add(new DrivingLicense
                    {
                        Text = "C",
                        IsSelected = false
                    });
                    DrivingLicenses.Add(new DrivingLicense
                    {
                        Text = "CE",
                        IsSelected = false
                    });
                    DrivingLicenses.Add(new DrivingLicense
                    {
                        Text = "D",
                        IsSelected = false
                    });
                    DrivingLicenses.Add(new DrivingLicense
                    {
                        Text = "DE",
                        IsSelected = false
                    });
                } 
            }
        }

        /// <summary>
        /// get open file path for employees' image
        /// </summary>
        private void openFile()
        {
           
            fileService = new StringFileService();

            try
            {
                if (dialogService.OpenFileDialog())
                {
                    imagePath = Path.GetFullPath(fileService.Open(dialogService.FilePath));
                    dialogService.ShowMessage("Фотография выбрана", "Уведомление");
                }
            }
            catch (Exception ex)
            {
                dialogService.ShowMessage(ex.Message, "Ошибка");
            }
        }
        private void getDrivingLicenses()
        {
            for(int i = 0; i < DrivingLicenses.Count; i++)
            {
                if(DrivingLicenses[i].IsSelected)
                {
                    drivingLicense += DrivingLicenses[i].Text + ",";
                }
            }
        }
        private void setProfessions()
        {
            connectionString = new ConnectionString();

            Professions = new ObservableCollection<string>();

            string sqlInquiryString = "SELECT name FROM professions";

            using (MySqlConnection connection = new MySqlConnection(connectionString.StringOfConnection))
            {
                connection.Open();

                using(MySqlCommand command = new MySqlCommand(sqlInquiryString,connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            Professions.Add(reader["name"].ToString());
                        }
                    }
                }
            }
        }
        private void setWorkYears()
        {
            WorkYears = new ObservableCollection<int>();
            for(int i = 1; i <= 100; i++)
            {
                WorkYears.Add(i);
            }
        }
        private void setWorkMonths()
        {
            WorkMonths = new ObservableCollection<int>();

            for(int i = 1; i <= 12; i++)
            {
                WorkMonths.Add(i);
            }
        }
        /// <summary>
        /// insert all data to database
        /// </summary>
        private void executeEmployeeRegistration()
        {
            bool registerChoice = dialogService.ChoosePopup("Берем сотрудника на работу?", "Регистрация");

            if(registerChoice)
            {
                getDrivingLicenses();

                connectionString = new ConnectionString();

                string sqlInquiryString = "insert_employee_data";

                using (MySqlConnection connection = new MySqlConnection(connectionString.StringOfConnection))
                {
                    connection.Open();

                    MySqlCommand command = new MySqlCommand(sqlInquiryString, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add("@last_name", MySqlDbType.VarChar).Value = LastName;
                    command.Parameters.Add("@first_name", MySqlDbType.VarChar).Value = FirstName;
                    command.Parameters.Add("@second_name", MySqlDbType.VarChar).Value = SecondName;
                    command.Parameters.Add("@phone_number", MySqlDbType.VarChar).Value = PhoneNumber;
                    command.Parameters.Add("@inn", MySqlDbType.VarChar).Value = INN;
                    command.Parameters.Add("@snils", MySqlDbType.VarChar).Value = SNILS;
                    command.Parameters.Add("@address", MySqlDbType.VarChar).Value = Address;
                    command.Parameters.Add("@passport", MySqlDbType.VarChar).Value = Passport;
                    command.Parameters.Add("@getting_place", MySqlDbType.VarChar).Value = GettingPlace;
                    command.Parameters.Add("@getting_date", MySqlDbType.Date).Value = GettingDate;
                    command.Parameters.Add("@marital_status", MySqlDbType.VarChar).Value = SelectedMaritalStatus;
                    command.Parameters.Add("@image_path", MySqlDbType.VarChar).Value = imagePath;
                    command.Parameters.Add("@birth_date", MySqlDbType.Date).Value = BirthDate;
                    command.Parameters.Add("@work_year_experience", MySqlDbType.Int32).Value = SelectedWorkYearExperience;
                    command.Parameters.Add("@work_month_experience", MySqlDbType.Int32).Value = SelectedWorkMonthExperience;
                    command.Parameters.Add("@driving_license", MySqlDbType.VarChar).Value = drivingLicense;
                    command.Parameters.Add("@id_profession", MySqlDbType.Int32).Value = SelectedProfession;

                    
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            if (reader["result"].ToString() == LastName)
                                dialogService.ShowMessage("Сотрудник был внесен в базу данных", "Уведомление");
                            else if (reader["result"].ToString() == INN)
                                dialogService.ShowMessage("Сотрудник с таким инн : " + reader["result"].ToString() + " уже существует!", "Ошибка");
                            else if (reader["result"].ToString() == SNILS)
                                dialogService.ShowMessage("Сотрудник с таким СНИЛС : " + reader["result"].ToString() + " уже существует!", "Ошибка");
                            else if (reader["result"].ToString() == passport)
                                dialogService.ShowMessage("Сотрудник с таким паспортом : "+ reader["result"].ToString() +" уже существует", "Ошибка");
                            else if (reader["result"].ToString() == PhoneNumber)
                                dialogService.ShowMessage("Сотрудник с таким номером телефона : " + reader["result"].ToString() + " уже существует", "Ошибка");

                        }
                    }
                    //doesn't work
                    //не проверяет были ли внесены данные, надо добавить

                    
                }
            }
            
        }
        #endregion
    }
}
