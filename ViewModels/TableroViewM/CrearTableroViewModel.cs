using System.ComponentModel.DataAnnotations;
namespace tl2_tp10_2023_julian_quin.ViewModels;

public class CrearTableroViewModel //luego se hace una conversion de este objeto desde un contructor del modelo original
{
    //aqui no es necesario tener el id de tablero
    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Selecione propietario")]
    public int IdUsuarioPropietario { get; set; }

    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Nombre")]
    public string Nombre { get ; set; }

    [Display(Name = "Descripcion")]
    public string Descripcion { get ; set; }

    public List<Usuario> usuarios;// lo necesito para la vista que desplegará la lista de usuarios y asi completar el campo--IdUsuarioPropietario
    public CrearTableroViewModel(){}
    
   
}
