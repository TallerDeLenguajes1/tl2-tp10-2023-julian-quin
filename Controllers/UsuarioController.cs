using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_julian_quin.ViewModels;

namespace tl2_tp10_2023_julian_quin.Controllers;

public class UsuarioController : Controller
{
    private readonly ILogger<UsuarioController> _logger;
    private readonly IUsuarioRepository _accesoUsuarios;
    private readonly ITareasRepository _tareaRepository;

    public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepository usuarioRepository, ITareasRepository tareaRepository)
    {
        _logger = logger;
        _accesoUsuarios = usuarioRepository;
        _tareaRepository = tareaRepository;
    }

    public IActionResult Index()
    {
        try
        {
            if (!SeLogueo()) return RedirectToRoute(new { controller = "Login", action = "Index" });
            List<Usuario> usuarios = new();
            if (EsAdmin())
            {
                usuarios = _accesoUsuarios.Usuarios();
                var usuariosView = new IndexUsuarioViewModel(usuarios);
                return View(usuariosView);
            }
            var usuarioId = (int)HttpContext.Session.GetInt32("Id");
            var usuario = _accesoUsuarios.UsuarioViaId(usuarioId);
            usuarios.Add(usuario);
            return View(new IndexUsuarioViewModel(usuarios));

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return View("Error");
        }

    }

    [HttpGet]
    public IActionResult NuevoUsuario()
    {
        try
        {
            if (!SeLogueo()) return RedirectToRoute(new { controller = "Login", action = "Index" });
            if (!EsAdmin()) return View("MensajeAdvertencia");
            return View(new CrearUsuarioViewModel());

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return View("Error");
        }

    }

    [HttpPost]
    public IActionResult NuevoUsuario(CrearUsuarioViewModel usuario)
    {
        try
        {
            if (!SeLogueo()) return RedirectToRoute(new { controller = "Login", action = "Index" });
            if (!EsAdmin()) return View("MensajeAdvertencia");
            if (!ModelState.IsValid) return RedirectToAction("Index");
            var nuevoUsuario = new Usuario(usuario);
            _accesoUsuarios.NuevoUsuario(nuevoUsuario);
            return RedirectToAction("Index");

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return View("Error");
        }

    }

    [HttpGet]
    public IActionResult ModificarUsuario(int idUsuario)
    {
        try
        {
            if (!SeLogueo()) return RedirectToRoute(new { controller = "Login", action = "Index" });
            if (!EsAdmin()) return View("MensajeAdvertencia");
            var usuario = _accesoUsuarios.UsuarioViaId(idUsuario);
            var usuarioViewModel = new ModificarUsuarioViewModel(usuario);
            return View(usuarioViewModel);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return View("Error");
        }

    }

    [HttpPost]
    public IActionResult ModificarUsuario(ModificarUsuarioViewModel usuarioUp)
    {
        try
        {
            if (!SeLogueo()) return RedirectToRoute(new { controller = "Login", action = "Index" });
            if (!EsAdmin()) return View("MensajeAdvertencia");
            if (!ModelState.IsValid) return RedirectToAction("Index");
            var usuario = new Usuario(usuarioUp);
            _accesoUsuarios.ActualizarUsuario(usuario, usuario.Id);
            return RedirectToAction("Index");

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return View("Error");
        }

    }

    public IActionResult EliminarUsuario(int idUsuario)
    {
        try
        {
            if (!SeLogueo()) return RedirectToRoute(new { controller = "Login", action = "Index" });
            if (!EsAdmin()) return View("MensajeAdvertencia");
            var tareas = _tareaRepository.TareasDeUnUsuario(idUsuario);
            foreach (var tarea in tareas)
            {
                tarea.IdUsuarioAsignado = null;
                _tareaRepository.ModificarTarea(tarea.Id, tarea);
            }
            _accesoUsuarios.EliminarUsuario(idUsuario);
            return RedirectToAction("Index");

        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            return View("Error");
        }

    }

    private bool EsAdmin()
    {
        if (HttpContext.Session.GetString("NivelAcceso") == Rol.administrador.ToString()) return true;
        return false;
    }
    private bool SeLogueo()
    {
        if (HttpContext.Session.GetString("Usuario") != null && HttpContext.Session.GetInt32("Id") != null
        && HttpContext.Session.GetString("NivelAcceso") != null) return true;

        return false;
    }

}
