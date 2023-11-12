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
        var usuarios = acceso.Usuarios();
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

    [HttpGet]
    public IActionResult ModificarUsuario(int idUsuario)
    {
        var usuario = acceso.UsuarioViaId(idUsuario);
        return View(usuario); //Pasando un modelo a las vistas como parámetro,
        //Cuando se utiliza return View(usuario);, ASP.NET MVC busca automáticamente una vista que tenga el
        // mismo nombre que el método de acción actual (en este caso, ModificarUsuario). Por ejemplo,
        // si mi método está en un controlador llamado UsuariosController, ASP.NET MVC buscará una vista
        // llamada ModificarUsuario.cshtml en la carpeta Views/Usuarios. Luego se muestra por pantalla
    }

    [HttpPost]
    public IActionResult ModificarUsuario(Usuario usuarioUpdate)
    {
        acceso.ActualizarUsuario(usuarioUpdate,usuarioUpdate.Id);
        return RedirectToAction("Index");
    }

    public IActionResult EliminarUsuario(int idUsuario)
    {
        acceso.EliminarUsuario(idUsuario);
        return RedirectToAction("Index");
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
