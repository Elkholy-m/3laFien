using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class FavouritePlaceNotFoundException : NotFoundException
    {
        public FavouritePlaceNotFoundException(Guid visitorId, Guid placeId) :
            base($"There is no favourite place for visitor with ID: {visitorId} to the place with ID: {placeId}")
        {
            
        }
    }
}
