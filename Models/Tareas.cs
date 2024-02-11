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
    private int id;
    private int idTablero;
    private string nombre;
    private string descripcion;
    private string color;
    private int? idUsuarioAsignado;
    public string NombreUsuarioAsignado {get;set;}
    private EstadoTarea estado;

    public int Id { get => id; set => id = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }
    public string Color { get => color; set => color = value; }
    public EstadoTarea Estado { get => estado; set => estado = value; }
    public int? IdUsuarioAsignado { get => idUsuarioAsignado; set => idUsuarioAsignado = value; }
    public int IdTablero { get => idTablero; set => idTablero = value; }
    public Tarea (CrearTareaViewModel tarea)
    {
        Nombre = tarea.Nombre;
        Descripcion = tarea.Descripcion;
        Color = tarea.Color;
        Estado = tarea.Estado;
        IdUsuarioAsignado = tarea.IdUsuarioAsignado;
        idTablero = tarea.IdTablero; 
    }
    public Tarea(ModificarTareaViewModel tarea )
    {
        Id = tarea.Id;
        Nombre = tarea.Nombre;
        Descripcion = tarea.Descripcion;
        Color = tarea.Color;
        Estado = tarea.Estado;
        IdUsuarioAsignado = tarea.IdUsuarioAsignado;
        idTablero = tarea.IdTablero;
    }
    public Tarea (){}
}