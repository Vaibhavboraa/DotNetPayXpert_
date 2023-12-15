using PayXpert.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert.Repository
{
    internal interface IEmployeeSevice
    {
        
        public int AddEmployee(Employee employee);
        public int RemoveEmployee(int employeeId);
        public List<Employee> GetAllEmployees();
        
        public Employee GetEmployeeById(int employeeId);
        public int UpdateEmployee(Employee updatedEmployee);
        public int CalculateAge(int employeeId);



    }
}
