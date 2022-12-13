using Employees.ViewModels.Base;
using Models;
using MySqlConnector;
using DataBase;
using System.Collections.ObjectModel;
using System;
using System.Windows.Input;
using System.Windows;

namespace Employees.ViewModels
{
    public class EmployeeDetailedInformationViewModel : BaseViewModel
    {
        #region fields

        private int selectedEmployee;
        private ConnectionString connectionString;

        private string lastName;
        private string firstName;
        private string secondName;
        #endregion

        #region properties

        private string fullName;
        public string FullName
        {
            get => fullName;
            set
            {
                fullName = value;
                OnPropertyChanged(nameof(FullName));
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

        private string passport;
        public string Passport
        {
            get => passport;
            set
            {
                passport = value;
                OnPropertyChanged(nameof(Passport));
            }
        }

        private string imagePath;
        public string ImagePath
        {
            get => imagePath;
            set
            {
                imagePath = value;
                OnPropertyChanged(nameof(ImagePath));
            }
        }

        private string month;
        public string Month
        {
            get => month;
            set
            {
                month = value;
                OnPropertyChanged(nameof(month));
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

        private int businessTravelDays;
        public int BusinessTravelDays 
        {
            get => businessTravelDays;
            set
            {
                businessTravelDays = value;
                OnPropertyChanged(nameof(BusinessTravelDays));
            }
        }

        private int workingHours;
        public int WorkingHours
        {
            get => workingHours;
            set
            {
                workingHours = value;
                OnPropertyChanged(nameof(WorkingHours));
            }
        }

        private int sickLeave;
        public int SickLeave
        {
            get => sickLeave;
            set
            {
                sickLeave = value;
                OnPropertyChanged(nameof(SickLeave));
            }
        }

        private int vacationLeave;
        public int VacationLeave
        {
            get => vacationLeave;
            set
            {
                vacationLeave = value;
                OnPropertyChanged(nameof(vacationLeave));
            }
        }

        private ObservableCollection<EmployeeSchedule> employeeSchedules;
        public ObservableCollection<EmployeeSchedule> EmployeeSchedules
        {
            get => employeeSchedules;
            set
            {
                employeeSchedules = value;
                OnPropertyChanged(nameof(EmployeeSchedules));
            }
        }
        #endregion

        #region commands

        private readonly ICommand saveScheduleCommand;
        public ICommand SaveScheduleCommand => saveScheduleCommand;

        #endregion

        #region constructor
        public EmployeeDetailedInformationViewModel(int selectedEmployee)
        {
            this.selectedEmployee = selectedEmployee;

            gettingEmployeeInformation();

            

            fillSchedule();

            Month = DateTime.Now.ToString("MMM");

            saveScheduleCommand = new RelayCommand(saveSchedule, p => true);
        }
        #endregion

        #region methods

        private void saveSchedule(object parameter)
        {
            string sqlInquiryString = "insert_or_update_employee_schedule";

            using(MySqlConnection connection = new MySqlConnection(connectionString.StringOfConnection))
            {
                connection.Open();

                for (int i = 0; i <= employeeSchedules.Count-1; i++)
                {
                    if (!employeeSchedules[i].ActionOnDay.Equals(""))
                    {
                        MySqlCommand command = new MySqlCommand(sqlInquiryString, connection);
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.Add("@employee_id", MySqlDbType.Int32).Value = selectedEmployee;
                        command.Parameters.Add("@employee_date", MySqlDbType.Date).Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, employeeSchedules[i].Days);
                        command.Parameters.Add("@action_on_day", MySqlDbType.VarChar).Value = employeeSchedules[i].ActionOnDay;
                        command.ExecuteNonQuery();
                    }
                    else
                        break;           
                }
                
            }
        }
        private void fillSchedule()
        {
            EmployeeSchedules = new ObservableCollection<EmployeeSchedule>();

            string sqlInquiryString = "employee_action_on_day";

            int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

            string checkMonthAndYear = DateTime.Now.Year.ToString()+ "-" + DateTime.Now.Month.ToString("d2") + "%";

            using (MySqlConnection connection = new MySqlConnection(connectionString.StringOfConnection))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand(sqlInquiryString, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add("@employee_id", MySqlDbType.Int32).Value = selectedEmployee;
                command.Parameters.Add("@check_date", MySqlDbType.VarChar).Value = checkMonthAndYear;

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    for (int i = 1; i <= daysInMonth; i++)
                    {
                        if(reader.Read())
                        {
                            EmployeeSchedules.Add(new EmployeeSchedule
                            {
                                Days = i,
                                ActionOnDay = reader["action_on_day"].ToString()
                            });
                        }
                        else
                        {
                            EmployeeSchedules.Add(new EmployeeSchedule
                            {
                                Days = i,
                                ActionOnDay = ""
                            });
                        }
                        
                    }
                }
            }   
        }

        private void gettingEmployeeInformation()
        {
            connectionString = new ConnectionString();

            string sqlInquiryString = "get_employee_information";

            string checkMonthAndYear = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString("d2") + "%";

            using (MySqlConnection connection = new MySqlConnection(connectionString.StringOfConnection))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand(sqlInquiryString, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.Add("@employee_id", MySqlDbType.Int32).Value = selectedEmployee;
                command.Parameters.Add("@check_date", MySqlDbType.VarChar).Value = checkMonthAndYear;

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lastName = reader["last_name"].ToString();
                        firstName = reader["first_name"].ToString();
                        secondName = reader["patronymic"].ToString();
                        PhoneNumber = reader["phone_number"].ToString();
                        Passport = reader["passport"].ToString();
                        ImagePath = reader["image_path"].ToString();
                        Profession = reader["name"].ToString();
                        BusinessTravelDays = (int)reader["business_travel_days"];
                        WorkingHours = (int)reader["working_hours"];
                        SickLeave = (int)reader["sick_leave"];
                        VacationLeave = (int)reader["vacation_leave"];
                    }
                }
            }
            fullName = lastName + " " + firstName + " " + secondName;
        }
        #endregion
    }
}
