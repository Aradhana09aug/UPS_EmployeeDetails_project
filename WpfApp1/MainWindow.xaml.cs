using ExampleRESTFULLAPI.ApiControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ExampleRESTFULLAPI.Models;
using System.IO;
using CsvHelper;
using System.Globalization;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IApiEmployeeController _apiEmployee { get; }
        EmployeeDetails NewEmployee = new EmployeeDetails();
        EmployeeDetails selectedEmployee = new EmployeeDetails();
        public MainWindow(IApiEmployeeController apiEmployee)
        {           
            _apiEmployee = apiEmployee;
            InitializeComponent();
            GetAllEmployee();
            NewEmployeeGrid.DataContext = NewEmployee;

        }

        private void GetAllEmployee()
        {
            EmployeeDG.ItemsSource   = _apiEmployee.GetAllEmployeeAsync().Result;
        }

        private void AddEmployee(object s, RoutedEventArgs e)
        {
            _apiEmployee.PostEmployeeDetailsAsync(NewEmployee);
            GetAllEmployee();
            NewEmployee = new EmployeeDetails();
            NewEmployeeGrid.DataContext = NewEmployee;
        }

        private void UpdateEmployee(object s, RoutedEventArgs e)
        {
            _apiEmployee.UpdateEmployeeDetails(selectedEmployee);
            GetAllEmployee();
        }

        private void EditEmployee(object s, RoutedEventArgs e)
        {
            selectedEmployee = (s as FrameworkElement).DataContext as EmployeeDetails;
            UpdateEmployeeGrid.DataContext = selectedEmployee;
        }

        private void DeleteEmployee(object s, RoutedEventArgs e)
        {
            var employeeToDelete = (s as FrameworkElement).DataContext as EmployeeDetails;
             var result =_apiEmployee.DeleteEmployeeDetails(employeeToDelete);
            GetAllEmployee();
        }

        private void ExportEmployee(object s, RoutedEventArgs e)
        {
            var employee = _apiEmployee.GetAllEmployeeAsync().Result;

            using (var writer = new StreamWriter(@"C:\Output.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(employee);
            }
        }
    }
}
