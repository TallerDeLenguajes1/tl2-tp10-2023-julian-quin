using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_julian_quin.ViewModels;

namespace tl2_tp10_2023_julian_quin.Controllers;

public class TableroController : Controller
{
    private readonly ILogger<TableroController> _logger;
    private readonly ITableroRepository _accesoTableros;
    private readonly IUsuarioRepository _accesoUsuarios;

    public TableroController(ILogger<TableroController> logger, ITableroRepository tableroRepository, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        _accesoTableros = tableroRepository;
        _accesoUsuarios = usuarioRepository;
    }
    [HttpGet]
    public IActionResult Index()
    {
        try
        {
            if (!SeLogueo()) return RedirectToRoute(new { controller = "Login", action = "Index" });
            var idUsuario =  IdSesion();
            var tablerosNoPropios = _accesoTableros.TablerosTareasUsuario(idUsuario);
            var tablerosPropios = _accesoTableros.TablerosDeUnUsuario(idUsuario);
            return View(new IndexTableroViewModel(tablerosPropios,tablerosNoPropios));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return View("Error");
        }

    }
    [HttpGet]
    public IActionResult OtrosTableros(int idUsuario)
    {
        try
        {
            if (!SeLogueo()) return RedirectToRoute(new { controller = "Login", action = "Index" });
            if (!EsAdmin()) return BadRequest();
            var tableros= _accesoTableros.TablerosRestantes(idUsuario);
            return View(new IndexTableroViewModel(tableros));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return View("Error");
        }
    }

    [HttpGet]
    public IActionResult NuevoTablero()
    {
        try
        {
            if (!SeLogueo()) return RedirectToRoute(new { controller = "Home", action = "Index" });
            var nuevoTablero = new CrearTableroViewModel();
            if (EsAdmin())
            {
                nuevoTablero.Usuarios = _accesoUsuarios.Usuarios();
                return View(nuevoTablero);
            }
            var idUsuario = (int)HttpContext.Session.GetInt32("Id");
            nuevoTablero.Usuarios.Add(_accesoUsuarios.UsuarioViaId(idUsuario));
            return View(nuevoTablero); 
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return View("Error");
        }

    }

    [HttpPost]
    public IActionResult NuevoTablero(CrearTableroViewModel tablero)
    {
        try
        {
            if (!SeLogueo()) return RedirectToRoute(new { controller = "Home", action = "Index" });
            if (!ModelState.IsValid) return RedirectToAction("Index");
            var nuevotablero = new Tablero(tablero);
            _accesoTableros.NuevoTablero(nuevotablero);
            var tableros = _accesoTableros.Tableros();
            TempData["idTablero"] = tableros.FirstOrDefault(t => t.Nombre == tablero.Nombre).Id;
            TempData["Mensaje"] = "Nuevo";
            return RedirectToAction("Index");

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return View("Error");
        }

    }

    [HttpGet]
    public IActionResult ModificarTablero(int idTablero)
    {
        try
        {
            if (!SeLogueo()) return RedirectToRoute(new { controller = "Home", action = "Index" });
            var tablero = _accesoTableros.TableroViaId(idTablero);
            var idUsuario = IdSesion();
            var tableroAmodificadar = new ModificarTableroViewModel(tablero);
            return View(tableroAmodificadar);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return View("Error");
        }
    }

    [HttpPost]
    public IActionResult ModificarTablero(ModificarTableroViewModel tablero)
    {
        try
        {
            if (!SeLogueo()) return RedirectToRoute(new { controller = "Home", action = "Index" });
            if (!ModelState.IsValid) return RedirectToAction("Index");
            var tableroModificaciones = new Tablero(tablero);
            _accesoTableros.ModificarTablero(tableroModificaciones, tableroModificaciones.Id);
            TempData["idTablero"] = tablero.Id;
            TempData["Mensaje"] = "Actualizado";
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return View("Error");
        }

    }
    public IActionResult EliminarTablero(int idTablero)
    {
        try
        {
            if (!SeLogueo()) return RedirectToRoute(new { controller = "Home", action = "Index" });
            if (!_accesoTableros.ExistenTareasEnTablero(idTablero))
            {
                _accesoTableros.EliminarTablero(idTablero);
                return RedirectToAction("Index");
            }
            return View("MensajeAdvertencia",idTablero); //id para ir al tablero
           
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return View("Error");
        }


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
    private int IdSesion()
    {
        return (int)HttpContext.Session.GetInt32("Id");
    }


}
