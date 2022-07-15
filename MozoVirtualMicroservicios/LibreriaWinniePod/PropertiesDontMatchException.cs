using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaWinniePod
{
    /// <summary>
    /// When the types and the names of the properties not match
    /// </summary>
    internal class PropertiesDontMatchException : Exception
    {
        public PropertiesDontMatchException(string? message) : base(message)
        {
        }

        public PropertiesDontMatchException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
