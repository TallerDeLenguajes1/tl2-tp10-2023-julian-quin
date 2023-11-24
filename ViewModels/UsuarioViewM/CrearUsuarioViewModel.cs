using System.ComponentModel.DataAnnotations;
namespace tl2_tp10_2023_julian_quin.ViewModels;

public class CrearUsuarioViewModel
{
    
    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Nombre de usuario")]
    public string NombreDeUsuario { get ; set; }

    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Contraseña")]
    public string Contrasenia { get; set ; }

    [Display(Name = "Rol")]
    public Rol Rol { get; set; }
}