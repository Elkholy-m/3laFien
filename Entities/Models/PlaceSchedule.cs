using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class PlaceSchedule
    {
        [Key]
        public Guid ScheduleId { get; set; }
        public DayOfWeek WeekDay { get; set; }
        public TimeSpan OpenTime { get; set; }
        public TimeSpan ClosedTime { get; set; }

        [ForeignKey(nameof(Place))]
        public Guid PlaceId { get; set; }

        // Navigational Properties
        public Place? Place { get; set; }
    }
}
