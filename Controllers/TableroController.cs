using Microsoft.AspNetCore.Mvc;

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
    public IActionResult Index ()
    {
        return View(acceso.Tableros()); 
    }

    [HttpGet]

    public IActionResult NuevoTablero()
    {
        return View(new Tablero());
    }
    
    [HttpPost]
    public IActionResult NuevoTablero(Tablero tablero)
    {
        acceso.NuevoTablero(tablero);
        return RedirectToAction("Index");
    }
    [HttpGet]
    public IActionResult ModificarTablero(int idTablero)
    {
        var tablero = acceso.TableroViaId(idTablero);
        return View(tablero);
    }

    [HttpPost]
    public IActionResult ModificarTablero(Tablero tablero)
    {
        acceso.ModificarTablero(tablero,tablero.Id);
        return RedirectToAction("Index");
    }

    public IActionResult EliminarTablero(int idTablero)
    {
        acceso.EliminarTablero(idTablero);
        return RedirectToAction("Index");
    }



   
}
