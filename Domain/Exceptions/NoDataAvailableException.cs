using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class NoDataAvailableException : Exception
    {
        public NoDataAvailableException(string message) : base(message) { }
    }
}
