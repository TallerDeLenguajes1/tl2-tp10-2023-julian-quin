using System.ComponentModel.DataAnnotations;

namespace tl2_tp10_2023_julian_quin.ViewModels;

public class ModificarTareaViewModel
{
    public int Id{get; set;}
    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Id del propietario")]
    public string Nombre  { get; set;}
    public string Descripcion  { get; set;}
    public string Color  { get; set;}
    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Estado")]
    public EstadoTarea Estado  { get; set;}
    public int? IdUsuarioAsignado  { get; set;}

    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Id De Tablero")]
    public int IdTablero  { get; set;}

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
    
    
}
