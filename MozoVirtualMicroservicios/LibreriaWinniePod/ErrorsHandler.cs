using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaWinniePod
{
    public class ErrorsHandler
    {
        public List<string> Errors { get; set; } = new();
        public bool HasErrors() => this.Errors?.Any() ?? false;

        public dynamic ToError()
        {
            return new { Errors = this.Errors };
        }
    }
}
