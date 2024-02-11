using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_julian_quin.ViewModels;

namespace tl2_tp10_2023_julian_quin.Controllers;

public class TareaController : Controller
{
    private readonly ILogger<TareaController> _logger;
    private readonly ITareasRepository _accesoTareas;
    private readonly ITableroRepository _accesoTableros;

    public TareaController(ILogger<TareaController> logger, ITareasRepository tareasRepository, ITableroRepository tableroRepository)
    {
        _logger = logger;
        _accesoTareas = tareasRepository;
        _accesoTableros = tableroRepository;

    }

    [HttpGet]
    public IActionResult Index(int idTablero)
    {
        try
        {
            if (!SeLogueo()) return RedirectToRoute(new { controller = "Home", action = "Index" });
            var idSession = (int)HttpContext.Session.GetInt32("Id");
            List<Tarea> tareas;
            var idPropTableroPath = _accesoTableros.TableroViaId(idTablero).IdUsuarioPropietario;
            tareas = _accesoTareas.TareasDeUnTablero(idTablero);
            if (idSession == idPropTableroPath)return View(new IndexTareaViewModel(tareas));
            var tareasView= new IndexTareaViewModel(tareas);
            ViewBag.idSession = idSession;
            return View("Index2",tareasView);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }

    }

    [HttpGet]
    public IActionResult NuevaTarea()
    {
        try
        {
            if (!SeLogueo()) return RedirectToRoute(new { controller = "Home", action = "Index" });
            var usuarioId = (int)HttpContext.Session.GetInt32("Id");
            var nuevaTarea = new CrearTareaViewModel();
            if (EsAdmin())
            {
                nuevaTarea.Tableros = _accesoTableros.Tableros();
                return View(nuevaTarea);
            }
            else
            {
                nuevaTarea.Tableros = _accesoTableros.TablerosDeUnUsuario(usuarioId);
                return View(nuevaTarea);
            }

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest(); 
        }

    }


    [HttpPost]
    public IActionResult NuevaTarea(CrearTareaViewModel tarea)
    {
        try
        {
            if (!SeLogueo()) return RedirectToRoute(new { controller = "Home", action = "Index" });
            if (!ModelState.IsValid) return RedirectToAction("Index");
            var tareaConvertida = new Tarea(tarea);
            _accesoTareas.CrearTarea(tarea.IdTablero, tareaConvertida);
            return RedirectToAction("Index", new { idTablero = tarea.IdTablero });

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }

    [HttpGet]
    public IActionResult ModificarTarea(int idTarea)
    {
        try
        {
            if (!SeLogueo()) return RedirectToRoute(new { controller = "Home", action = "Index" });
            var tarea = _accesoTareas.TareaId(idTarea);
            var usuarioId = (int)HttpContext.Session.GetInt32("Id");
            if (EsAdmin())
            {
                var tareaAmodificadar = new ModificarTareaViewModel(tarea);
                tareaAmodificadar.Tableros = _accesoTableros.Tableros();
                return View(tareaAmodificadar);
            }
            else
            {
                var tareaAmodificadar = new ModificarTareaViewModel(tarea);
                tareaAmodificadar.Tableros = _accesoTableros.TablerosDeUnUsuario(usuarioId);
                return View(tareaAmodificadar);
            }

        }
        catch (Exception ex)
        {
            _logger.LogError("Error al intentar modificar " + ex.ToString());
            return BadRequest();
        }
    }

    [HttpPost]
    public IActionResult ModificarTarea(ModificarTareaViewModel tarea)
    {
        try
        {
            if (!SeLogueo()) return RedirectToRoute(new { controller = "Home", action = "Index" });
            if (!ModelState.IsValid) return RedirectToAction("Index");
            var tareaRecuperada = new Tarea(tarea);
            _accesoTareas.ModificarTarea(tarea.Id, tareaRecuperada);
            return RedirectToAction("Index", new { idTablero = tarea.IdTablero });

        }
        catch (Exception ex)
        {
            _logger.LogError("Error al intentar modificar " + ex.ToString());
            return BadRequest();
        }
    }

    public IActionResult EliminarTarea(int idTarea)
    {
        try
        {
            if (!SeLogueo()) return RedirectToRoute(new { controller = "Home", action = "Index" });
            var tarea = _accesoTareas.TareaId(idTarea);
            var tablero = _accesoTableros.TableroViaId(tarea.IdTablero);
            _accesoTareas.EliminarTarea(idTarea);
            return RedirectToAction("Index", new { idTablero = tarea.IdTablero });

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }

    }

    [HttpPost]
    public IActionResult CambiarEstadoTarea(int idTarea, EstadoTarea estado)
    {
        try
        {
            if (!SeLogueo()) return RedirectToRoute(new { controller = "Home", action = "Index" });
            var tarea = _accesoTareas.TareaId(idTarea);
            tarea.Estado = estado;
            _accesoTareas.ModificarTarea(tarea.Id,tarea);
            return RedirectToAction("Index" , new { idTablero = tarea.IdTablero});
        }
        catch (Exception Ex)
        {
            _logger.LogError(Ex.ToString());
            return BadRequest(); 
        }
    }

    [HttpGet]
    public IActionResult TareasAsignadas()
    {
        if (!SeLogueo()) return RedirectToRoute(new { controller = "Home", action = "Index" });
        var usuarioId = (int)HttpContext.Session.GetInt32("Id");
        var tareas = _accesoTareas.TareasDeUnUsuario(usuarioId);
        var tareasView = new IndexTareaViewModel(tareas);
        ViewBag.idSession = usuarioId;
        return View("Index2",tareasView);
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
