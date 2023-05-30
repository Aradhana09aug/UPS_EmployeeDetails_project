
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExampleRESTFULLAPI.Models;

namespace EmployeeTestCases
{
    class EmployeeApiFake : IEmployeeApiFake
    {
        private readonly List<EmployeeDetails> _employee;
        public EmployeeApiFake()
        {

            _employee = new List<EmployeeDetails>()
            {
                 new EmployeeDetails() { id = 1, name = "Aradhana", email = "aradhana@gmail.com", gender = "female" },
                 new EmployeeDetails() { id = 2, name = "Sadhana", email = "sadhana@gmail.com", gender = "female" },
                 new EmployeeDetails() { id = 3, name = "Atul", email = "atul@gmail.com", gender = "male" }
              };

         
        }

        public List<EmployeeDetails> GetAllEmployeeAsync()
        {
            return _employee;
        }

        public EmployeeDetails GetEmployeeByFirstNameAsync(string name)
        {
            return _employee.Where(a => a.name == name)
           .FirstOrDefault();
        }

        public EmployeeDetails GetEmployeeByIdAsync(int id)
        {
            return _employee.Where(a => a.id == id)
          .FirstOrDefault();
        }

        public void PostEmployeeDetailsAsync(EmployeeDetails emp)
        {
            _employee.ToList().Add( emp);
        }

        public void UpdateEmployeeDetails(EmployeeDetails emp)
        {
            throw new NotImplementedException();
        }

        public EmployeeDetails DeleteEmployeeDetails(EmployeeDetails emp)
        {
            var existing = _employee.First(a => a.id == emp.id);
            _employee.ToList().Remove(existing);

            return emp;
        }

        List<EmployeeDetails> IEmployeeApiFake.GetEmployeeByFirstNameAsync(string name)
        {
            throw new NotImplementedException();
        }
    }

}
