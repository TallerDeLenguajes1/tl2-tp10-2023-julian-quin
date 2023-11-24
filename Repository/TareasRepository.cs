using System.Data.SQLite;

namespace tl2_tp10_2023_julian_quin;
public class TareasRepository:ITareasRepository
{
    private string cadenaConexion = "Data Source=DB/Kanban.db;Cache=Shared";

    //Crear una nueva tarea en un tablero. (recibe un idTablero, devuelve un objeto Tarea)
    public Tarea CrearTarea(int idTablero, Tarea tarea)
    {
        var query = "INSERT INTO Tarea (id_tablero,nombre,descripcion,color,id_usuario_asignado,estado) VALUES (@idTablero,@nombre,@descripcion,@color,@idU,@estado)";
        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {

            var command = new SQLiteCommand(query, connection);
            connection.Open();
            command.Parameters.Add(new SQLiteParameter("@idTablero",idTablero));
            command.Parameters.Add(new SQLiteParameter("@nombre",tarea.Nombre));
            command.Parameters.Add(new SQLiteParameter("@descripcion", tarea.Descripcion));
            command.Parameters.Add(new SQLiteParameter("@estado", tarea.Estado));
            command.Parameters.Add(new SQLiteParameter("@idU", tarea.IdUsuarioAsignado));
            command.Parameters.Add(new SQLiteParameter("@color", tarea.Color));
            command.ExecuteNonQuery();
            connection.Close();
        }
        tarea.IdTablero = idTablero;
        return tarea;
    }
    public void ModificarTarea(int id, Tarea tarea)
    {
        var query = "UPDATE Tarea SET nombre = @nombre, descripcion = @descripcion ,color = @color, id_usuario_asignado = @idU, estado = @estado WHERE id = @id";
        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            connection.Open();
            var command = new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@nombre",tarea.Nombre));
            command.Parameters.Add(new SQLiteParameter("@descripcion", tarea.Descripcion));
            command.Parameters.Add(new SQLiteParameter("@estado", tarea.Estado));
            command.Parameters.Add(new SQLiteParameter("@idU", tarea.IdUsuarioAsignado));
            command.Parameters.Add(new SQLiteParameter("@color", tarea.Color));
            command.Parameters.Add(new SQLiteParameter("@id", id));
            command.ExecuteNonQuery();
            connection.Close();
        }

    }
    //Obtener detalles de una tarea por su ID. (devuelve un objeto Tarea)
    public Tarea TareaId(int id)
    {   
        var queryString = @"SELECT * FROM Tarea WHERE id = @id;";
        Tarea tarea = null;
        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            SQLiteCommand command = new SQLiteCommand(queryString, connection);
            connection.Open();
            command.Parameters.Add(new SQLiteParameter("@id", id));

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    tarea = new Tarea();
                    tarea.Nombre = reader["nombre"].ToString();
                    tarea.Color = reader["color"].ToString();
                    if(reader["id_usuario_asignado"]!=null) tarea.IdUsuarioAsignado =Convert.ToInt32(reader["id_usuario_asignado"]);
                    else tarea.IdUsuarioAsignado = null;
                    tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                    tarea.Id =  Convert.ToInt32(reader["id"]);
                    tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                    tarea.Descripcion= reader["descripcion"].ToString();
                }
            }
            connection.Close();
        }
        return tarea;
        

    }
    //Listar todas las tareas asignadas a un usuario específico.(recibe un idUsuario devuelve un list de tareas)
    public List<Tarea> TareasDeUnUsuario(int idUsuario)
    {
        string query = "SELECT * FROM Tarea WHERE id_usuario_asignado = @idUsuario";
      
        List<Tarea> tareas = new();
        Tarea tarea;
        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion) )
        {
            connection.Open();
            var command = new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@idUsuario",idUsuario));
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    tarea = new Tarea();
                    tarea.Nombre = reader["nombre"].ToString();
                    tarea.Color = reader["color"].ToString();
                    if(reader["id_usuario_asignado"]!=null) tarea.IdUsuarioAsignado =Convert.ToInt32(reader["id_usuario_asignado"]);
                    else tarea.IdUsuarioAsignado = null;
                    tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                    tarea.Id =  Convert.ToInt32(reader["id"]);
                    tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                    tarea.Descripcion= reader["descripcion"].ToString();
                    tareas.Add(tarea);
                }
            }
        }
        return tareas;

    }
    //Listar todas las tareas de un tablero específico. (recibe un idTablero, devuelve un list de tareas)

    public List<Tarea> TareasDeUnTablero(int idTablero)
    {
        string query = "SELECT * FROM Tarea WHERE id_tablero = @idUsuario";
        List<Tarea> tareas = new();
        Tarea tarea;
        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion) )
        {
            connection.Open();
            var command = new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@idUsuario",idTablero));
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    tarea = new Tarea();
                    tarea.Nombre = reader["nombre"].ToString();
                    tarea.Color = reader["color"].ToString();
                    if(reader["id_usuario_asignado"]!=null) tarea.IdUsuarioAsignado =Convert.ToInt32(reader["id_usuario_asignado"]);
                    else tarea.IdUsuarioAsignado = null;
                    tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                    tarea.Id =  Convert.ToInt32(reader["id"]);
                    tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                    tarea.Descripcion= reader["descripcion"].ToString();
                    tareas.Add(tarea);
                }
            }
        }
        return tareas;
    }
   //Eliminar una tarea (recibe un IdTarea) 
    public void EliminarTarea(int idTarea)
    {
        var query = "DELETE FROM Tarea WHERE id = @idTarea";

        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(query,connection);
            command.Parameters.Add(new SQLiteParameter("@idTarea", idTarea));
            command.ExecuteNonQuery();
            connection.Close();

        }
    }
    //Asignar Usuario a Tarea (recibe idUsuario y un idTarea)
    public void AsignarTarea(int idUsuario, int idTarea)
    {
        var query = "UPDATE Tarea set  id_usuario_asignado = @usuarioAsignado   WHERE id = @idTarea";
        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            connection.Open();
            var command = new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@usuarioAsignado",idUsuario));
            command.Parameters.Add(new SQLiteParameter("@idTarea", idTarea));
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}