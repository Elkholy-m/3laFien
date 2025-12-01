using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class PlaceNotFoundException : NotFoundException
    {
        public PlaceNotFoundException(Guid placeId) : base($"Place with id: {placeId} didn't exist in the db")
        {

        }
    }
}
