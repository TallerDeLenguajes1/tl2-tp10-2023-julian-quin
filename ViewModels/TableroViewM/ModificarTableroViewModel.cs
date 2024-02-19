using System.ComponentModel.DataAnnotations;

namespace tl2_tp10_2023_julian_quin.ViewModels;

public class ModificarTableroViewModel
{

  [Required(ErrorMessage = "Este campo es requerido")]
  [Display(Name = "Nombre")]
  public string Nombre { get; set; }

  [Required(ErrorMessage = "Este campo es requerido")]
  [Display(Name = "Descripcion")]
  public string Descripcion { get; set; }
  public int IdUsuarioPropietario { get; set; } 
  public int Id { get; set; }

  public ModificarTableroViewModel(Tablero tablero)
  {
    Id = tablero.Id;
    IdUsuarioPropietario = tablero.IdUsuarioPropietario;
    Nombre = tablero.Nombre;
    Descripcion = tablero.Descripcion;
  }
  public ModificarTableroViewModel() { }


}