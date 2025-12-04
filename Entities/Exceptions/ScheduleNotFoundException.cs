using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class ScheduleNotFoundException : NotFoundException
    {
        public ScheduleNotFoundException(Guid scheduleId) : base($"Schedule with ID: {scheduleId} didn't exist in db.") { }
    }
}
