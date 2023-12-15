using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert.Exceptions
{
    internal class FinancialRecordException:Exception
    {
        public FinancialRecordException() { }

        public FinancialRecordException(string message) : base(message) { }

        public FinancialRecordException(string message, Exception innerException) : base(message, innerException) { }
    }
}
