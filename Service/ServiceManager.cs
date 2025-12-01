using AutoMapper;
using Contracts;
using EmailService;
using Entities.Models;
using Microsoft.AspNetCore.Hosting;
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
        private readonly Lazy<ImageService> _imageService;
        private readonly Lazy<PlaceImageService> _placeImageService;
        private readonly Lazy<CategoryService> _categoryService;
        private readonly Lazy<PlaceService> _placeService;
        public ServiceManager(EmailConfiguration emailConfig, UserManager<User> userManager, IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper, IWebHostEnvironment webHost)
        {
            _visitorService = new Lazy<VisitorService>(() => new VisitorService(repositoryManager, mapper, userManager, webHost));
            _socialAccountService = new Lazy<SocialAccountService>(() => new SocialAccountService(repositoryManager, mapper));
            _imageService = new Lazy<ImageService>(() => new ImageService(webHost));
            _placeImageService = new Lazy<PlaceImageService>(() => new PlaceImageService(repositoryManager, mapper));
            _categoryService = new Lazy<CategoryService>(() => new CategoryService(repositoryManager, mapper));
            _placeService = new Lazy<PlaceService>(() => new PlaceService(repositoryManager, mapper));
        }
        public IVisitorService VisitorService => _visitorService.Value;

        public ISocialAccountService SocialAccountService => _socialAccountService.Value;

        public IImageService ImageService => _imageService.Value;

        public IPlaceImageService PlaceImageService => _placeImageService.Value;

        public ICategoryService CategoryService => _categoryService.Value;
        public IPlaceService PlaceService => _placeService.Value;
    }
}
