using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayXpert.Exceptions
{
    internal class PayrollGenerationException : Exception
    {
        public PayrollGenerationException() : base()
        {
        }

        public PayrollGenerationException(string message) : base(message)
        {
        }

        public PayrollGenerationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
