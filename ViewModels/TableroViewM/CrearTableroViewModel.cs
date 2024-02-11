using System.ComponentModel.DataAnnotations;
namespace tl2_tp10_2023_julian_quin.ViewModels;

public class CrearTableroViewModel 
{
    public int IdUsuarioPropietario { get; set; }

    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Nombre")]
    public string Nombre { get ; set; }

    [Display(Name = "Descripcion")]
    public string Descripcion { get ; set; }
    public CrearTableroViewModel(){}
    
   
}
