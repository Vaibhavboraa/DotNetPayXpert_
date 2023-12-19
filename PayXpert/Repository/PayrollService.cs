using PayXpert.Model;
using PayXpert.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using PayXpert.Utility;
using System.Data;

namespace PayXpert.Repository
{
    public class PayrollService : IPayrollService 
    {
        public string connectionString;
        SqlCommand cmd = null;
        public PayrollService()
        {
            connectionString = DbConnUtil.GetConnectionString();
            cmd = new SqlCommand();
        }
        public List<Payroll> payroll;
       

       
        public PayrollService(string connectionString)
        {
            this.connectionString = connectionString;
          
        }

        public Payroll GeneratePayroll(int employeeId, DateTime startDate, DateTime endDate, double basicSalary, double overtimePay, double deductions)
        {
            Payroll payroll = new Payroll();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    cmd.CommandText = "INSERT INTO Payroll (EmployeeID, PayPeriodStartDate, PayPeriodEndDate, BasicSalary, OvertimePay, Deductions, NetSalary) " +
                                      "VALUES (@EmployeeID, @StartDate, @EndDate, @BasicSalary, @OvertimePay, @Deductions, @NetSalary); " +
                                      "SELECT SCOPE_IDENTITY();";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                    cmd.Parameters.AddWithValue("@EndDate", endDate);

                    double netSalary = basicSalary + overtimePay - deductions;

                    cmd.Parameters.AddWithValue("@BasicSalary", basicSalary);
                    cmd.Parameters.AddWithValue("@OvertimePay", overtimePay);
                    cmd.Parameters.AddWithValue("@Deductions", deductions);
                    cmd.Parameters.AddWithValue("@NetSalary", netSalary);

                    cmd.Connection = sqlConnection;
                    sqlConnection.Open();

                    object payrollId = cmd.ExecuteScalar();

                    if (payrollId != null)
                    {
                        payroll = GetPayrollById(Convert.ToInt32(payrollId));
                    }
                    else
                    {
                        throw new PayrollGenerationException("Error: Payroll ID not retrieved after insertion.");
                    }
                }
            }
            catch (PayrollGenerationException ex)
            {
                Console.WriteLine($"Payroll generation error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return payroll;
        }




        public Payroll GetPayrollById(int payrollId)
        {
            Payroll foundPayroll = null;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Payroll WHERE PayrollID = @PayrollId", sqlConnection))
            {
                cmd.Parameters.AddWithValue("@PayrollId", payrollId);

                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    foundPayroll = new Payroll
                    {
                        PayrollID = (int)reader["PayrollID"],
                        EmployeeID = (int)reader["EmployeeID"],
                        PayPeriodStartDate = (DateTime)reader["PayPeriodStartDate"],
                        PayPeriodEndDate = (DateTime)reader["PayPeriodEndDate"],
                        BasicSalary = (decimal)reader["BasicSalary"],
                        OvertimePay = (decimal)reader["OvertimePay"],
                        Deductions = (decimal)reader["Deductions"],
                        NetSalary = (decimal)reader["NetSalary"]
                    };
                }
            }

            return foundPayroll;
        }





        public List<Payroll> GetPayrollsForEmployee(int employeeId)
        {
            List<Payroll> payrolls = new List<Payroll>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Payroll WHERE EmployeeID = @EmployeeId", sqlConnection))
            {
                cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Payroll payroll = new Payroll
                    {
                        PayrollID = (int)reader["PayrollID"],
                        EmployeeID = (int)reader["EmployeeID"],
                        PayPeriodStartDate = (DateTime)reader["PayPeriodStartDate"],
                        PayPeriodEndDate = (DateTime)reader["PayPeriodEndDate"],
                        BasicSalary = (decimal)reader["BasicSalary"],
                        OvertimePay = (decimal)reader["OvertimePay"],
                        Deductions = (decimal)reader["Deductions"],
                        NetSalary = (decimal)reader["NetSalary"]
                    };

                    payrolls.Add(payroll);
                }

                if (payrolls.Count == 0)
                {
                    throw new EmployeeNotFoundException($"Employee with ID {employeeId} not found.");
                }
            }

            return payrolls;
        }

        public List<Payroll> GetPayrollsForPeriod(DateTime startDate, DateTime endDate)
        {
            List<Payroll> payrolls = new List<Payroll>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Payroll WHERE PayPeriodStartDate >= @StartDate AND PayPeriodEndDate <= @EndDate", sqlConnection))
            {
                cmd.Parameters.AddWithValue("@StartDate", startDate);
                cmd.Parameters.AddWithValue("@EndDate", endDate);

                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Payroll payroll = new Payroll
                    {
                        PayrollID = (int)reader["PayrollID"],
                        EmployeeID = (int)reader["EmployeeID"],
                        PayPeriodStartDate = (DateTime)reader["PayPeriodStartDate"],
                        PayPeriodEndDate = (DateTime)reader["PayPeriodEndDate"],
                        BasicSalary = (decimal)reader["BasicSalary"],
                        OvertimePay = (decimal)reader["OvertimePay"],
                        Deductions = (decimal)reader["Deductions"],
                        NetSalary = (decimal)reader["NetSalary"]
                    };

                    payrolls.Add(payroll);
                }
            }

            return payrolls;
        }





















        

    }
}
