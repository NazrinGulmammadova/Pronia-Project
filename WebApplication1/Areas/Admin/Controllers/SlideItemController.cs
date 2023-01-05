using Core.Entities;
using DataAccess.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Areas.Admin.ViewModels.Slider;
using WebApplication1.Utilities;

namespace WebApplication1.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "Admin")]
public class SlideItemController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;
    private int _count;

    public SlideItemController(AppDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
        _count = _context.SlideItems.Count();
    }

    public IActionResult Index()
    {
        ViewBag.Count = _count;
        return View(_context.SlideItems);
    }

    public async Task<IActionResult> Detail(int id)
    {
        var model = await _context.SlideItems.FindAsync(id);
        if (model == null) return NotFound();
        return View(model);
    }

    public IActionResult Create(int id)
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SlideCreateVM item)
    {
        if (!ModelState.IsValid) return View(item);
        if (item.Photo == null)
        {
            ModelState.AddModelError("Photo", "Please, select the photo");
            return View(item);
        }
        if (!item.Photo.CheckFileSize(100))
        {
            ModelState.AddModelError("Photo", "The Photo size must be less than 100 kbyte");
            return View(item);
        }
        if (!item.Photo.CheckFileFormat("image/"))
        {
            ModelState.AddModelError("Photo", "Please, choose image file");
            return View(item);
        }
        var fileName = string.Empty;
        try
        {
            fileName = await item.Photo.CopyFileAsync(_env.WebRootPath, "assets", "images", "website-images");
        }
        catch (Exception)
        {

            return View(item);
        }

        SlideItem slide = new()
        {
            Title = item.Title,
            Offer = item.Offer,
            Description = item.Description,
            Photo = fileName
        };
        await _context.AddAsync(slide);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        if (_count == 1) return BadRequest();
        var slide = await _context.SlideItems.FindAsync(id);
        if (slide == null) return NotFound();
        return View(slide);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    [ActionName("Delete")]
    public async Task<IActionResult> DeletePost(int id)
    {
        if (_count == 1) return BadRequest();
        var slide = await _context.SlideItems.FindAsync(id);
        if (slide == null) return NotFound();
        Helper.DeleteFile(_env.WebRootPath, "assets", "images", "website-images", slide.Photo);
        _context.SlideItems.Remove(slide);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Update(int id, SlideItem slide)
    {
        var model = await _context.SlideItems.FindAsync(id);
        if (model == null) return NotFound();
        return View(model);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    [ActionName("Update")]
    public async Task<IActionResult> UpdatePost(int id, SlideItem item)
    {
        if(!ModelState.IsValid) return View(item);
        var model=_context.SlideItems.Find(id);
        if(model == null) return NotFound();
        model.Photo = item.Photo;
        model.Title = item.Title;
        model.Description = item.Description;
        model.Offer=item.Offer;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
 