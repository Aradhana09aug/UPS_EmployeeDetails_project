using ExampleRESTFULLAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeTestCases
{
      public interface IEmployeeApiFake
    {
        List<EmployeeDetails> GetAllEmployeeAsync();
        List<EmployeeDetails> GetEmployeeByFirstNameAsync(string name);
        EmployeeDetails GetEmployeeByIdAsync(int id);
        void PostEmployeeDetailsAsync(EmployeeDetails emp);
        void UpdateEmployeeDetails(EmployeeDetails emp);
        EmployeeDetails DeleteEmployeeDetails(EmployeeDetails emp);
    }
}
