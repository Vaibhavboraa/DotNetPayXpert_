using NUnit.Framework;
using PayXpert.Model;
using PayXpert.Repository;
using System.Data.SqlClient;

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
            public void TestGeneratePayroll()
            {
                // Arrange
                PayrollService payrollService = new PayrollService();
                payrollService.connectionString = connectionString;

                int employeeId = 5; 
                DateTime startDate = new DateTime(2002, 9, 9);
                DateTime endDate = new DateTime(2003, 9, 9);
                double basicSalary = 30000.00;
                double overtimePay = 300.00;
                double deductions = 200.00;

                // Act
                Payroll generatedPayroll = payrollService.GeneratePayroll(employeeId, startDate, endDate, basicSalary, overtimePay, deductions);

                // Assert
                Assert.IsNotNull(generatedPayroll);
                Assert.AreEqual(employeeId, generatedPayroll.EmployeeID);
                Assert.AreEqual(startDate, generatedPayroll.PayPeriodStartDate);
                Assert.AreEqual(endDate, generatedPayroll.PayPeriodEndDate);
                Assert.AreEqual(basicSalary, generatedPayroll.BasicSalary);
                Assert.AreEqual(overtimePay, generatedPayroll.OvertimePay);
                Assert.AreEqual(deductions, generatedPayroll.Deductions);
                Assert.AreEqual(basicSalary + overtimePay - deductions, generatedPayroll.NetSalary);
            }

        [Test]
        public void TestCalculateTax()
        {
            // Arrange
            TaxService taxService = new TaxService();
            taxService.connectionString = connectionString;

            int employeeId = 5; 
            int taxYear = 2024;
            double taxableIncome = 40000.00;

            // Act
            Tax calculatedTax = taxService.CalculateTax(employeeId, taxYear, taxableIncome);

            // Assert
            Assert.IsNotNull(calculatedTax);
            Assert.AreEqual(employeeId, calculatedTax.EmployeeID);
            Assert.AreEqual(taxYear, calculatedTax.TaxYear);
            Assert.AreEqual(taxableIncome, calculatedTax.TaxableIncome);

            
            double expectedTaxAmount = taxableIncome * 0.10;
            Assert.AreEqual(expectedTaxAmount, (double)calculatedTax.TaxAmount, 0.10); 
        }











    }
}