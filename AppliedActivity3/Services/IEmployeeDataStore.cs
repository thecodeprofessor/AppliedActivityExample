using EmployeeApplication.Models;

namespace EmployeeApplication.Services
{
    interface IEmployeeDataStore
    {
        Task<List<Employee>> GetEmployeesAsync();
        Task<Employee> GetEmployeeAsync(int id);
        Task<int> SaveEmployeeAsync(Employee employee);
        Task<int> DeleteEmployeeAsync(Employee employee);
    }
}
