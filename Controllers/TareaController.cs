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
    public IActionResult TareasTablerosPropios(int idTablero)
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
            return BadRequest();
        }

    }
    
    [HttpGet]
    public IActionResult TareasTablerosNoPropios(int idTablero)
    {
        List<Tarea> tareas;
        var idSession = IdSesion();
        tareas = _accesoTareas.TareasDeUnTablero(idTablero);
        ViewBag.idSession = idSession;
        var tareasView= new IndexTareaViewModel(tareas);
        return View(tareasView);
    }

    [HttpGet]
    public IActionResult NuevaTarea()
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
            return BadRequest(); 
        }

    }

    [HttpGet]
    public IActionResult NuevaTareaTableroExplicito(int IdTablero)
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
            return RedirectToAction("TareasTablerosPropios", new { idTablero = tarea.IdTablero });

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
            var tareaAmodificadar = new ModificarTareaViewModel(tarea);
            tareaAmodificadar.Usuarios = _usuariosRepository.Usuarios();
            return View(tareaAmodificadar);
        
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
            return RedirectToAction("TareasTablerosPropios", new { idTablero = tarea.IdTablero });

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
            return RedirectToAction("TareasTablerosPropios", new { idTablero = tarea.IdTablero });

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
            TempData["EstadoTarea"] = "¡ Estado Actualizado !";
            return RedirectToAction("TareasTablerosNoPropios",new { idTablero = tarea.IdTablero});
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
        if (!SeLogueo()) return RedirectToRoute(new { controller = "Login", action = "Index" });
        int usuarioId = IdSesion();
        var tareas = _accesoTareas.TareasDeUnUsuario(usuarioId);
        var tareasView = new IndexTareaViewModel(tareas);
        ViewBag.idSession = usuarioId;
        return View("TareasTablerosNoPropios", tareasView);
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
