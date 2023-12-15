using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert.Exceptions
{
    internal class TaxCalculationException : Exception
    {
        public TaxCalculationException()
        {
        }

        public TaxCalculationException(string message) : base(message)
        {
        }

        public TaxCalculationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
