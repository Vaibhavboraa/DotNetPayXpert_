using PayXpert.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert.Repository
{
    internal interface ITaxService
    {

        public Tax GetTaxById(int taxId);
        public List<Tax> GetTaxesForEmployee(int employeeId);
        public List<Tax> GetTaxesForYear(int taxYear);
        public int CalculateTax(int employeeId, int taxYear);
       
    }
}
