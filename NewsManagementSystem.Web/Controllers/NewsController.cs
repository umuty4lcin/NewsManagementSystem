using Microsoft.AspNetCore.Mvc;
using NewsManagementSystem.Service.Interfaces;

namespace NewsManagementSystem.Web.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        private readonly ICategoryService _categoryService;

        public NewsController(INewsService newsService, ICategoryService categoryService)
        {
            _newsService = newsService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Detail(int id)
        {
            var news = await _newsService.GetNewsByIdWithDetailsAsync(id);
            if (news == null || !news.IsPublished)
                return NotFound();

            // View count artır
            await _newsService.IncrementViewCountAsync(id);

            ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.LatestNews = await _newsService.GetLatestNewsAsync(5);
            
            return View(news);
        }
    }
}

