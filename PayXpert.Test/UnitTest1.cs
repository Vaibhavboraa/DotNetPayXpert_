using NUnit.Framework;
using PayXpert.Model;
using PayXpert.Repository;

namespace PayXpert.Test
{
    public class Tests
    {
        private string connectionString = "Server=LAPTOP-DUMLSALK;Database=PayXpert;Trusted_Connection=True";

        [Test]
        public void TestToAddEmployee()
        {
            
            EmployeeService employeeService = new EmployeeService();
            employeeService.connectionString = connectionString;

           
            int addEmployeeStatus = employeeService.AddEmployee(new Model.Employee
            {
                FirstName = "Vaibhv",
                LastName = "Bora",
                DateOfBirth = new DateTime(2002, 1, 1),
                Gender = "Male",
                Email = "vaibhav@example.com",
                PhoneNumber = "1234567890",
                Address = "123 Main Street",
                Position = "Developer",
                JoiningDate = new DateTime(2022, 1, 1),
                TerminationDate = null 
            });

            
            Assert.AreEqual(1, addEmployeeStatus);
        }
        
       

            [Test]
            public void GetFinancialRecordsForEmployeeTest()
            {
                
                FinancialRecordService financialService = new FinancialRecordService();
                financialService.ConnectionString = connectionString;

                
                int employeeId = 4;

                
                List<FinancialRecord> financialRecords = financialService.GetFinancialRecordsForEmployee(employeeId);

                
                Assert.IsNotNull(financialRecords);
                Assert.IsTrue(financialRecords.Count > 0);

                
                foreach (var record in financialRecords)
                {
                    Assert.AreEqual(employeeId, record.EmployeeID);
                }
            }
        [Test]
        public void CalculateTax_ValidInput_ReturnsCorrectTaxAmount()
        {
            
            TaxService taxService = new TaxService(connectionString);

            
            int employeeId = 2;
            int taxYear = 2023;

            
            int result = taxService.CalculateTax(employeeId, taxYear);

            
            Assert.AreEqual(4500, result); 
        }

       

        [Test]
        public void GeneratePayroll_ValidInput_CorrectNetSalary()
        {

            PayrollService payrollService = new PayrollService();
            payrollService.connectionString = connectionString;

            int employeeId = 4;
            DateTime startDate = DateTime.Parse("2023-01-01");
            DateTime endDate = DateTime.Parse("2023-01-15");
            decimal basicSalary = 5200.00m;
            decimal overtimePay = 220.00m;
            decimal deductions = 320.00m;
            decimal expectedNetSalary = 5100.00m;

            
            payrollService.GeneratePayroll(employeeId, startDate, endDate);

           
            Payroll generatedPayroll = payrollService.GetPayrollById(employeeId);

            Assert.AreEqual(expectedNetSalary, generatedPayroll.NetSalary);
        }

    }
}