using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewsManagementSystem.Core.Entities;
using NewsManagementSystem.Service.Interfaces;
using NewsManagementSystem.Web.Areas.Admin.Models;

namespace NewsManagementSystem.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        private readonly ICategoryService _categoryService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public NewsController(INewsService newsService, ICategoryService categoryService, 
            UserManager<AppUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _newsService = newsService;
            _categoryService = categoryService;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var news = await _newsService.GetAllNewsAsync();
            return View(news);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewsViewModel model, IFormFile? imageFile)
        {
            // Görsel kontrolü
            if (imageFile == null || imageFile.Length == 0)
            {
                
                ModelState.AddModelError("imageFile", "Haber görseli zorunludur. Lütfen bir görsel seçiniz.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
                return View(model);
            }

            try
            {
                var user = await _userManager.GetUserAsync(User);
                
                if (user == null)
                {
                    ModelState.AddModelError("", "Kullanıcı bulunamadı. Lütfen tekrar giriş yapın.");
                    ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
                    return View(model);
                }
                
                var news = new News
                {
                    Title = model.Title,
                    Summary = model.Summary,
                    Content = model.Content,
                    Author = model.Author,
                    CategoryId = model.CategoryId,
                    UserId = user.Id,
                    ImageUrl = await UploadImage(imageFile!), // imageFile null olamaz çünkü yukarıda kontrol ettik
                    IsPublished = false,
                    ViewCount = 0
                };

                await _newsService.AddNewsAsync(news);
                TempData["Success"] = "Haber başarıyla eklendi.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException != null ? " - " + ex.InnerException.Message : "";
                ModelState.AddModelError("", "Haber eklenirken bir hata oluştu: " + ex.Message + innerMessage);
                ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var news = await _newsService.GetNewsByIdAsync(id);
            if (news == null)
                return NotFound();

            var model = new NewsViewModel
            {
                Id = news.Id,
                Title = news.Title,
                Summary = news.Summary,
                Content = news.Content,
                Author = news.Author,
                CategoryId = news.CategoryId,
                CurrentImageUrl = news.ImageUrl
            };

            ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(NewsViewModel model, IFormFile? imageFile)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                TempData["Error"] = "Form doğrulama hatası: " + string.Join(", ", errors);
                ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
                return View(model);
            }

            try
            {
                var news = await _newsService.GetNewsByIdAsync(model.Id);
                if (news == null)
                    return NotFound();

                news.Title = model.Title;
                news.Summary = model.Summary;
                news.Content = model.Content;
                news.Author = model.Author;
                news.CategoryId = model.CategoryId;

                // Yeni görsel yüklendiyse güncelle
                if (imageFile != null && imageFile.Length > 0)
                {
                    news.ImageUrl = await UploadImage(imageFile);
                }

                await _newsService.UpdateNewsAsync(news);
                TempData["Success"] = "Haber başarıyla güncellendi.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException != null ? " - " + ex.InnerException.Message : "";
                ModelState.AddModelError("", "Haber güncellenirken bir hata oluştu: " + ex.Message + innerMessage);
                ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _newsService.DeleteNewsAsync(id);
            TempData["Success"] = "Haber başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Publish(int id)
        {
            await _newsService.PublishNewsAsync(id);
            TempData["Success"] = "Haber yayınlandı.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Unpublish(int id)
        {
            await _newsService.UnpublishNewsAsync(id);
            TempData["Success"] = "Haber yayından kaldırıldı.";
            return RedirectToAction(nameof(Index));
        }

        private async Task<string> UploadImage(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
                throw new ArgumentException("Görsel dosyası gereklidir.");

            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "news");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return "/uploads/news/" + uniqueFileName;
        }
    }
}

