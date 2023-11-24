using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_julian_quin.ViewModels;
namespace tl2_tp10_2023_julian_quin.Controllers;

public class LoginController : Controller
{

    private readonly ILogger<LoginController> _logger;
    //login no tiene un model
    private IUsuarioRepository acceso;
    public LoginController(ILogger<LoginController> logger)
    {
        _logger = logger;
        acceso = new UsuarioRepositorio();
    }

    public IActionResult Index()
    {
        return View(new LoginViewModel());
    }


    public IActionResult Login(LoginViewModel usuario) //si recivo "LoginViewModel usuario" no se rompe
    {
        //usuario en el campo nivelDeAcceso viene por defecto 0

        //existe el usuario?
        var listaDeUsuario = acceso.Usuarios();
        var usuarioLogin = listaDeUsuario.FirstOrDefault(usser => usser.Contrasenia == usuario.Contrasenia && usser.NombreDeUsuario.ToLower() == usuario.Nombre.ToLower());

        // si el usuario no existe devuelvo al index
        if (usuarioLogin == null) return RedirectToAction("Index");
        //Registro el usuario
        logearUsuario(usuarioLogin);
        
        //Devuelvo el usuario al Home
        return RedirectToRoute(new { controller = "Home", action = "Index" }); // redirecciona a al index de home, pues el index de login tiene un formulario
        // controller = "Home" especifica el nombre del controlador al que se redirigirá.
        // action = "Index" especifica la acción del controlador a la que se redirigirá.
    }

    private void logearUsuario(Usuario Usuario)
    {
    
        HttpContext.Session.SetString("Usuario", Usuario.NombreDeUsuario);
        HttpContext.Session.SetString("pass", Usuario.Contrasenia);
        HttpContext.Session.SetString("NivelAcceso", Usuario.Rol.ToString().ToLower());
        HttpContext.Session.SetInt32("Id",Usuario.Id);
    }
}