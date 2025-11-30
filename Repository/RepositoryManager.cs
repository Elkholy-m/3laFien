using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly Lazy<SocialAccountRepository> _socialAccountRepository;
        private readonly Lazy<VisitorRepository> _visitorRepository;
        private readonly Lazy<PlaceImageRepository> _placeImageRepository;
        private readonly RepositoryContext _context;

        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
            _socialAccountRepository = new Lazy<SocialAccountRepository>(() => new SocialAccountRepository(context));
            _visitorRepository = new Lazy<VisitorRepository>(() => new VisitorRepository(context));
            _placeImageRepository = new Lazy<PlaceImageRepository>(() => new PlaceImageRepository(context));
        }

        public ISocialAccountRepository SocialAccount => _socialAccountRepository.Value;

        public IVisitorRepository Visitor => _visitorRepository.Value;

        public IPlaceImageRepository PlaceImage => _placeImageRepository.Value;

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
