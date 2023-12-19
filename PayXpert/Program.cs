

using Microsoft.VisualBasic;
using PayXpert.Exceptions;
using PayXpert.Model;
using PayXpert.Repository;
using System.Data.SqlClient;
using System.Globalization;
using System.Reflection;

while (true)
{

    Console.WriteLine("<<-----------------------------------------WELCOME TO PAYXPERT------------------------------------------->>\n");
    Console.WriteLine("Select an option:\n");
    Console.WriteLine("1. Employee");
    Console.WriteLine("2. Payroll");
    Console.WriteLine("3. Tax");
    Console.WriteLine("4. Financial record");

    int mainChoice = int.Parse(Console.ReadLine());

    switch (mainChoice)
    {
        case 1:
            Console.WriteLine("Employee options:");
            Console.WriteLine("1. Calculate age");
            Console.WriteLine("2. GetEmployeeById");
            Console.WriteLine("3. GetAllEmployees");
            Console.WriteLine("4. AddEmployee");
            Console.WriteLine("5. UpdateEmployee");
            Console.WriteLine("6. RemoveEmployee"); 

            int employeeChoice = int.Parse(Console.ReadLine());

            switch (employeeChoice)
            {
                case 1:
                    Console.WriteLine("Calculating age...");

                    CalculateAge();
                    break;
                case 2:
                    Console.WriteLine("Getting employee by ID...");

                    GetEmployeeById();
                    break;
                case 3:
                    Console.WriteLine("Getting all employees...");
                    GetAllEmployees();
                    break;
                case 4:
                    Console.WriteLine("Adding employee...");

                    AddEmployee();
                    break;
                case 5:
                    Console.WriteLine("Updating employee...");

                    UpdateEmployee();
                    break;
                case 6:
                    Console.WriteLine("Removing employee...");

                    RemoveEmployee();
                    break;
                default:
                    Console.WriteLine("Invalid option for Employee");
                    break;
            }
            break;

        case 2:
            Console.WriteLine("Payroll options:");
            Console.WriteLine("1. GeneratePayroll");
            Console.WriteLine("2. GetPayrollById");
            Console.WriteLine("3. GetPayrollsForEmployee");
            Console.WriteLine("4. GetPayrollsForPeriod");

            int payrollChoice = int.Parse(Console.ReadLine());

            switch (payrollChoice)
            {
                case 1:
                    Console.WriteLine("Generating payroll...");
                    GeneratePayroll();
                    break;
                case 2:
                    Console.WriteLine("Getting payroll by ID...");
                    GetPayrollById();
                    break;
                case 3:
                    Console.WriteLine("Getting payrolls for employee...");
                    GetPayrollsForEmployee();
                    break;
                case 4:
                    Console.WriteLine("Getting payrolls for period...");
                    GetPayrollsForPeriod();
                    break;
                default:
                    Console.WriteLine("Invalid option for Payroll");
                    break;
            }
            break;

        case 3:
            Console.WriteLine("Tax options:");
            Console.WriteLine("1. CalculateTax");
            Console.WriteLine("2. GetTaxById");
            Console.WriteLine("3. GetTaxesForEmployee");
            Console.WriteLine("4. GetTaxesForYear");

            int taxChoice = int.Parse(Console.ReadLine());

            switch (taxChoice)
            {
                case 1:
                    Console.WriteLine("Calculating tax...");
                    CalculateTax();
                    break;
                case 2:
                    Console.WriteLine("Getting tax by ID...");
                    GetTaxById();
                    break;
                case 3:
                    Console.WriteLine("Getting taxes for employee...");
                    GetTaxesForEmployee();
                    break;
                case 4:
                    Console.WriteLine("Getting taxes for year...");
                    GetTaxesForYear();
                    break;
                default:
                    Console.WriteLine("Invalid option for Tax");
                    break;
            }
            break;

        case 4:
            Console.WriteLine("Financial record options:");
            Console.WriteLine("1. AddFinancialRecord");
            Console.WriteLine("2. GetFinancialRecordById");
            Console.WriteLine("3. GetFinancialRecordsForEmployee");
            Console.WriteLine("4. GetFinancialRecordsForDate");

            int financialRecordChoice = int.Parse(Console.ReadLine());

            switch (financialRecordChoice)
            {
                case 1:
                    Console.WriteLine("Adding financial record...");
                    AddFinancialRecord();
                    break;
                case 2:
                    Console.WriteLine("Getting financial record by ID...");
                    GetFinancialRecordById();
                    break;
                case 3:
                    Console.WriteLine("Getting financial records for employee...");
                    GetFinancialRecordsForEmployee();
                    break;
                case 4:
                    Console.WriteLine("Getting financial records for date...");
                    GetFinancialRecordsForDate();
                    break;
                default:
                    Console.WriteLine("Invalid option for Financial Record");
                    break;
            }
            break;

        default:
            Console.WriteLine("Invalid option");
            break;
    }

}













