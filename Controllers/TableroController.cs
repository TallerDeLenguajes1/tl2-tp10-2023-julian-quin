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
            if (EsAdmin()) return View(new IndexTableroViewModel(_accesoTableros.Tableros()));
            var idUsuario = (int)HttpContext.Session.GetInt32("Id");
            var tablerosPropios = _accesoTableros.TablerosDeUnUsuario(idUsuario);
            var tablerosNoPropios = _accesoTableros.TablerosTareasUsuario(idUsuario);
            var AlModelo = new IndexTableroViewModel(tablerosPropios,tablerosNoPropios);
            return View(AlModelo);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }

    }

    [HttpGet]
    public IActionResult NuevoTablero()
    {
        try
        {
            if (!SeLogueo()) return RedirectToRoute(new { controller = "Home", action = "Index" });
            var nuevoTablero = new CrearTableroViewModel();
            return View(nuevoTablero);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }

    }

    [HttpPost]
    public IActionResult NuevoTablero(CrearTableroViewModel tablero)
    {
        try
        {
            if (!SeLogueo()) return RedirectToRoute(new { controller = "Home", action = "Index" });
            if (!ModelState.IsValid) return RedirectToAction("Index");
            var idSession = (int)HttpContext.Session.GetInt32("Id");
            var nuevotablero = new Tablero(tablero);
            nuevotablero.IdUsuarioPropietario = idSession;
            _accesoTableros.NuevoTablero(nuevotablero);
            return RedirectToAction("Index");

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }

    }

    [HttpGet]
    public IActionResult ModificarTablero(int idTablero)
    {
        try
        {
            if (!SeLogueo()) return RedirectToRoute(new { controller = "Home", action = "Index" });
            var tablero = _accesoTableros.TableroViaId(idTablero);
            var usuarios = _accesoUsuarios.Usuarios();
            var tableroAmodificadar = new ModificarTableroViewModel(tablero);
            tableroAmodificadar.Usuarios = usuarios;
            return View(tableroAmodificadar);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
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
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
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
            return View("MensajeAdvertencia");
           
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
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


}
