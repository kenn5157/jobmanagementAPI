using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IConstants
    {
        public string getConnectionString();
        public string getSerilog();
    }
}