#region  employee






////////////////// ADD EMPLOYEE

void AddEmployee()
{

    EmployeeService employeeService = new EmployeeService();


    Console.WriteLine("Enter Details for the New Employee:");

    Console.Write("Enter First Name: ");
    string firstName = Console.ReadLine();

    Console.Write("Enter Last Name: ");
    string lastName = Console.ReadLine();

    Console.Write("Enter DOB (YYYY-MM-DD): ");
    DateTime dateOfBirth;
    while (!DateTime.TryParse(Console.ReadLine(), out dateOfBirth))
    {
        Console.WriteLine("Invalid date format. Please enter a valid date (YYYY-MM-DD): ");
    }

    Console.Write("Enter Gender: ");
    string gender = Console.ReadLine();

    Console.Write("Enter Email: ");
    string email = Console.ReadLine();

    Console.Write("Enter Phone Number: ");
    string phoneNumber = Console.ReadLine();

    Console.Write("Enter Address: ");
    string address = Console.ReadLine();

    Console.Write("Enter Position: ");
    string position = Console.ReadLine();

    Console.Write("Enter Joining Date (YYYY-MM-DD): ");
    DateTime joiningDate;
    while (!DateTime.TryParse(Console.ReadLine(), out joiningDate))
    {
        Console.WriteLine("Invalid date format. Please enter a valid date (YYYY-MM-DD): ");
    }

    Console.Write("Enter Termination Date (optional, leave blank for no termination, YYYY-MM-DD): ");
    DateTime? terminationDate = null;
    string terminationDateInput = Console.ReadLine();
    if (!string.IsNullOrEmpty(terminationDateInput))
    {
        DateTime parsedTerminationDate;
        if (DateTime.TryParse(terminationDateInput, out parsedTerminationDate))
        {
            terminationDate = parsedTerminationDate;
        }
        else
        {
            Console.WriteLine("Invalid date format. Termination date will be set to null.");
        }
    }


    Employee newEmployee = new Employee
    {
        FirstName = firstName,
        LastName = lastName,
        DateOfBirth = dateOfBirth,
        Gender = gender,
        Email = email,
        PhoneNumber = phoneNumber,
        Address = address,
        Position = position,
        JoiningDate = joiningDate,
        TerminationDate = terminationDate
    };


    int addEmployeeStatus = employeeService.AddEmployee(newEmployee);

    if (addEmployeeStatus > 0)
    {
        Console.WriteLine("\nEmployee Added Successfully.");
    }
    else
    {
        Console.WriteLine("\nFailed to Add Employee.");
    }

}




////////////////     DELETE EMPLOYEE


