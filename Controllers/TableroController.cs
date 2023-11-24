using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_julian_quin.ViewModels;

namespace tl2_tp10_2023_julian_quin.Controllers;

public class TableroController : Controller
{
    private readonly ILogger<TableroController> _logger;
    private ITableroRepository acceso ;
    

    public TableroController(ILogger<TableroController> logger)
    {
        _logger = logger;
        acceso = new TableroRepository();
    }
    [HttpGet]
    public IActionResult Index()
    {
        if(!SeLogueo()) return RedirectToRoute (new { controller = "Login", action = "Index" });
        else if(EsAdmin())return View(new IndexTableroViewModel(acceso.Tableros()));
        var idUsuario =  (int)HttpContext.Session.GetInt32("Id");
        var tableros = acceso.TablerosDeUnUsuario(idUsuario);
        return View(new IndexTableroViewModel(tableros));
    }

    [HttpGet]
    public IActionResult NuevoTablero()
    {
        if(!SeLogueo()) return RedirectToRoute (new { controller = "Home", action = "Index" });
        return View(new CrearTableroViewModel());
    }
    
    [HttpPost]
    public IActionResult NuevoTablero(CrearTableroViewModel tablero)
    {
        if(!SeLogueo()) return RedirectToRoute (new { controller = "Home", action = "Index" });
        var nuevotablero = new Tablero(tablero);
        acceso.NuevoTablero(nuevotablero);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult ModificarTablero(int idTablero)
    {
        if(!SeLogueo()) return RedirectToRoute (new { controller = "Home", action = "Index" });
        var tablero = acceso.TableroViaId(idTablero);
        var idSession = (int)HttpContext.Session.GetInt32("Id");
        var NivelAcceso = HttpContext.Session.GetString("NivelAcceso");
        if (NivelAcceso == Rol.administrador.ToString()) return View(new ModificarTableroViewModel(tablero));
        if(idSession == tablero.IdUsuarioPropietario)  return View(new ModificarTableroViewModel(tablero));
        return NotFound("ERROR 404 : El tablero no te pertenece");
    }

    [HttpPost]
    public IActionResult ModificarTablero(ModificarTableroViewModel tablero)
    {
        if(!SeLogueo()) return RedirectToRoute (new { controller = "Home", action = "Index" });
        var tableroModif = new Tablero(tablero);
        var idSession = (int)HttpContext.Session.GetInt32("Id");
        tableroModif.IdUsuarioPropietario = idSession;
        acceso.ModificarTablero(tableroModif,tableroModif.Id);
        return RedirectToAction("Index");
    }

    public IActionResult EliminarTablero(int idTablero)
    {
        if(!SeLogueo()) return RedirectToRoute (new { controller = "Home", action = "Index" });
        acceso.EliminarTablero(idTablero);
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
