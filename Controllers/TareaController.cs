using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_julian_quin.ViewModels;

namespace tl2_tp10_2023_julian_quin.Controllers;

public class TareaController : Controller
{
    private readonly ILogger<TareaController> _logger;
    private readonly ITareasRepository _accesoTareas;
    private readonly ITableroRepository _accesoTableros;
    private readonly IUsuarioRepository _usuariosRepository;

    public TareaController(ILogger<TareaController> logger, ITareasRepository tareasRepository, ITableroRepository tableroRepository, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        _accesoTareas = tareasRepository;
        _accesoTableros = tableroRepository;
        _usuariosRepository = usuarioRepository;
    }

    [HttpGet]
    public IActionResult TareasTablerosPropios(int idTablero) //llega id de tableros propios
    {
        try
        {
            if (!SeLogueo()) return RedirectToRoute(new { controller = "Home", action = "Index" });
            List<Tarea> tareas;
            tareas = _accesoTareas.TareasDeUnTablero(idTablero);
            return View(new IndexTareaViewModel(tareas));

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return View("Error");
        }

    }

    [HttpGet]
    public IActionResult TareasTablerosNoPropios(int idTablero) //llega id de tableros no propios
    {
        try
        {
            if (!SeLogueo()) return RedirectToRoute(new { controller = "Home", action = "Index" });
            List<Tarea> tareas;
            var idSession = IdSesion();
            tareas = _accesoTareas.TareasDeUnTablero(idTablero);
            ViewBag.idSession = idSession;
            var tareasView = new IndexTareaViewModel(tareas);
            return View(tareasView);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return View("Error");
        }

    }

    [HttpGet]
    public IActionResult NuevaTarea()//con opcion select de tableros
    {
        try
        {
            if (!SeLogueo()) return RedirectToRoute(new { controller = "Home", action = "Index" });
            var usuarioId = IdSesion();
            var nuevaTarea = new CrearTareaViewModel();
            nuevaTarea.Tableros = _accesoTableros.TablerosDeUnUsuario(usuarioId);
            nuevaTarea.Usuarios = _usuariosRepository.Usuarios();
            return View(nuevaTarea);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return View("Error");
        }

    }

    [HttpGet]
    public IActionResult NuevaTareaDesdeUnTablero(int IdTablero) //sin opcion select de tableros
    {
        try
        {
            if (!SeLogueo()) return RedirectToRoute(new { controller = "Home", action = "Index" });
            var usuarioId = IdSesion();
            var nuevaTarea = new CrearTareaViewModel();
            nuevaTarea.Tableros = _accesoTableros.TablerosDeUnUsuario(usuarioId);
            nuevaTarea.Usuarios = _usuariosRepository.Usuarios();
            nuevaTarea.IdTablero = IdTablero;
            return View(nuevaTarea);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return View("Error");
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
            var tareas = _accesoTareas.Tareas();
            TempData["idTarea"] = tareas.FirstOrDefault(t => t.Nombre == tarea.Nombre).Id;
            TempData["Mensaje"] = "Nueva";
            return RedirectToAction("TareasTablerosPropios", new { idTablero = tarea.IdTablero });

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return View("Error");
        }
    }

    [HttpGet]
    public IActionResult ModificarTarea(int idTarea)
    {
        try
        {
            if (!SeLogueo()) return RedirectToRoute(new { controller = "Home", action = "Index" });
            var tarea = _accesoTareas.TareaId(idTarea);
            var tareaAmodificadar = new ModificarTareaViewModel(tarea);
            tareaAmodificadar.Usuarios = _usuariosRepository.Usuarios();
            return View(tareaAmodificadar);

        }
        catch (Exception ex)
        {
            _logger.LogError("Error al intentar modificar " + ex.ToString());
            return View("Error");
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
            TempData["idTarea"] = tarea.Id;
            TempData["Mensaje"] = "Actualizada";
            return RedirectToAction("TareasTablerosPropios", new { idTablero = tarea.IdTablero });

        }
        catch (Exception ex)
        {
            _logger.LogError("Error al intentar modificar " + ex.ToString());
            return View("Error");
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
            return RedirectToAction("TareasTablerosPropios", new { idTablero = tarea.IdTablero });

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return View("Error");
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
            _accesoTareas.ModificarTarea(tarea.Id, tarea);
            TempData["EstadoTarea"] = "¡ Estado Actualizado !";
            return RedirectToAction("TareasTablerosNoPropios", new { idTablero = tarea.IdTablero });
        }
        catch (Exception Ex)
        {
            _logger.LogError(Ex.ToString());
            return View("Error");
        }
    }
    public IActionResult CambiarEstadoTareaNav(int idTarea, EstadoTarea estado)
    {
        try
        {
            if (!SeLogueo()) return RedirectToRoute(new { controller = "Home", action = "Index" });
            var tarea = _accesoTareas.TareaId(idTarea);
            tarea.Estado = estado;
            _accesoTareas.ModificarTarea(tarea.Id, tarea);
            TempData["Mensaje"] = "Actualizada";
            TempData["idTarea"] = tarea.Id;
            return RedirectToAction("TareasAsignadas");
        }
        catch (Exception Ex)
        {
            _logger.LogError(Ex.ToString());
            return View("Error");
        }
    }

    [HttpGet]
    public IActionResult TareasAsignadas()
    {
        try
        {
            if (!SeLogueo()) return RedirectToRoute(new { controller = "Login", action = "Index" });
            int usuarioId = IdSesion();
            var tareas = _accesoTareas.TareasDeUnUsuario(usuarioId);
            var tareasView = new IndexTareaViewModel(tareas);
            ViewBag.idSession = usuarioId;
            return View("TareasAsignadas", tareasView);

        }
        catch (Exception Ex)
        {
            _logger.LogError(Ex.ToString());
            return View("Error");
        }

    }
    [HttpPost]
    public IActionResult ListarTareaPorEstado(EstadoTarea estado)
    {
        try
        {
            if (!SeLogueo()) return RedirectToRoute(new { controller = "Login", action = "Index" });
            int usuarioId = IdSesion();
            var tareas = _accesoTareas.TareasViaEstado(usuarioId, estado);
            var tareasVM = new IndexTareaViewModel(tareas);
            return View("TareasAsignadas", tareasVM);

        }
        catch (Exception Ex)
        {
            _logger.LogError(Ex.ToString());
            return View("Error");
        }

    }

    private int IdSesion()
    {
        return (int)HttpContext.Session.GetInt32("Id");
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