void RemoveEmployee()
{
    EmployeeService employeeService = new EmployeeService();


    Console.WriteLine("Existing Employees:");
    DisplayEmployees(employeeService.GetAllEmployees());


    Console.Write("Enter the Employee ID to remove: ");
    if (int.TryParse(Console.ReadLine(), out int employeeIdToRemove))
    {

        int deleteEmployeeStatus = employeeService.RemoveEmployee(employeeIdToRemove);

        if (deleteEmployeeStatus > 0)
        {
            Console.WriteLine($"\nEmployee with ID {employeeIdToRemove} removed successfully.");
        }
        else
        {
            Console.WriteLine($"\nFailed to remove employee with ID. ID Not Found {employeeIdToRemove}.");
        }


        Console.WriteLine("\nUpdated Employees:");
        DisplayEmployees(employeeService.GetAllEmployees());
    }
    else
    {
        Console.WriteLine("Invalid input for Employee ID.");
    }


    static void DisplayEmployees(List<Employee> employees)
    {
        foreach (var employee in employees)
        {
            Console.WriteLine($"Employee ID: {employee.EmployeeID}, Name: {employee.FirstName} {employee.LastName}");
        }
    }

}



///////GET ALL EMPLOYEE

void GetAllEmployees()
{

    EmployeeService employeeService = new EmployeeService();


    Console.WriteLine("All Employees:");
    DisplayEmployees(employeeService.GetAllEmployees());

    static void DisplayEmployees(List<Employee> employees)
    {
        foreach (var employee in employees)
        {
            Console.WriteLine($"Employee ID: {employee.EmployeeID}, Name: {employee.FirstName} {employee.LastName}, Position: {employee.Position}");
        }
    }

}


////////////    GET EMPLOYEE BY ID

void GetEmployeeById()
{
    EmployeeService employeeService = new EmployeeService();


    Console.Write("Enter Employee ID: ");
    if (int.TryParse(Console.ReadLine(), out int employeeId))
    {

        Employee foundEmployee = employeeService.GetEmployeeById(employeeId);


        if (foundEmployee != null)
        {

            Console.WriteLine("Employee Found:");
            DisplayEmployeeDetails(foundEmployee);
        }
        else
        {
            Console.WriteLine($"No employee found with ID {employeeId}.");
        }
    }
    else
    {
        Console.WriteLine("Invalid input. Please enter a valid numeric Employee ID.");
    }


    static void DisplayEmployeeDetails(Employee employee)
    {
        Console.WriteLine($"Employee ID: {employee.EmployeeID}");
        Console.WriteLine($"Name: {employee.FirstName} {employee.LastName}");
        Console.WriteLine($"Date of Birth: {employee.DateOfBirth.ToShortDateString()}");
        Console.WriteLine($"Gender: {employee.Gender}");
        Console.WriteLine($"Email: {employee.Email}");
        Console.WriteLine($"Phone Number: {employee.PhoneNumber}");
        Console.WriteLine($"Address: {employee.Address}");
        Console.WriteLine($"Position: {employee.Position}");
        Console.WriteLine($"Joining Date: {employee.JoiningDate.ToShortDateString()}");
        Console.WriteLine($"Termination Date: {employee.TerminationDate?.ToShortDateString() ?? "N/A"}");
    }

}


/////////////     UPDATE EMPLOYEE

void UpdateEmployee()
{

    EmployeeService employeeService = new EmployeeService();


    Console.WriteLine("Existing Employees:");
    DisplayEmployees(employeeService.GetAllEmployees());


    Console.Write("Enter Employee ID to Update: ");
    if (int.TryParse(Console.ReadLine(), out int employeeIdToUpdate))
    {

        Employee existingEmployee = employeeService.GetEmployeeById(employeeIdToUpdate);

        if (existingEmployee != null)
        {
            Console.WriteLine($"\nUpdating Employee with ID {existingEmployee.EmployeeID}");


            Console.Write("Enter Updated Phone Number: ");
            string updatedPhoneNumber = Console.ReadLine();

            Console.Write("Enter Updated Position: ");
            string updatedPosition = Console.ReadLine();


            Employee updatedEmployee = new Employee
            {
                EmployeeID = existingEmployee.EmployeeID,
                FirstName = existingEmployee.FirstName,
                LastName = existingEmployee.LastName,
                DateOfBirth = existingEmployee.DateOfBirth,
                Gender = existingEmployee.Gender,
                Email = existingEmployee.Email,
                PhoneNumber = updatedPhoneNumber,
                Address = existingEmployee.Address,
                Position = updatedPosition,
                JoiningDate = existingEmployee.JoiningDate,
            };


            int updateEmployeeStatus = employeeService.UpdateEmployee(updatedEmployee);

            if (updateEmployeeStatus > 0)
            {
                Console.WriteLine("Employee Updated Successfully.");
            }
            else
            {
                Console.WriteLine("Failed to Update Employee.");
            }


            Console.WriteLine("\nUpdated Employees:");
            DisplayEmployees(employeeService.GetAllEmployees());
        }
        else
        {
            Console.WriteLine($"Employee with ID {employeeIdToUpdate} not found.");
        }
    }
    else
    {
        Console.WriteLine("Invalid input. Please enter a valid numeric Employee ID.");
    }


    static void DisplayEmployees(List<Employee> employees)
    {
        foreach (var employee in employees)
        {
            Console.WriteLine($"Employee ID: {employee.EmployeeID}, Name: {employee.FirstName} {employee.LastName},Phone Number: {employee.PhoneNumber},Position: {employee.Position},Email: {employee.Email}");
        }
    }

}
///////////////// CALCULATE AGE

