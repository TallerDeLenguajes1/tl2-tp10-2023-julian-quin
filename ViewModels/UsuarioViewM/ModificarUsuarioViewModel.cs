using System.ComponentModel.DataAnnotations;

namespace tl2_tp10_2023_julian_quin.ViewModels;
public class ModificarUsuarioViewModel
{
    public int Id {get; set;}

    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Nombre de usuario")]
    public string NombreDeUsuario { get ; set; }

    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Contraseña")]
    public string Contrasenia { get; set ; }

    [Display(Name = "Rol")]
    internal Rol Rol { get; set; }

    public ModificarUsuarioViewModel(Usuario usuario)
    {
        Id = usuario.Id;
        NombreDeUsuario = usuario.NombreDeUsuario;
        Contrasenia = usuario.Contrasenia;
        Rol = usuario.Rol;
    }
    public ModificarUsuarioViewModel(){}
}