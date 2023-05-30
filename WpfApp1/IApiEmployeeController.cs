using ExampleRESTFULLAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExampleRESTFULLAPI.ApiControllers
{
    public interface IApiEmployeeController
    {
        Task<IEnumerable<EmployeeDetails>> GetAllEmployeeAsync();
        Task<IEnumerable<EmployeeDetails>> GetEmployeeByFirstNameAsync(string name);
        Task<EmployeeDetails> GetEmployeeByIdAsync(int id);
        Task PostEmployeeDetailsAsync(EmployeeDetails emp);
        void UpdateEmployeeDetails(EmployeeDetails emp);
        Task<EmployeeDetails> DeleteEmployeeDetails(EmployeeDetails emp);
    }
}
