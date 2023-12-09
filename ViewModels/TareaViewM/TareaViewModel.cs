namespace tl2_tp10_2023_julian_quin.ViewModels;
public class TareaViewModel
{
    public int Id {get; set;} // lo necesito para los endpoint (links Eliminar o editar)
    public string Nombre  { get; set;}
    public string Descripcion  { get; set;}
    public string Color  { get; set;}
    public EstadoTarea Estado  { get; set;}

    public TareaViewModel(Tarea tarea)
    {
        Nombre = tarea.Nombre;
        Id = tarea.Id;
        Descripcion = tarea.Descripcion;
        Color = tarea.Color;
        Estado = tarea.Estado;
    }

}