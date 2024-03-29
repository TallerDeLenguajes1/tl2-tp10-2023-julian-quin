namespace tl2_tp10_2023_julian_quin;
public interface ITareasRepository
{
    List<Tarea> Tareas();
    Tarea CrearTarea(int idTablero, Tarea tarea);
    void ModificarTarea(int id, Tarea tarea);
    Tarea TareaId(int id);
    List<Tarea> TareasDeUnUsuario(int idUsuario);
    void EliminarTarea(int idTarea);
    List<Tarea> TareasDeUnTablero(int idTablero);
    void AsignarTarea(int idUsuario, int idTarea);
    List<Tarea> TareasViaEstado(int idUsuario, EstadoTarea estado);
}