void CalculateAge()
{
    EmployeeService employeeService = new EmployeeService();
    Console.Write("Enter Employee ID: ");
    if (int.TryParse(Console.ReadLine(), out int employeeId))
    {
        try
        {

            int age = employeeService.CalculateAge(employeeId);

            if (age > 0)
            {
                Console.WriteLine($"Employee Age: {age} years");
            }
            else
            {
                Console.WriteLine($"Employee with ID {employeeId} not found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
    else
    {
        Console.WriteLine("Invalid Employee ID. Please enter a valid integer.");
    }
}
#endregion


#region  PAYROLL


 


//////////////////// GET PAYROLL BY ID


void GetPayrollById()
{
    try
    {

        var payrollService = new PayrollService();


        Console.Write("Enter Payroll ID: ");
        if (int.TryParse(Console.ReadLine(), out int payrollIdToRetrieve))
        {

            Payroll payroll = payrollService.GetPayrollById(payrollIdToRetrieve);

            if (payroll != null)
            {

                Console.WriteLine($"Payroll Details for ID {payrollIdToRetrieve}:");
                Console.WriteLine($"Employee ID: {payroll.EmployeeID}");
                Console.WriteLine($"Pay Period Start Date: {payroll.PayPeriodStartDate}");
                Console.WriteLine($"Pay Period End Date: {payroll.PayPeriodEndDate}");
                Console.WriteLine($"Basic Salary: {payroll.BasicSalary}");
                Console.WriteLine($"Overtime Pay: {payroll.OvertimePay}");
                Console.WriteLine($"Deductions: {payroll.Deductions}");
                Console.WriteLine($"Net Salary: {payroll.NetSalary}");
            }
            else
            {
                Console.WriteLine($"No payroll found for ID {payrollIdToRetrieve}.");
            }
        }
        else
        {
            Console.WriteLine("Invalid Payroll ID format.");
        }
    }
    catch (PayrollGenerationException ex)
    {

        Console.WriteLine($"Error generating payroll: {ex.Message}");
    }
    catch (Exception ex)
    {

        Console.WriteLine($"An error occurred: {ex.Message}");
    }
}

////////// GET PAYROLL (EMPLOYEE ID ,START DATE, END DATE)

//void GetEmployeePayrollDetails()
//{
//    {
//        PayrollService payrollService = new PayrollService();


//        Console.Write("Enter Employee ID: ");
//        if (int.TryParse(Console.ReadLine(), out int employeeId))
//        {

//            Console.Write("Enter Start Date (yyyy-MM-dd): ");
//            if (DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
//            {

//                Console.Write("Enter End Date (yyyy-MM-dd): ");
//                if (DateTime.TryParse(Console.ReadLine(), out DateTime endDate))
//                {

//                    payrollService.GetEmployeePayrollDetails(employeeId, startDate, endDate);


//                }
//            }
//        }
//        Console.WriteLine("Invalid input. Please enter valid values.");
//    }
//}


//////////////////// GET PAYROLLS FOR EMPLYOEE(EMPLOYEE ID)


void GetPayrollsForEmployee()
{


    try
    {

        var payrollService = new PayrollService();


        Console.Write("Enter Employee ID: ");
        if (int.TryParse(Console.ReadLine(), out int employeeIdToRetrieve))
        {

            List<Payroll> payrollList = payrollService.GetPayrollsForEmployee(employeeIdToRetrieve);


            Console.WriteLine($"Payroll Details for Employee ID {employeeIdToRetrieve}:");

            foreach (var payroll in payrollList)
            {
                Console.WriteLine($"Payroll ID: {payroll.PayrollID}");
                Console.WriteLine($"Pay Period Start Date: {payroll.PayPeriodStartDate}");
                Console.WriteLine($"Pay Period End Date: {payroll.PayPeriodEndDate}");
                Console.WriteLine($"Basic Salary: {payroll.BasicSalary}");
                Console.WriteLine($"Overtime Pay: {payroll.OvertimePay}");
                Console.WriteLine($"Deductions: {payroll.Deductions}");
                Console.WriteLine($"Net Salary: {payroll.NetSalary}");
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("Invalid Employee ID format.");
        }
    }
    catch (EmployeeNotFoundException ex)
    {

        Console.WriteLine($"Employee not found: {ex.Message}");
    }
    catch (Exception ex)
    {

        Console.WriteLine($"An error occurred: {ex.Message}");
    }
}

////////////// get payrolls for period

void GetPayrollsForPeriod()
{

    try
    {

        var payrollService = new PayrollService();


        Console.Write("Enter Start Date (yyyy-MM-dd): ");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
        {
            Console.Write("Enter End Date (yyyy-MM-dd): ");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime endDate))
            {
                try
                {

                    List<Payroll> payrollList = payrollService.GetPayrollsForPeriod(startDate, endDate);

                    if (payrollList.Count > 0)
                    {

                        Console.WriteLine($"Payroll Details for the Period {startDate.ToString("yyyy-MM-dd")} to {endDate.ToString("yyyy-MM-dd")}:");

                        foreach (var payroll in payrollList)
                        {
                            Console.WriteLine($"Payroll ID: {payroll.PayrollID}");
                            Console.WriteLine($"Employee ID: {payroll.EmployeeID}");
                            Console.WriteLine($"Pay Period Start Date: {payroll.PayPeriodStartDate}");
                            Console.WriteLine($"Pay Period End Date: {payroll.PayPeriodEndDate}");
                            Console.WriteLine($"Basic Salary: {payroll.BasicSalary}");
                            Console.WriteLine($"Overtime Pay: {payroll.OvertimePay}");
                            Console.WriteLine($"Deductions: {payroll.Deductions}");
                            Console.WriteLine($"Net Salary: {payroll.NetSalary}");
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("No payroll found for the specified date range.");
                    }
                }
                catch (PayrollGenerationException ex)
                {
                    Console.WriteLine($"Payroll Generation Exception: {ex.Message}");

                }
            }
            else
            {
                Console.WriteLine("Invalid End Date format.");
            }
        }
        else
        {
            Console.WriteLine("Invalid Start Date format.");
        }
    }
    catch (Exception ex)
    {

        Console.WriteLine($"An error occurred: {ex.Message}");
    }
}
//void GeneratePayroll()
//{
//    PayrollService payrollService = new PayrollService();

//    Console.WriteLine("Enter Payroll Details:");

//    Console.Write("Enter Employee ID: ");
//    int employeeId;
//    while (!int.TryParse(Console.ReadLine(), out employeeId))
//    {
//        Console.WriteLine("Invalid input. Please enter a valid numeric Employee ID: ");
//    }

//    Console.Write("Enter Start Date (YYYY-MM-DD): ");
//    DateTime startDate;
//    while (!DateTime.TryParse(Console.ReadLine(), out startDate))
//    {
//        Console.WriteLine("Invalid date format. Please enter a valid date (YYYY-MM-DD): ");
//    }

//    Console.Write("Enter End Date (YYYY-MM-DD): ");
//    DateTime endDate;
//    while (!DateTime.TryParse(Console.ReadLine(), out endDate))
//    {
//        Console.WriteLine("Invalid date format. Please enter a valid date (YYYY-MM-DD): ");
//    }

//    Console.Write("Enter Basic Salary: ");
//    decimal basicSalary;
//    while (!decimal.TryParse(Console.ReadLine(), out basicSalary))
//    {
//        Console.WriteLine("Invalid input. Please enter a valid numeric Basic Salary: ");
//    }

//    Console.Write("Enter Overtime Pay: ");
//    decimal overtimePay;
//    while (!decimal.TryParse(Console.ReadLine(), out overtimePay))
//    {
//        Console.WriteLine("Invalid input. Please enter a valid numeric Overtime Pay: ");
//    }

//    Console.Write("Enter Deductions: ");
//    decimal deductions;
//    while (!decimal.TryParse(Console.ReadLine(), out deductions))
//    {
//        Console.WriteLine("Invalid input. Please enter a valid numeric Deductions: ");
//    }

//    try
//    {

//        payrollService.GeneratePayroll(new Payroll
//        {
//            EmployeeID = employeeId,
//            PayPeriodStartDate = startDate,
//            PayPeriodEndDate = endDate,
//            BasicSalary = basicSalary,
//            OvertimePay = overtimePay,
//            Deductions = deductions
//        });

//        Console.WriteLine("\nPayroll Generated Successfully.");
//    }
//    catch (PayrollGenerationException ex)
//    {
//        Console.WriteLine($"\nError generating payroll: {ex.Message}");
//    }
//}
void GeneratePayroll()
{
    PayrollService payrollService = new PayrollService();

    // Get input from the user or provide values directly
    Console.Write("Enter Employee ID: ");
    int employeeId = int.Parse(Console.ReadLine());

    Console.Write("Enter Start Date (YYYY-MM-DD): ");
    DateTime startDate = DateTime.Parse(Console.ReadLine());

    Console.Write("Enter End Date (YYYY-MM-DD): ");
    DateTime endDate = DateTime.Parse(Console.ReadLine());

    Console.Write("Enter Basic Salary: ");
    double basicSalary = double.Parse(Console.ReadLine());

    Console.Write("Enter Overtime Pay: ");
    double overtimePay = double.Parse(Console.ReadLine());

    Console.Write("Enter Deductions: ");
    double deductions = double.Parse(Console.ReadLine());


    Payroll generatedPayroll = payrollService.GeneratePayroll(employeeId, startDate, endDate, basicSalary, overtimePay, deductions);


    Console.WriteLine($"Payroll generated successfully. Payroll ID: {generatedPayroll.PayrollID}");
}
#endregion


#region TAX SERVICE

//////////   TAX SERCICE


///////////////// CALCULATE TAX


//void CalculateTax()
//{


//    TaxService taxService = new TaxService();
//    try
//    {

//        Console.Write("Enter Employee ID: ");
//        int employeeId;
//        while (!int.TryParse(Console.ReadLine(), out employeeId))
//        {
//            Console.WriteLine("Invalid input. Please enter a valid integer for Employee ID.");
//            Console.Write("Enter Employee ID: ");
//        }

//        Console.Write("Enter Tax Year: ");
//        int taxYear;
//        while (!int.TryParse(Console.ReadLine(), out taxYear))
//        {
//            Console.WriteLine("Invalid input. Please enter a valid integer for Tax Year.");
//            Console.Write("Enter Tax Year: ");
//        }


//        int taxAmount = taxService.CalculateTax(employeeId, taxYear);


//        Console.WriteLine($"Tax Amount for Employee ID {employeeId} in Tax Year {taxYear}: {taxAmount}");
//    }
//    catch (Exception ex)
//    {

//        Console.WriteLine($"An unexpected error occurred: {ex.Message}");
//    }
//}

/////////////  GET TAX BY ID(TAX ID)


void GetTaxById()
{

    ITaxService taxService = new TaxService();

    try
    {

        Console.Write("Enter Tax ID: ");
        int taxId;
        while (!int.TryParse(Console.ReadLine(), out taxId))
        {
            Console.WriteLine("Invalid input. Please enter a valid integer for Tax ID.");
        }


        Tax retrievedTax = taxService.GetTaxById(taxId);


        Console.WriteLine($"Tax ID: {retrievedTax.TaxID}, Tax Amount: {retrievedTax.TaxAmount}");
    }
    catch (TaxCalculationException ex)
    {
        Console.WriteLine($"Tax calculation error: {ex.Message}");

    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);

    }
}


/////// GET TAXES FOR EMPLOYEE(EMPLOYEE ID)

void GetTaxesForEmployee()
{
    ITaxService taxService = new TaxService();

    try
    {

        Console.Write("Enter Employee ID: ");
        int employeeId;
        while (!int.TryParse(Console.ReadLine(), out employeeId))
        {
            Console.WriteLine("Invalid input. Please enter a valid integer for Employee ID.");
        }


        List<Tax> taxesForEmployee = taxService.GetTaxesForEmployee(employeeId);

        if (taxesForEmployee.Count > 0)
        {

            foreach (Tax tax in taxesForEmployee)
            {
                Console.WriteLine($"Tax ID: {tax.TaxID}, Tax Amount: {tax.TaxAmount}");
            }
        }
        else
        {

            Console.WriteLine($"No taxes found for employee with ID {employeeId}.");
        }
    }
    catch (Exception ex)
    {

        Console.WriteLine(ex.Message);
    }

}

//////// GET TAX FOR YEAR (TAX YEAR)

void GetTaxesForYear()
{
    ITaxService taxService = new TaxService();

    try
    {

        Console.Write("Enter Tax Year: ");
        int taxYear;
        while (!int.TryParse(Console.ReadLine(), out taxYear))
        {
            Console.WriteLine("Invalid input. Please enter a valid integer for Tax Year.");
        }


        List<Tax> taxesForYear = taxService.GetTaxesForYear(taxYear);

        if (taxesForYear.Count > 0)
        {

            foreach (Tax tax in taxesForYear)
            {
                Console.WriteLine($"Tax ID: {tax.TaxID}, Tax Amount: {tax.TaxAmount}");
            }
        }
        else
        {

            Console.WriteLine($"No taxes found for the year {taxYear}.");
        }
    }
    catch (TaxCalculationException ex)
    {

        Console.WriteLine($"Error: {ex.Message}");
    }
    catch (Exception ex)
    {

        Console.WriteLine(ex.Message);
    }
}



void CalculateTax()
{
    try
    {
        TaxService taxService = new TaxService();
        // Example usage for CalculateTax
        Console.WriteLine("Enter Employee ID:");
        int employeeId = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter Tax Year:");
        int taxYear = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter Taxable Income:");
        double taxableIncome = double.Parse(Console.ReadLine());

        Tax calculatedTax = taxService.CalculateTax(employeeId, taxYear, taxableIncome);
        Console.WriteLine($"Calculated Tax Amount: {calculatedTax.TaxAmount}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }


}

#endregion



#region  FINANCIAL RECORD

/////  ADD FINANCIAL RECORD 


void AddFinancialRecord()
{
    FinancialRecordService financialService = new FinancialRecordService();

    try
    {

        Console.WriteLine("Enter Employee ID:");
        int employeeId = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Enter Description:");
        string description = Console.ReadLine();

        Console.WriteLine("Enter Amount:");
        decimal amount = Convert.ToDecimal(Console.ReadLine());

        Console.WriteLine("Enter Record Type:");
        string recordType = Console.ReadLine();


        int addFinancialRecordStatus = financialService.AddFinancialRecord(employeeId, description, amount, recordType);


        if (addFinancialRecordStatus > 0)
        {
            Console.WriteLine("Financial Record added successfully!");
        }
        else
        {
            Console.WriteLine("Failed to add Financial Record. Please check your input and try again.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
    }

    Console.ReadLine();
}


///////// GET FINANCIAL RECORD (RECORD ID)

void GetFinancialRecordById()
{


    try
    {
        FinancialRecordService financialService = new FinancialRecordService();

        Console.WriteLine("Enter Financial Record ID to Retrieve:");
        int recordId = Convert.ToInt32(Console.ReadLine());

        FinancialRecord retrievedRecord = financialService.GetFinancialRecordById(recordId);

        if (retrievedRecord != null)
        {
            Console.WriteLine("\nRetrieved Financial Record:");
            DisplayFinancialRecord(retrievedRecord);
        }
        else
        {
            Console.WriteLine($"Financial Record not found.");
        }
    }
    catch (FinancialRecordException ex)
    {
        Console.WriteLine($"Financial Record Exception: {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An unexpected error occurred: {ex.Message}");
    }

    static void DisplayFinancialRecord(FinancialRecord record)
    {
        Console.WriteLine($"Record ID: {record.RecordID}");
        Console.WriteLine($"Employee ID: {record.EmployeeID}");
        Console.WriteLine($"Record Date: {record.RecordDate}");
        Console.WriteLine($"Description: {record.Description}");
        Console.WriteLine($"Amount: {record.Amount}");
        Console.WriteLine($"Record Type: {record.RecordType}");
    }
}

//////// GET FINANCIAL RECORD FOR EMPLOYEE( EMPLOYEE ID)

void GetFinancialRecordsForEmployee()
{
    try
    {
        FinancialRecordService financialService = new FinancialRecordService();


        Console.Write("Enter Employee ID: ");
        int employeeId = Convert.ToInt32(Console.ReadLine());


        List<FinancialRecord> financialRecords = financialService.GetFinancialRecordsForEmployee(employeeId);


        Console.WriteLine("\nFinancial Records for Employee:");
        foreach (var record in financialRecords)
        {
            DisplayFinancialRecord(record);
        }
    }
    catch (FinancialRecordException ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Unexpected Error: {ex.Message}");
    }
    static void DisplayFinancialRecord(FinancialRecord record)
    {
        Console.WriteLine($"Record ID: {record.RecordID}");
        Console.WriteLine($"Employee ID: {record.EmployeeID}");
        Console.WriteLine($"Record Date: {record.RecordDate}");
        Console.WriteLine($"Description: {record.Description}");
        Console.WriteLine($"Amount: {record.Amount}");
        Console.WriteLine($"Record Type: {record.RecordType}");
        Console.WriteLine();
    }
}

////////// GET FINANCIAL RECORD FOR DATE (RECORD DATE)

void GetFinancialRecordsForDate()
{
    FinancialRecordService financialService = new FinancialRecordService();

    try
    {

        Console.WriteLine("Enter the date (yyyy-MM-dd) to retrieve financial records:");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime inputDate))
        {

            List<FinancialRecord> records = financialService.GetFinancialRecordsForDate(inputDate);


            if (records.Count > 0)
            {
                Console.WriteLine("\nFinancial Records for the specified date:");
                foreach (var record in records)
                {
                    DisplayFinancialRecord(record);
                }
            }
            else
            {
                Console.WriteLine("No financial records found for the specified date.");
            }
        }
        else
        {
            Console.WriteLine("Invalid date format. Please enter a valid date in the format yyyy-MM-dd.");
        }
    }
    catch (FinancialRecordException ex)
    {
        Console.WriteLine($"Error retrieving financial records: {ex.Message}");
    }



    static void DisplayFinancialRecord(FinancialRecord record)
    {
        Console.WriteLine($"Record ID: {record.RecordID}");
        Console.WriteLine($"Employee ID: {record.EmployeeID}");
        Console.WriteLine($"Record Date: {record.RecordDate}");
        Console.WriteLine($"Description: {record.Description}");
        Console.WriteLine($"Amount: {record.Amount}");
        Console.WriteLine($"Record Type: {record.RecordType}");
    }
}

#endregion






