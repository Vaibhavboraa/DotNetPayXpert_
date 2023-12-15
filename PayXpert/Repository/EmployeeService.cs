using PayXpert.Exceptions;
using PayXpert.Model;
using PayXpert.Utility;
using System.Data;
using System.Data.SqlClient;
using System.Text;


namespace PayXpert.Repository
{
    public class EmployeeService : IEmployeeSevice
    {
        public string connectionString;
        SqlCommand cmd = null;
        public EmployeeService()
        {
            connectionString = DbConnUtil.GetConnectionString();
            cmd = new SqlCommand();
        }



        public int CalculateAge(int employeeId)
        {
            int age = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT DATEDIFF(YEAR, DateOfBirth, GETDATE()) AS Age FROM Employee WHERE EmployeeID = @EmployeeId", sqlConnection))
            {
                cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    age = (int)reader["Age"];
                }
            }

            return age;
        }


        public int AddEmployee(Employee employee)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("INSERT INTO Employee (FirstName, LastName, DateOfBirth, Gender, Email, PhoneNumber, Address, Position, JoiningDate, TerminationDate) " +
                                                   "VALUES (@FirstName, @LastName, @DateOfBirth, @Gender, @Email, @PhoneNumber, @Address, @Position, @JoiningDate, @TerminationDate)", sqlConnection))
            {
                
                cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                cmd.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
                cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                cmd.Parameters.AddWithValue("@Email", employee.Email);
                cmd.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
                cmd.Parameters.AddWithValue("@Address", employee.Address);
                cmd.Parameters.AddWithValue("@Position", employee.Position);
                cmd.Parameters.AddWithValue("@JoiningDate", employee.JoiningDate);

               
                SqlParameter terminationDateParameter = new SqlParameter("@TerminationDate", SqlDbType.DateTime);
                terminationDateParameter.Value = (object)employee.TerminationDate ?? DBNull.Value;
                cmd.Parameters.Add(terminationDateParameter);

               
                sqlConnection.Open();
                int addEmployeeStatus = cmd.ExecuteNonQuery();
                return addEmployeeStatus;
            }
        }

        public int RemoveEmployee(int employeeId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("DELETE FROM Employee WHERE EmployeeID = @EmployeeId", sqlConnection))
            {
                cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                sqlConnection.Open();
                int deleteEmployeeStatus = cmd.ExecuteNonQuery();
                return deleteEmployeeStatus;
            }
        }
        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Employee", sqlConnection))
                {
                    sqlConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Employee employee = new Employee();
                        employee.EmployeeID = (int)reader["EmployeeID"];
                        employee.FirstName = (string)reader["FirstName"];
                        employee.LastName = (string)reader["LastName"];
                        employee.DateOfBirth = (DateTime)reader["DateOfBirth"];
                        employee.Gender = (string)reader["Gender"];
                        employee.Email = (string)reader["Email"];
                        employee.PhoneNumber = (string)reader["PhoneNumber"];
                        employee.Address = (string)reader["Address"];
                        employee.Position = (string)reader["Position"];
                        employee.JoiningDate = (DateTime)reader["JoiningDate"];

                        
                        if (reader["TerminationDate"] != DBNull.Value)
                        {
                            employee.TerminationDate = (DateTime)reader["TerminationDate"];
                        }

                        employees.Add(employee);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return employees;
        }
       
        public Employee GetEmployeeById(int employeeId)
        {
            Employee foundEmployee = null;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Employee WHERE EmployeeID = @EmployeeId", sqlConnection))
            {
                cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    foundEmployee = new Employee
                    {
                        EmployeeID = (int)reader["EmployeeID"],
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"],
                        DateOfBirth = (DateTime)reader["DateOfBirth"],
                        Gender = (string)reader["Gender"],
                        Email = (string)reader["Email"],
                        PhoneNumber = (string)reader["PhoneNumber"],
                        Address = (string)reader["Address"],
                        Position = (string)reader["Position"],
                        JoiningDate = (DateTime)reader["JoiningDate"],
                        TerminationDate = reader["TerminationDate"] == DBNull.Value ? (DateTime?)null : (DateTime)reader["TerminationDate"]
                    };
                }
            }

            return foundEmployee;
        }
        public int UpdateEmployee(Employee updatedEmployee)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("UPDATE Employee SET FirstName = @FirstName, LastName = @LastName, " +
                                                   "DateOfBirth = @DateOfBirth, Gender = @Gender, Email = @Email, " +
                                                   "PhoneNumber = @PhoneNumber, Address = @Address, " +
                                                   "Position = @Position, JoiningDate = @JoiningDate " +
                                                   "WHERE EmployeeID = @EmployeeId", sqlConnection))
            {
                cmd.Parameters.AddWithValue("@EmployeeId", updatedEmployee.EmployeeID);
                cmd.Parameters.AddWithValue("@FirstName", updatedEmployee.FirstName);
                cmd.Parameters.AddWithValue("@LastName", updatedEmployee.LastName);
                cmd.Parameters.AddWithValue("@DateOfBirth", updatedEmployee.DateOfBirth);
                cmd.Parameters.AddWithValue("@Gender", updatedEmployee.Gender);
                cmd.Parameters.AddWithValue("@Email", updatedEmployee.Email);
                cmd.Parameters.AddWithValue("@PhoneNumber", updatedEmployee.PhoneNumber);
                cmd.Parameters.AddWithValue("@Address", updatedEmployee.Address);
                cmd.Parameters.AddWithValue("@Position", updatedEmployee.Position);
                cmd.Parameters.AddWithValue("@JoiningDate", updatedEmployee.JoiningDate);

                sqlConnection.Open();
                int updateEmployeeStatus = cmd.ExecuteNonQuery();
                return updateEmployeeStatus;
            }
        }



    }
}






