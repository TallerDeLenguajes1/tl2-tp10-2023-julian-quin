using System.Data.SQLite;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_julian_quin.Models;
using tl2_tp10_2023_julian_quin.ViewModels;

namespace tl2_tp10_2023_julian_quin.Controllers;

public class TareaController : Controller
{
    private readonly ILogger<TareaController> _logger;
    private ITareasRepository acceso;
    private ITableroRepository acceso2;

    public TareaController(ILogger<TareaController> logger)
    {
        _logger = logger;
        acceso = new TareasRepository();
        acceso2 = new TableroRepository();
        
    }

    [HttpGet]
    public IActionResult Index (int idTablero)
    {
        if (!SeLogueo()) return RedirectToRoute(new { controller = "Home", action = "Index" });
        var idSession = (int)HttpContext.Session.GetInt32("Id");
        string nivelDeAcceso = HttpContext.Session.GetString("NivelAcceso");
        List<Tarea> tareas = new();
        if (nivelDeAcceso==Rol.administrador.ToString())
        {
           tareas = acceso.TareasDeUnTablero(idTablero); 
        } else 
        {
            tareas = acceso.TareasDeUnUsuario(idSession);
            tareas = tareas.FindAll(t => t.IdTablero == idTablero);
        }
        return View(new IndexTareaViewModel(tareas));
    }


    [HttpGet]
    public IActionResult NuevaTarea()
    {
        if (!SeLogueo()) return RedirectToRoute(new { controller = "Home", action = "Index" });
        return View(new CrearTareaViewModel());
    }


    [HttpPost]
    public IActionResult NuevaTarea(CrearTareaViewModel tarea)
    {
        if (!SeLogueo()) return RedirectToRoute(new { controller = "Home", action = "Index" });
        var tareaConvertida = new Tarea(tarea);
        acceso.CrearTarea(tarea.IdTablero,tareaConvertida);
        return View("Index",tarea.IdTablero);
    }

    

    [HttpGet]
    public IActionResult ModificarTarea(int idTarea)
    {
        if (!SeLogueo()) return RedirectToRoute(new { controller = "Home", action = "Index" });
        ModificarTareaViewModel tareaView;
        Tarea tarea; 
        if (!EsAdmin())
        {
            int idSession = (int)HttpContext.Session.GetInt32("Id");
            tarea = acceso.TareaId(idTarea);
            if (tarea.IdUsuarioAsignado == idSession) 
            { 
                tareaView = new ModificarTareaViewModel(tarea); 
                View(tareaView);

            } else return NotFound("Error 404, la tarea no te pertenece");
        }
        tarea = acceso.TareaId(idTarea);
        tareaView = new ModificarTareaViewModel(tarea); 
        return View(tareaView);
    }

    [HttpPost]
    public IActionResult ModificarTarea(ModificarTareaViewModel tarea)
    {
        if (!SeLogueo()) return RedirectToRoute(new { controller = "Home", action = "Index" });
        var tareaView = new Tarea(tarea);
        acceso.ModificarTarea(tarea.Id, tareaView);
        return View("Index",tarea.IdTablero);
    }

    public IActionResult EliminarTarea(int idTarea)
    {
        if (!SeLogueo()) return RedirectToRoute(new { controller = "Home", action = "Index" });
        acceso.EliminarTarea(idTarea);
        return Ok();
    }
    private bool EsAdmin()
    {
        if (HttpContext.Session != null && HttpContext.Session.GetString("NivelAcceso") == "administrador") return true;
        return false;
    }

    private bool SeLogueo()
    {
        if (HttpContext.Session.GetString("Usuario") != null && HttpContext.Session.GetInt32("Id") != null
        && HttpContext.Session.GetString("NivelAcceso") != null) return true;

        return false;
    }
}
