using PayXpert.Model;
using PayXpert.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using PayXpert.Utility;

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

        public void GeneratePayroll(int employeeId, DateTime startDate, DateTime endDate)
        {
            try
            {
                
                Console.Write("Enter Basic Salary: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal basicSalary))
                {
                    
                    Console.Write("Enter Overtime Pay: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal overtimePay))
                    {
                       
                        Console.Write("Enter Deductions: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal deductions))
                        {
                            
                            decimal netSalary = basicSalary + overtimePay - deductions;

                           
                            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                            using (SqlCommand cmd = new SqlCommand("INSERT INTO Payroll (EmployeeID, PayPeriodStartDate, PayPeriodEndDate, BasicSalary, OvertimePay, Deductions, NetSalary) VALUES (@EmployeeId, @StartDate, @EndDate, @BasicSalary, @OvertimePay, @Deductions, @NetSalary)", sqlConnection))
                            {
                                cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                                cmd.Parameters.AddWithValue("@StartDate", startDate);
                                cmd.Parameters.AddWithValue("@EndDate", endDate);
                                cmd.Parameters.AddWithValue("@BasicSalary", basicSalary);
                                cmd.Parameters.AddWithValue("@OvertimePay", overtimePay);
                                cmd.Parameters.AddWithValue("@Deductions", deductions);
                                cmd.Parameters.AddWithValue("@NetSalary", netSalary);

                                sqlConnection.Open();
                                int rowsAffected = cmd.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    Console.WriteLine("Payroll generated successfully.");
                                }
                                else
                                {
                                    Console.WriteLine("Failed to generate payroll.");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input for Deductions. Please enter a valid numeric value.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input for Overtime Pay. Please enter a valid numeric value.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input for Basic Salary. Please enter a valid numeric value.");
                }
            }
            catch (Exception ex)
            {
               
                throw new PayrollGenerationException("Error generating payroll for employee.", ex);
            }
        }


        //public void GetEmployeePayrollDetails(int employeeId, DateTime startDate, DateTime endDate)
        //{
        //    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        //    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Payroll WHERE EmployeeID = @EmployeeId AND PayPeriodStartDate = @StartDate AND PayPeriodEndDate = @EndDate", sqlConnection))
        //    {
        //        cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
        //        cmd.Parameters.AddWithValue("@StartDate", startDate);
        //        cmd.Parameters.AddWithValue("@EndDate", endDate);

        //        try
        //        {
        //            sqlConnection.Open();
        //            SqlDataReader reader = cmd.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                Console.WriteLine($"Payroll ID: {reader["PayrollID"]}");
        //                Console.WriteLine($"Employee ID: {reader["EmployeeID"]}");
        //                Console.WriteLine($"Pay Period Start Date: {reader["PayPeriodStartDate"]}");
        //                Console.WriteLine($"Pay Period End Date: {reader["PayPeriodEndDate"]}");
        //                Console.WriteLine($"Basic Salary: {reader["BasicSalary"]}");
        //                Console.WriteLine($"Overtime Pay: {reader["OvertimePay"]}");
        //                Console.WriteLine($"Deductions: {reader["Deductions"]}");
        //                Console.WriteLine($"Net Salary: {reader["NetSalary"]}");
        //                Console.WriteLine();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine($"Error retrieving employee payroll details: {ex.Message}");
        //        }
        //    }
        //}
        //public void GeneratePayroll(int employeeId, DateTime startDate, DateTime endDate)
        //{

        //    try
        //    {
        //        // Get user input for BasicSalary
        //        Console.Write("Enter Basic Salary: ");
        //        if (decimal.TryParse(Console.ReadLine(), out decimal basicSalary))
        //        {
        //            // Get user input for OvertimePay
        //            Console.Write("Enter Overtime Pay: ");
        //            if (decimal.TryParse(Console.ReadLine(), out decimal overtimePay))
        //            {
        //                // Get user input for Deductions
        //                Console.Write("Enter Deductions: ");
        //                if (decimal.TryParse(Console.ReadLine(), out decimal deductions))
        //                {
        //                    // Calculate NetSalary using the provided formula
        //                    decimal netSalary = basicSalary + overtimePay - deductions;

        //                    // Adding the newly generated payroll entry to the 'payroll' list
        //                    payroll.Add(new Payroll
        //                    {
        //                        // PayrollID is excluded here, as it's an identity column
        //                        EmployeeID = employeeId,
        //                        PayPeriodStartDate = startDate,
        //                        PayPeriodEndDate = endDate,
        //                        BasicSalary = basicSalary,
        //                        OvertimePay = overtimePay,
        //                        Deductions = deductions,
        //                        NetSalary = netSalary
        //                    });
        //                }
        //                else
        //                {
        //                    Console.WriteLine("Invalid input for Deductions. Please enter a valid numeric value.");
        //                }
        //            }
        //            else
        //            {
        //                Console.WriteLine("Invalid input for Overtime Pay. Please enter a valid numeric value.");
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine("Invalid input for Basic Salary. Please enter a valid numeric value.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // If there's an issue with payroll generation, throw a PayrollGenerationException
        //        throw new PayrollGenerationException("Error generating payroll for employee.", ex);
        //    }
        //}



        //public void GetEmployeePayrollDetails(int employeeId, DateTime startDate, DateTime endDate)
        //{
        //    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        //    using (SqlCommand cmd = new SqlCommand("SELECT BasicSalary, OvertimePay, Deductions, NetSalary FROM Payroll WHERE EmployeeID = @EmployeeId AND PayPeriodStartDate >= @StartDate AND PayPeriodEndDate <= @EndDate", sqlConnection))
        //    {
        //        cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
        //        cmd.Parameters.AddWithValue("@StartDate", startDate);
        //        cmd.Parameters.AddWithValue("@EndDate", endDate);

        //        try
        //        {
        //            sqlConnection.Open();
        //            SqlDataReader reader = cmd.ExecuteReader();

        //            if (reader.HasRows)
        //            {
        //                Console.WriteLine($"Payroll details for Employee ID {employeeId} between {startDate.ToShortDateString()} and {endDate.ToShortDateString()}:");


        //                while (reader.Read())
        //                {
        //                    Console.WriteLine($"Basic Salary: {reader["BasicSalary"]}");
        //                    Console.WriteLine($"Overtime Pay: {reader["OvertimePay"]}");
        //                    Console.WriteLine($"Deductions: {reader["Deductions"]}");
        //                    Console.WriteLine($"Net Salary: {reader["NetSalary"]}");

        //                }
        //            }
        //            else
        //            {
        //                Console.WriteLine($"No payroll information found for Employee ID {employeeId} between {startDate.ToShortDateString()} and {endDate.ToShortDateString()}");
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine($"Error retrieving employee payroll details: {ex.Message}");
        //        }
        //    }
        //}



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
