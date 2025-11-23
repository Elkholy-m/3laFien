using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class VisitorNotFoundException : NotFoundException
    {
        public VisitorNotFoundException(Guid visitorId) : base($"Visitor with id: {visitorId} didn't exist in DB.") { }
    }
}
