namespace tl2_tp10_2023_julian_quin.ViewModels;
public class TareaViewModel
{
    public int Id {get; set;}
    public string Nombre  { get; set;}
    public string Descripcion  { get; set;}
    public string Color  { get; set;}
    public EstadoTarea Estado  { get; set;}
    public string UsuarioAsignado  { get; set;}
     public int? IdUsuarioAsignado  { get; set;}
    public int IdTablero  { get; set;}

    public TareaViewModel(Tarea tarea)
    {
        var accesoUsuarios = new UsuarioRepositorio();
        UsuarioAsignado = accesoUsuarios.UsuarioViaId(tarea.IdUsuarioAsignado ?? 0).NombreDeUsuario; 
        Nombre=tarea.Nombre;
        Id = tarea.Id;
        Descripcion = tarea.Descripcion;
        Color = tarea.Color;
        Estado = tarea.Estado;
        IdTablero = tarea.IdTablero;
        IdUsuarioAsignado = tarea.IdUsuarioAsignado;
    }
}