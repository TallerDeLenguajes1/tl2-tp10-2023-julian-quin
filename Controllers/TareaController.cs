using System.Data.SQLite;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_julian_quin.Models;

namespace tl2_tp10_2023_julian_quin.Controllers;

public class TareaController : Controller
{
    private readonly ILogger<TareaController> _logger;
    private ITareasRepository acceso;

    public TareaController(ILogger<TareaController> logger)
    {
        _logger = logger;
        acceso = new TareasRepository();
    }
    public IActionResult Index()
    {
        return View(acceso.TareasDeUnTablero(1)); //(Por el momento asuma que el tablero al que pertenece la tarea es siempre la misma, y que no posee usuario asignado)
    }

    [HttpGet]
    public IActionResult NuevaTarea()
    {
        return View(new Tarea());
    }
    
    
    [HttpPost]
    public IActionResult NuevaTarea(Tarea tarea)
    {
        acceso.CrearTarea(1,tarea);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult ModificarTarea(int idTarea)
    {
        return View(acceso.TareaId(idTarea));
    }

    [HttpPost]
    public IActionResult ModificarTarea(Tarea tarea)
    {
        acceso.ModificarTarea(tarea.Id,tarea);
        return RedirectToAction("Index");
    }

    public IActionResult EliminarTarea(int idTarea)
    {
        acceso.EliminarTarea(idTarea);
        return RedirectToAction("Index");
    }


    // Creo que estan bien, but segun lo que interpreto de la consigna no se pidieron.

    // [HttpGet]
    // public IActionResult TareasDeUnUsuario(int idUsuario)
    // {
    //     var lista = acceso.TareasDeUnUsuario(2);
    //     return View("Index",lista);
    // }

    // [HttpGet]
    // public IActionResult TareasDeUnTablero(int idTablero)
    // {
    //     var lista = acceso.TareasDeUnTablero(4);
    //     return View("Index",lista);
    // }
}
