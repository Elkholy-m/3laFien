using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public record class PlaceImageDto(Guid ImageId, string ImageUrl, bool IsMain, Guid PlaceId);
}
