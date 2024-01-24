using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class NoPreviousDayDataException : Exception
    {
        public NoPreviousDayDataException(string message) : base(message) { }
    }
}
