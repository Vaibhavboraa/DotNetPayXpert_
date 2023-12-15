using PayXpert.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert.Repository
{
    internal interface IPayrollService
    {
        public Payroll GetPayrollById(int payrollId);
        public List<Payroll> GetPayrollsForEmployee(int employeeId);
        public List<Payroll> GetPayrollsForPeriod(DateTime startDate, DateTime endDate);
        public void GeneratePayroll(int employeeId, DateTime startDate, DateTime endDate);


    }
}
