using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_julian_quin.ViewModels;
namespace tl2_tp10_2023_julian_quin.Controllers;

public class LoginController : Controller
{

    private readonly ILogger<LoginController> _logger;
    private readonly IUsuarioRepository accesoUsuarios;
    public LoginController(ILogger<LoginController> logger, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        this.accesoUsuarios = usuarioRepository;
    }

    public IActionResult Index()
    {
        return View(new LoginViewModel());
    }


    public IActionResult Login(LoginViewModel usuario)
    {
        try
        {
            var usuarioLogin = accesoUsuarios.Logueo(usuario.Contrasenia, usuario.Nombre);
            logearUsuario(usuarioLogin);
            _logger.LogInformation("Usuario " + usuarioLogin.NombreDeUsuario + " logueado correctamente");
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }
        catch (Exception Ex)
        {
            _logger.LogError(Ex.ToString());
            _logger.LogWarning("Intento de acceso invalido: Usuario: " + usuario.Nombre + " -- Clave ingresada: " + usuario.Contrasenia);
            return RedirectToAction("Index");   
        }

    }

    private void logearUsuario(Usuario Usuario)
    {
        HttpContext.Session.SetString("Usuario", Usuario.NombreDeUsuario);
        HttpContext.Session.SetString("pass", Usuario.Contrasenia);
        HttpContext.Session.SetString("NivelAcceso", Usuario.Rol.ToString().ToLower());
        HttpContext.Session.SetInt32("Id", Usuario.Id);
    }
}