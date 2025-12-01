using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        ISocialAccountRepository SocialAccount { get; }
        IVisitorRepository Visitor { get; }
        IPlaceImageRepository PlaceImage { get; }
        IPlaceRepository Place { get; }
        ICategoryRepository Category { get; }
        Task SaveAsync();
    }
}
