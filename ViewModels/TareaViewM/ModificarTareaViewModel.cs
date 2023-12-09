using System.ComponentModel.DataAnnotations;

namespace tl2_tp10_2023_julian_quin.ViewModels;

public class ModificarTareaViewModel
{
    
    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Nombre")]
    public string Nombre  { get; set;}

    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Descripcion")]
    public string Descripcion  { get; set;}

    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Color")]
    public string Color  { get; set;}
    
    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Estado")]
    public EstadoTarea Estado  { get; set;}

    [Required(ErrorMessage = "Este campo es requerido")]
    public int IdTablero  { get; set;} // para el link que lleva al get 
    
    
    public int? IdUsuarioAsignado  { get; set;}
    public List<Tablero> Tableros; 
    public int Id{get; set;} // para identificar la tarea desde el metodo get
    public ModificarTareaViewModel(Tarea tarea)
    {
        Id = tarea.Id;
        Nombre = tarea.Nombre;
        Descripcion = tarea.Descripcion;
        Estado = tarea.Estado;
        Color = tarea.Color;
        IdTablero = tarea.IdTablero;
        IdUsuarioAsignado = tarea.IdUsuarioAsignado;
    }
    public ModificarTareaViewModel(){}
    
    
}
