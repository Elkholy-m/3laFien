using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public abstract class ConflictException : Exception
    {
        public ConflictException(string msg) : base(msg) { }
    }
}
