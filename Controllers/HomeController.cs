using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_julian_quin.Models;
using tl2_tp10_2023_julian_quin.ViewModels;

namespace tl2_tp10_2023_julian_quin.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
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
    public IActionResult LoginHome()
    {
        var logedUser = GetlogedUser();
        return View(logedUser);
    }
    private LogedUserViewModel GetlogedUser()
    {
        var logged = new LogedUserViewModel();
        if (HttpContext.Session.GetString("Usuario") == null){
            logged.IsLoged = false;
            logged.Nombre  =  "";            
            logged.NivelAcceso  = "";               
        }else
        {
            logged.IsLoged = true;
            logged.Nombre  =  HttpContext.Session.GetString("Usuario") ;            
            logged.NivelAcceso  =  HttpContext.Session.GetString("NivelAcceso");             
        }
        return logged;
    }
}
