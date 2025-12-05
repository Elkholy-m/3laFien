using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class PlaceAlreadyFavouritedConflictException : ConflictException
    {
        public PlaceAlreadyFavouritedConflictException() :
            base($"This place is already in the visitor's favorites.") { }
    }
}
