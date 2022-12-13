using DataBase;
using Employees.Services.ChangeContent;
using Employees.Services.DialogService;
using Employees.ViewModels.Base;
using Models;
using MySqlConnector;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Employees.ViewModels
{
    public class EmployeeListViewModel : BaseViewModel
    {
        #region fields
        private IChangeContent changeContent;
        private readonly ConnectionString connectionString;
        public ObservableCollection<Employee> Employees { get; private set; }
        private DefaultDialogService dialogService;
        #endregion

        #region properties
        private int selectedEmployee;
        public int SelectedEmployee
        {
            get => selectedEmployee;
            set
            {
                selectedEmployee = value;
                OnPropertyChanged(nameof(SelectedEmployee));
            }
        }
        #endregion

        #region commands

        private readonly RelayCommand openEmployeeDetailedInformation;
        public RelayCommand OpenEmployeeDetailedInformation => openEmployeeDetailedInformation;

        private readonly ICommand removeEmployeeCommand;
        public ICommand RemoveEmployeeCommand => removeEmployeeCommand;

        private readonly ICommand useFilterCommand;
        public ICommand UseFilterCommand => useFilterCommand;
        #endregion

        #region constructor

        public EmployeeListViewModel(ChangeContent changeContent)
        {
            connectionString = new ConnectionString();

            Employees = new ObservableCollection<Employee>();

            getEmployees();

            this.changeContent = changeContent;

            openEmployeeDetailedInformation = new RelayCommand(OnOpenEmployeeDetailedInformation, p => true);

            removeEmployeeCommand = new RelayCommand(p=>removeEmployee());

            dialogService = new DefaultDialogService();

            useFilterCommand = Task.Run(() => new RelayCommand(UseFilter, p => true)).Result;
        }
        #endregion

        #region methods

        /// <summary>
        /// remove employee data after fired
        /// </summary>
        private void removeEmployee()
        {
            bool removeChoice = dialogService.ChoosePopup("Вы точно хотите удалить данные", "Предупреждение");

            if(removeChoice)
            {
               
                string sqlInquiryString = "remove_employee";

                using (MySqlConnection connection = new MySqlConnection(connectionString.StringOfConnection))
                {
                    connection.Open();

                    MySqlCommand command = new MySqlCommand(sqlInquiryString, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add("@employee_id", MySqlDbType.Int32).Value = SelectedEmployee + 1;
                    
                    using(MySqlDataReader reader = command.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            if ((bool)reader[0])
                                dialogService.ShowMessage("Данные о сотруднике были удалены!", "Уведомление");
                            else
                                dialogService.ShowMessage("Невозможно удалить данные о директоре!", "Ошибка");
                        }

                    }
                }   
            }
        }

        private void OnOpenEmployeeDetailedInformation(object parameter)
        {
            changeContent.ChangeViewModel(new EmployeeDetailedInformationViewModel(SelectedEmployee + 1));
        }

        /// <summary>
        /// getting employees from db then showing it
        /// </summary>
        private void getEmployees()
        {
            string sqlInquiryString = "get_employees_main_information";
            using (MySqlConnection connection = new MySqlConnection(connectionString.StringOfConnection))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand(sqlInquiryString, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
 
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Employees.Add(new Employee
                        {
                            LastName = reader["last_name"].ToString(),
                            FirstName = reader["first_name"].ToString(),
                            Patronymic = reader["patronymic"].ToString(),
                            PhoneNumber = reader["phone_number"].ToString(),
                            PassportData = reader["passport"].ToString(),
                            ImagePath = reader["image_path"].ToString()
                        });
                    }
                }
            }
            
        }

        private void UseFilter(object parameter)
        {
            if (!(bool)parameter)
            {
                getEmployees();
                return;
            }

            ObservableCollection<Employee> tempEmployees = new ObservableCollection<Employee>();

            for(int i = 0; i < Employees.Count; i++)
            {
                if(Employees[i].FirstName == "Victor")
                {
                    tempEmployees.Add(Employees[i]);
                }
            }

            if(tempEmployees.Count <= 0)
            {
                Employees.Clear();
                return;
            }

            Employees = tempEmployees;
        }
        #endregion
    }
}
