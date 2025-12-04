using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public abstract record class PlaceScheduleForManipulation
    {
        [EnumDataType(typeof(DayOfWeek), ErrorMessage = "Invalid day of week: [choose number from 0 to 6].")]
        public DayOfWeek WeekDay { get; set; }
        public TimeOnly OpenTime { get; set; }
        public TimeOnly ClosedTime { get; set; }
    }
}
