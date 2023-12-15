using Microsoft.VisualBasic;
using PayXpert.Exceptions;
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
    public class FinancialRecordService : IFinancialRecordService
    {
        public string connectionString;
        SqlCommand cmd = null;

        public string ConnectionString { get; set; }

        public FinancialRecordService()
        {
            connectionString = DbConnUtil.GetConnectionString();
            cmd = new SqlCommand();
        }

        public int AddFinancialRecord(int employeeId, string description, decimal amount, string recordType)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("INSERT INTO FinancialRecord (EmployeeId, Description, Amount, RecordType, RecordDate) " +
                                                   "VALUES (@EmployeeId, @Description, @Amount, @RecordType, @RecordDate)", sqlConnection))
            {
               
                cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                cmd.Parameters.AddWithValue("@Description", description);
                cmd.Parameters.AddWithValue("@Amount", amount);
                cmd.Parameters.AddWithValue("@RecordType", recordType);
                cmd.Parameters.AddWithValue("@RecordDate", DateTime.Now); 

                
                sqlConnection.Open();
                int addFinancialRecordStatus = cmd.ExecuteNonQuery();
                return addFinancialRecordStatus;
            }
        }

        public FinancialRecord GetFinancialRecordById(int recordId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM FinancialRecord WHERE RecordID = @RecordId", sqlConnection))
            {
               
                cmd.Parameters.AddWithValue("@RecordId", recordId);

                
                sqlConnection.Open();

                
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    
                    if (reader.Read())
                    {
                        
                        return new FinancialRecord
                        {
                            RecordID = Convert.ToInt32(reader["RecordID"]),
                            EmployeeID = Convert.ToInt32(reader["EmployeeId"]),
                            RecordDate = Convert.ToDateTime(reader["RecordDate"]),
                            Description = Convert.ToString(reader["Description"]),
                            Amount = Convert.ToDecimal(reader["Amount"]),
                            RecordType = Convert.ToString(reader["RecordType"])
                        };
                    }
                }

                
                return null;
            }
        }

        public List<FinancialRecord> GetFinancialRecordsForEmployee(int employeeId)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM FinancialRecord WHERE EmployeeId = @EmployeeId", sqlConnection))
                {
                    
                    cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                    
                    sqlConnection.Open();

           
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<FinancialRecord> records = new List<FinancialRecord>();

                       
                        while (reader.Read())
                        {
                            
                            FinancialRecord record = new FinancialRecord
                            {
                                RecordID = Convert.ToInt32(reader["RecordID"]),
                                EmployeeID = Convert.ToInt32(reader["EmployeeId"]),
                                RecordDate = Convert.ToDateTime(reader["RecordDate"]),
                                Description = Convert.ToString(reader["Description"]),
                                Amount = Convert.ToDecimal(reader["Amount"]),
                                RecordType = Convert.ToString(reader["RecordType"])
                            };

                            records.Add(record);
                        }

                        
                        if (records.Count == 0)
                        {
                            throw new FinancialRecordException($"No financial records found for Employee ID {employeeId}.");
                        }

                        return records;
                    }
                }
            }
            catch (Exception ex)
            {
                
                throw new FinancialRecordException($" {employeeId}: {ex.Message}", ex);
            }
        }

        public List<FinancialRecord> GetFinancialRecordsForDate(DateTime recordDate)
        {
            List<FinancialRecord> records = new List<FinancialRecord>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM FinancialRecord WHERE RecordDate = @RecordDate", sqlConnection))
            {
                
                cmd.Parameters.AddWithValue("@RecordDate", recordDate);

                
                sqlConnection.Open();

                
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    
                    if (reader.HasRows)
                    {
                        
                        while (reader.Read())
                        {
                            records.Add(new FinancialRecord
                            {
                                RecordID = Convert.ToInt32(reader["RecordID"]),
                                EmployeeID = Convert.ToInt32(reader["EmployeeId"]),
                                RecordDate = Convert.ToDateTime(reader["RecordDate"]),
                                Description = Convert.ToString(reader["Description"]),
                                Amount = Convert.ToDecimal(reader["Amount"]),
                                RecordType = Convert.ToString(reader["RecordType"])
                            });
                        }
                    }
                }
            }

            return records;
        }



    }

}

