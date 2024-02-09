using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_julian_quin.ViewModels;

namespace tl2_tp10_2023_julian_quin.Controllers;

public class UsuarioController : Controller
{
    private readonly ILogger<UsuarioController> _logger;
    private readonly IUsuarioRepository accesoUsuarios;

    public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        this.accesoUsuarios = usuarioRepository;
    }

    public IActionResult Index()
    { 
        if(!SeLogueo())return RedirectToRoute(new {controller = "Login", action = "Index" });
        if(!EsAdmin()) return View("MensajeAdvertencia");
        var usuarios = accesoUsuarios.Usuarios();
        var usuariosView = new IndexUsuarioViewModel(usuarios);
        return View(usuariosView);
    }

    [HttpGet]
    public IActionResult NuevoUsuario()
    {
        if(!SeLogueo())return RedirectToRoute(new {controller = "Login", action = "Index" });
        if(!EsAdmin())return View("MensajeAdvertencia");
        return View(new CrearUsuarioViewModel());
    }

    [HttpPost]
    public IActionResult NuevoUsuario(CrearUsuarioViewModel usuario)
    {
        if(!SeLogueo())return RedirectToRoute(new {controller = "Login", action = "Index" });
        if(!EsAdmin()) return View("MensajeAdvertencia");
        if(!ModelState.IsValid) return RedirectToAction("Index");
        var nuevoUsuario = new Usuario(usuario);
        accesoUsuarios.NuevoUsuario(nuevoUsuario);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult ModificarUsuario(int idUsuario)
    {
        if(!SeLogueo())return RedirectToRoute(new {controller = "Login", action = "Index" });
        if(!EsAdmin())return View("MensajeAdvertencia");
        var usuario = accesoUsuarios.UsuarioViaId(idUsuario);
        var usuarioViewModel = new ModificarUsuarioViewModel(usuario);
        return View(usuarioViewModel);
    }

    [HttpPost]
    public IActionResult ModificarUsuario(ModificarUsuarioViewModel usuarioUp)
    {
        if(!SeLogueo())return RedirectToRoute(new {controller = "Login", action = "Index" });
        if(!EsAdmin())return View("MensajeAdvertencia");
        if(!ModelState.IsValid) return RedirectToAction("Index");
        var usuario = new Usuario(usuarioUp);
        accesoUsuarios.ActualizarUsuario(usuario, usuario.Id);
        return RedirectToAction("Index");
    }

    public IActionResult EliminarUsuario(int idUsuario)
    {
        if(!SeLogueo())return RedirectToRoute(new {controller = "Login", action = "Index" });
        if(!EsAdmin())return View("MensajeAdvertencia");
        if(!EsAdmin())return RedirectToAction("Index");
        accesoUsuarios.EliminarUsuario(idUsuario);
        return RedirectToAction("Index");
    }

    private bool EsAdmin()
    {
        if (HttpContext.Session.GetString("NivelAcceso") == "administrador") return true;
        return false;
    }
    private bool SeLogueo()
    {
        if(HttpContext.Session.GetString("Usuario") != null && HttpContext.Session.GetInt32("Id") != null 
        && HttpContext.Session.GetString("NivelAcceso") != null) return true;

        return false; 
    }










    //Pasando un modelo a las vistas como parámetro,
    //Cuando se utiliza return View(usuario);, ASP.NET MVC busca automáticamente una vista que tenga el
    // mismo nombre que el método de acción actual (en este caso, ModificarUsuario). Por ejemplo,
    // si mi método está en un controlador llamado UsuariosController, ASP.NET MVC buscará una vista
    // llamada ModificarUsuario.cshtml en la carpeta Views/Usuarios. Luego se muestra por pantalla
}
