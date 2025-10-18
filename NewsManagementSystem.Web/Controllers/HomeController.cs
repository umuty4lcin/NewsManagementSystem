using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NewsManagementSystem.Service.Interfaces;
using NewsManagementSystem.Web.Models;

namespace NewsManagementSystem.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly INewsService _newsService;
    private readonly ICategoryService _categoryService;

    public HomeController(ILogger<HomeController> logger, INewsService newsService, ICategoryService categoryService)
    {
        _logger = logger;
        _newsService = newsService;
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        var news = await _newsService.GetPublishedNewsAsync();
        ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
        return View(news);
    }

    public async Task<IActionResult> Category(int id)
    {
        var news = await _newsService.GetNewsByCategoryAsync(id);
        var category = await _categoryService.GetCategoryByIdAsync(id);
        ViewBag.CategoryName = category?.Name ?? "Kategori";
        ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
        return View("Index", news);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
