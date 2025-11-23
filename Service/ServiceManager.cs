using AutoMapper;
using Contracts;
using EmailService;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using NETCore.MailKit.Core;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<VisitorService> _visitorService;
        private readonly Lazy<SocialAccountService> _socialAccountService;
        public ServiceManager(EmailConfiguration emailConfig, UserManager<User> userManager, IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
        {
            _visitorService = new Lazy<VisitorService>(() => new VisitorService(repositoryManager, mapper, userManager));
            _socialAccountService = new Lazy<SocialAccountService>(() => new SocialAccountService(repositoryManager, mapper));

        }
        public IVisitorService VisitorService => _visitorService.Value;

        public ISocialAccountService socialAccountService => _socialAccountService.Value;
    }
}
