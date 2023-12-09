using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_julian_quin.ViewModels;

namespace tl2_tp10_2023_julian_quin.Controllers;

public class TableroController : Controller
{
    private readonly ILogger<TableroController> _logger;
    private readonly ITableroRepository accesoTableros;
    private readonly IUsuarioRepository accesoUsuarios;

    public TableroController(ILogger<TableroController> logger, ITableroRepository tableroRepository, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        this.accesoTableros = tableroRepository;
        this.accesoUsuarios = usuarioRepository;
    }
    [HttpGet]
    public IActionResult Index()
    {
        if(!SeLogueo()) return RedirectToRoute (new { controller = "Login", action = "Index" });
        else if(EsAdmin())return View(new IndexTableroViewModel(accesoTableros.Tableros()));
        var idUsuario =  (int)HttpContext.Session.GetInt32("Id");
        var tableros = accesoTableros.TablerosDeUnUsuario(idUsuario);
        return View(new IndexTableroViewModel(tableros));
    }

    [HttpGet]
    public IActionResult NuevoTablero()
    {
        if(!SeLogueo()) return RedirectToRoute (new { controller = "Home", action = "Index" });
        if(!EsAdmin()) return RedirectToAction("Index");
        var nuevoTablero = new CrearTableroViewModel();
        nuevoTablero.usuarios = accesoUsuarios.Usuarios();
        return View(nuevoTablero);
    }
    
    [HttpPost]
    public IActionResult NuevoTablero(CrearTableroViewModel tablero)
    {
        if(!SeLogueo()) return RedirectToRoute (new { controller = "Home", action = "Index" });
        if(!EsAdmin()) return RedirectToAction("Index");
        if(!ModelState.IsValid) return RedirectToAction("Index");
        var nuevotablero = new Tablero(tablero);
        accesoTableros.NuevoTablero(nuevotablero);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult ModificarTablero(int idTablero)
    {
        if(!SeLogueo()) return RedirectToRoute (new { controller = "Home", action = "Index" });
        if(!EsAdmin()) return RedirectToAction("Index");
        var tablero = accesoTableros.TableroViaId(idTablero);
        var usuarios = accesoUsuarios.Usuarios();
        var tareaAmodificadar = new ModificarTableroViewModel(tablero);
        tareaAmodificadar.Usuarios = usuarios;
        return View(tareaAmodificadar);
    }

    [HttpPost]
    public IActionResult ModificarTablero(ModificarTableroViewModel tablero)
    {
        if(!SeLogueo()) return RedirectToRoute (new { controller = "Home", action = "Index" });
        if(!ModelState.IsValid) return RedirectToAction("Index");
        var tableroModificaciones = new Tablero(tablero);
        accesoTableros.ModificarTablero(tableroModificaciones,tableroModificaciones.Id);
        return RedirectToAction("Index");
    }
    public IActionResult EliminarTablero(int idTablero)
    {
        if(!SeLogueo()) return RedirectToRoute (new { controller = "Home", action = "Index" });
        if(!EsAdmin()) return RedirectToAction("Index");
        accesoTableros.EliminarTablero(idTablero);
        return RedirectToAction("Index");
    }
    private bool EsAdmin()
    {
        if (HttpContext.Session != null && HttpContext.Session.GetString("NivelAcceso") == "administrador") return true;
        return false;
    }  

    private bool SeLogueo()
    {
        if(HttpContext.Session.GetString("Usuario") != null && HttpContext.Session.GetInt32("Id") != null 
        && HttpContext.Session.GetString("NivelAcceso") != null) return true;
        return false; 
    }
 

}
