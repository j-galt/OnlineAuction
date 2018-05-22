using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineAuction.Core.Entities;
using OnlineAuction.Core.Interfaces;
using OnlineAuction.Web.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace OnlineAuction.Web.ViewComponents
{
    /// <summary>
    /// The category menu view component.
    /// </summary>
    /// <remarks>
    /// The view location: '~Views/Shared/Components/NavigationMenu/Default'.
    /// </remarks>
    public class NavigationMenuViewComponent : ViewComponent
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public NavigationMenuViewComponent(IRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public IViewComponentResult Invoke()
        {
            return View(new NavigationMenuViewModel
            {
                Categories = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryViewModel>>(
                    _categoryRepository
                    .GetAll()
                    .OrderBy(c => c.CategoryName)),
                CurrentCategory = RouteData?.Values["category"]
            });
        }
    }
}
