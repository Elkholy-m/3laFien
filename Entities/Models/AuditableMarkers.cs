using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public interface ISoftDelete
    {
        public bool IsDeleted { get; set; }
        public DateTime DeletedAt { get ; set; }
    }

    public interface IHasCreatedAt
    {
        public DateTime CreatedAt { get; set; }
    }

    public interface IHasJoinedAt
    {
        public DateTime JoinedAt { get; set; }
    }
    public interface IHasAddedAt
    {
        public DateTime AddedAt { get; set; }
    }
}
