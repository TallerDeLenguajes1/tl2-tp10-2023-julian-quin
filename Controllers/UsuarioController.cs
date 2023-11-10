using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_julian_quin.Models;

namespace tl2_tp10_2023_julian_quin.Controllers;

public class UsuarioController : Controller
{
    private readonly ILogger<UsuarioController> _logger;
    private IUsuarioRepository acceso ;

    public UsuarioController(ILogger<UsuarioController> logger)
    {
        _logger = logger;
        acceso = new UsuarioRepositorio();

    }

    public IActionResult Index()
    {
        var usuarios = acceso.GetUsser();
        return View(usuarios);
    }
    [HttpGet]
    public IActionResult NuevoUsuario()
    {   
       return View(new Usuario());
    }
    [HttpPost]
    public IActionResult NuevoUsuario(Usuario usuario)
    {   
        acceso.NuevoUsuario(usuario); 
        return RedirectToAction("Index");
    }





    // public IActionResult Privacy()
    // {
    //     return View();
    // }

    // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    // public IActionResult Error()
    // {
    //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    // }
}
