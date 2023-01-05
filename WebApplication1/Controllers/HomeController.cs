using Core.Entities;
using DataAccess.Contexts;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;
    public HomeController(AppDbContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {
        HttpContext.Session.SetString("name","Nazrin");
        Response.Cookies.Append("surname","Gulmammadova");
        HomeViewModel homeVM = new()
        {
            SlideItems= _context.SlideItems,
            ShippingItems=_context.ShippingItems
        };
        return View(homeVM);
    }
    public IActionResult Test()
    {
        var session= HttpContext.Session.GetString("name");
        var cookie = Request.Cookies["surname"];
        return Json(session+" "+cookie);
    }
}