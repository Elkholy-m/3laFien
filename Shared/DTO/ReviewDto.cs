using Entities.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public record class ReviewDto(Guid ReviewId, Ratings Rating, string Comment, Guid VisitorId, Guid PlaceId, DateTime CreatedAt);
}
