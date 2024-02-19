using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_julian_quin.ViewModels;
namespace tl2_tp10_2023_julian_quin.Controllers;
public class LoginController : Controller
{

    private readonly ILogger<LoginController> _logger;
    private readonly IUsuarioRepository _accesoUsuarios;
    public LoginController(ILogger<LoginController> logger, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        _accesoUsuarios = usuarioRepository;
    }

    public IActionResult Index()
    {
        return View(new LoginViewModel());
    }


    public IActionResult Login(LoginViewModel usuario)
    {
        try
        {
            var usuarioLogin = _accesoUsuarios.AutenticarUsuario(usuario.Nombre,usuario.Contrasenia);
            if (usuarioLogin == false)
            {
                usuario.Mensaje = "Usuario o contraseña no valida";
                DateTime fechaHora = DateTime.Now;
                _logger.LogWarning("Intento de acceso invalido: Usuario: " + usuario.Nombre + " -- Clave ingresada: " + usuario.Contrasenia + " --fecha/hora : " + fechaHora.ToString());
                return View("Index", usuario);   
            }
            var Usuario = _accesoUsuarios.Logueo(usuario.Contrasenia,usuario.Nombre);
            logearUsuario(Usuario);
            _logger.LogInformation("Usuario " + Usuario.NombreDeUsuario + " logueado correctamente");
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }
        catch (Exception Ex)
        {
            _logger.LogError("Error al intentar loguarse " + Ex.ToString());
            return BadRequest();      
        }

    }

    private void logearUsuario(Usuario Usuario)
    {
        HttpContext.Session.SetString("Usuario", Usuario.NombreDeUsuario);
        HttpContext.Session.SetString("pass", Usuario.Contrasenia);
        HttpContext.Session.SetString("NivelAcceso", Usuario.Rol.ToString().ToLower());
        HttpContext.Session.SetInt32("Id", Usuario.Id);
    }
    public IActionResult Logout()
    {
        try
        {
            DesloguearUsuario();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al intentar cerrar sesión del usuario {ex.ToString()}");
        }
        return RedirectToAction("Index");
    }

    private void DesloguearUsuario()
    {
        HttpContext.Session.Clear();
    }
}