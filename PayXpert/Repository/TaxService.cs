using PayXpert.Model;
using PayXpert.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert.Repository
{
    public class TaxService : ITaxService 
    {

        public string connectionString;
        SqlCommand cmd = null;
        public string ConnectionString { get; set; }
        public TaxService()
        {
            connectionString = DbConnUtil.GetConnectionString();
            cmd = new SqlCommand();
        }
        public TaxService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Tax GetTaxById(int taxId)
        {
            Tax foundTax = null;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Tax WHERE TaxID = @TaxId", sqlConnection))
            {
                cmd.Parameters.AddWithValue("@TaxId", taxId);

                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    foundTax = new Tax
                    {
                        TaxID = (int)reader["TaxID"],
                        EmployeeID = (int)reader["EmployeeID"],
                        TaxYear = (int)reader["TaxYear"],
                        TaxableIncome = (decimal)reader["TaxableIncome"],
                        TaxAmount = (decimal)reader["TaxAmount"]
                    };
                }
            }

            
            if (foundTax == null)
            {
                throw new Exception($"Tax with ID {taxId} not found.");
            }

            return foundTax;
        }

        public List<Tax> GetTaxesForEmployee(int employeeId)
        {
            List<Tax> taxesForEmployee = new List<Tax>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Tax WHERE EmployeeID = @EmployeeId", sqlConnection))
            {
                cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Tax tax = new Tax
                    {
                        TaxID = (int)reader["TaxID"],
                        EmployeeID = (int)reader["EmployeeID"],
                        TaxYear = (int)reader["TaxYear"],
                        TaxableIncome = (decimal)reader["TaxableIncome"],
                        TaxAmount = (decimal)reader["TaxAmount"]
                    };

                    taxesForEmployee.Add(tax);
                }
            }

           
            if (taxesForEmployee.Count == 0)
            {
               
                throw new Exception($"No taxes found for employee with ID {employeeId}.");
            }

            return taxesForEmployee;
        }

        public List<Tax> GetTaxesForYear(int taxYear)
        {
            List<Tax> taxesForYear = new List<Tax>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Tax WHERE TaxYear = @TaxYear", sqlConnection))
            {
                cmd.Parameters.AddWithValue("@TaxYear", taxYear);

                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Tax tax = new Tax
                    {
                        TaxID = (int)reader["TaxID"],
                        EmployeeID = (int)reader["EmployeeID"],
                        TaxYear = (int)reader["TaxYear"],
                        TaxableIncome = (decimal)reader["TaxableIncome"],
                        TaxAmount = (decimal)reader["TaxAmount"]
                    };

                    taxesForYear.Add(tax);
                }
            }

            
            if (taxesForYear.Count == 0)
            {
                
                throw new Exception($"No taxes found for the year {taxYear}.");
            }

            return taxesForYear;
        }
        //public int CalculateTax(int employeeId, int taxYear)
        //{
            
        //    decimal taxableIncome = GetTaxableIncome(employeeId);

            
        //    decimal taxAmount = taxableIncome * 0.10m;

            
        //    return (int)taxAmount;
        //}

        //public decimal GetTaxableIncome(int employeeId)
        //{
        //    decimal taxableIncome = 0;


        //    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        //    using (SqlCommand cmd = new SqlCommand("SELECT TaxableIncome FROM TAX WHERE EmployeeID = @EmployeeId", sqlConnection))
        //    {
        //        cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

        //        sqlConnection.Open();
        //        SqlDataReader reader = cmd.ExecuteReader();

        //        if (reader.Read())
        //        {
        //            taxableIncome = (decimal)reader["TaxableIncome"];
        //        }
        //    }

        //    return taxableIncome;
        //}
        public int CalculateTax(int employeeId, int taxYear)
        {
            decimal taxableIncome = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT TaxableIncome FROM TAX WHERE EmployeeID = @EmployeeId", sqlConnection))
            {
                cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    taxableIncome = (decimal)reader["TaxableIncome"];
                }
            }

            decimal taxAmount = taxableIncome * 0.10m;

            return (int)taxAmount;
        }



    }



}
