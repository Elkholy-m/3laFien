using Entities.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class PlaceSchedule
    {
        [Key]
        public Guid ScheduleId { get; set; }
        public DayOfWeek WeekDay { get; set; }
        public TimeOnly OpenTime {  get; set; }
        public TimeOnly ClosedTime { get; set; }
        public bool IsClosed { get; set; }

        [ForeignKey(nameof(Place))]
        public Guid PlaceId { get; set; }

        // Navigational Properties
        public Place? Place { get; set; }
    }
}
