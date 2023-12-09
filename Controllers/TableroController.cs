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
            else if (EsAdmin()) return View(new IndexTableroViewModel(_accesoTableros.Tableros()));
            var idUsuario = (int)HttpContext.Session.GetInt32("Id");
            var tableros = _accesoTableros.TablerosDeUnUsuario(idUsuario);
            return View(new IndexTableroViewModel(tableros));
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
            if (!EsAdmin()) return RedirectToAction("Index");
            var nuevoTablero = new CrearTableroViewModel();
            nuevoTablero.usuarios = _accesoUsuarios.Usuarios();
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
            if (!EsAdmin()) return RedirectToAction("Index");
            if (!ModelState.IsValid) return RedirectToAction("Index");
            var nuevotablero = new Tablero(tablero);
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
            if (!EsAdmin()) return RedirectToAction("Index");
            var tablero = _accesoTableros.TableroViaId(idTablero);
            var usuarios = _accesoUsuarios.Usuarios();
            var tareaAmodificadar = new ModificarTableroViewModel(tablero);
            tareaAmodificadar.Usuarios = usuarios;
            return View(tareaAmodificadar);

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
            if (!EsAdmin()) return RedirectToAction("Index");
            _accesoTableros.EliminarTablero(idTablero);
            return RedirectToAction("Index");
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
