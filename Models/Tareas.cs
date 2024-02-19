using tl2_tp10_2023_julian_quin.ViewModels;

namespace tl2_tp10_2023_julian_quin;
public enum EstadoTarea
{
    ToDo,
    Doing,
    Review,
    Done,
}
public class Tarea
{
    public int Id {get; set;}
    public string Nombre {get; set;}
    public string Descripcion {get; set;}
    public string Color {get; set;}
    public EstadoTarea Estado {get; set;}
    public int? IdUsuarioAsignado {get; set;}
    public int IdTablero {get; set;}
    public string NombreUsuarioAsignado {get;set;}
    public Tarea (CrearTareaViewModel tarea)
    {
        Nombre = tarea.Nombre;
        Descripcion = tarea.Descripcion;
        Color = tarea.Color;
        Estado = tarea.Estado;
        IdUsuarioAsignado = tarea.IdUsuarioAsignado;
        IdTablero = tarea.IdTablero; 
    }
    public Tarea(ModificarTareaViewModel tarea )
    {
        Id = tarea.Id;
        Nombre = tarea.Nombre;
        Descripcion = tarea.Descripcion;
        Color = tarea.Color;
        Estado = tarea.Estado;
        IdUsuarioAsignado = tarea.IdUsuarioAsignado;
        IdTablero = tarea.IdTablero;
    }
    public Tarea (){}
}