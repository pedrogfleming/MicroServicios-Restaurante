using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaWinniePod
{
    /// <summary>
    /// When the mapping is completed but with empty objects
    /// </summary>
    public class IncompletedMapperException : Exception
    {
        public IncompletedMapperException(string? message) : base(message)
        {
        }

        public IncompletedMapperException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
