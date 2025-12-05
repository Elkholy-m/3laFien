using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class ScheduleAlreadyExistConflictException : ConflictException
    {
        public ScheduleAlreadyExistConflictException() : base("This schedule is already in the place Schedule (UPDATE IT).")
        {
            
        }
    }
}
