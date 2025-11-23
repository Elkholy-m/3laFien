using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly Lazy<SocialAccountRepository> _socialAccountRepository;
        private readonly Lazy<VisitorRepository> _visitorRepository;
        private readonly RepositoryContext _context;

        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
            _socialAccountRepository = new Lazy<SocialAccountRepository>(() => new SocialAccountRepository(context));
            _visitorRepository = new Lazy<VisitorRepository>(() => new VisitorRepository(context));
        }

        public ISocialAccountRepository SocialAccount => _socialAccountRepository.Value;

        public IVisitorRepository Visitor => _visitorRepository.Value;

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
