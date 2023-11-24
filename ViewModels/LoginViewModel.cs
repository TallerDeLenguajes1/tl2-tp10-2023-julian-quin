using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace tl2_tp10_2023_julian_quin.ViewModels;

    public class LoginViewModel// vendria siendo como usuarioviewModel
    {
        [Required(ErrorMessage = "Se requiere este campo")]
        [Display(Name = "Nombre de Usuario")] 
        public string Nombre {get;set;}        
        
        [Required(ErrorMessage = "Este campo es requerido.")]
        [PasswordPropertyText]
        [Display(Name = "Contraseña")]
        public string Contrasenia {get;set;}
    }
