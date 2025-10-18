using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsManagementSystem.Service.Interfaces;

namespace NewsManagementSystem.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly INewsService _newsService;
        private readonly ICategoryService _categoryService;

        public DashboardController(INewsService newsService, ICategoryService categoryService)
        {
            _newsService = newsService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.TotalNews = (await _newsService.GetAllNewsAsync()).Count();
            ViewBag.PublishedNews = (await _newsService.GetPublishedNewsAsync()).Count();
            ViewBag.TotalCategories = (await _categoryService.GetAllCategoriesAsync()).Count();
            
            var latestNews = await _newsService.GetLatestNewsAsync(5);
            return View(latestNews);
        }
    }
}

