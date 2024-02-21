using System.Data.SQLite;
using System.Linq.Expressions;

namespace tl2_tp10_2023_julian_quin;
public class TareasRepository:ITareasRepository
{
    private readonly string _cadenaConexion;
    public TareasRepository(string CadenaDeConexion)
    {
        _cadenaConexion = CadenaDeConexion;
    }
    

    public List<Tarea> Tareas()
    {
        string query =  "SELECT Tarea.id, Tarea.id_tablero , Tarea.nombre, Tarea.descripcion, Tarea.color, Tarea.id_usuario_asignado, Tarea.estado, Usuario.nombre_de_usuario FROM Tarea LEFT JOIN usuario ON Usuario.id = Tarea.id_usuario_asignado;";
        List<Tarea> tareas = new();
        Tarea tarea;
        using (SQLiteConnection connection = new SQLiteConnection(_cadenaConexion) )
        {
            connection.Open();
            var command = new SQLiteCommand(query, connection);
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    tarea = new Tarea();
                    tarea.Nombre = reader["nombre"].ToString();
                    tarea.Color = reader["color"].ToString();
                    if(reader["id_usuario_asignado"]!= DBNull.Value)tarea.IdUsuarioAsignado =Convert.ToInt32(reader["id_usuario_asignado"]);
                    else tarea.IdUsuarioAsignado = null;
                    tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                    tarea.Id =  Convert.ToInt32(reader["id"]);
                    tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                    tarea.Descripcion= reader["descripcion"].ToString();
                    if(reader["id_usuario_asignado"] == DBNull.Value)tarea.NombreUsuarioAsignado="Sin asignar";
                    else tarea.NombreUsuarioAsignado= reader["nombre_de_usuario"].ToString();
                    tareas.Add(tarea);
                }
            }
        }
        return tareas;

    }
    public Tarea CrearTarea(int idTablero, Tarea tarea)
    {
        var query = "INSERT INTO Tarea (id_tablero,nombre,descripcion,color,id_usuario_asignado,estado) VALUES (@idTablero,@nombre,@descripcion,@color,@idU,@estado)";
        using (SQLiteConnection connection = new SQLiteConnection(_cadenaConexion))
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
        var query = "UPDATE Tarea SET id_tablero = @idTab, nombre = @nombre, descripcion = @descripcion ,color = @color, id_usuario_asignado = @idU, estado = @estado WHERE id = @id";
        using (SQLiteConnection connection = new SQLiteConnection(_cadenaConexion))
        {
            connection.Open();
            var command = new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@nombre",tarea.Nombre));
            command.Parameters.Add(new SQLiteParameter("@descripcion", tarea.Descripcion));
            command.Parameters.Add(new SQLiteParameter("@estado", tarea.Estado));
            command.Parameters.Add(new SQLiteParameter("@idU", tarea.IdUsuarioAsignado));
            command.Parameters.Add(new SQLiteParameter("@color", tarea.Color));
            command.Parameters.Add(new SQLiteParameter("@id", id));
             command.Parameters.Add(new SQLiteParameter("@idTab", tarea.IdTablero));
            command.ExecuteNonQuery();
            connection.Close();
        }

    }
    //Obtener detalles de una tarea por su ID. (devuelve un objeto Tarea)
    public Tarea TareaId(int id)
    {   
        var queryString = @"SELECT * FROM Tarea WHERE id = @id;";
        Tarea tarea = null;
        using (SQLiteConnection connection = new SQLiteConnection(_cadenaConexion))
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
                    if(reader["id_usuario_asignado"]!= DBNull.Value)tarea.IdUsuarioAsignado =Convert.ToInt32(reader["id_usuario_asignado"]);
                    else tarea.IdUsuarioAsignado = null;
                    tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                    tarea.Id =  Convert.ToInt32(reader["id"]);
                    tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                    tarea.Descripcion= reader["descripcion"].ToString();
                }
            }
            connection.Close();
        }
        if(tarea == null) throw new Exception("Tarea No encontrada");
        return tarea;
        

    }
    //Listar todas las tareas asignadas a un usuario específico.(recibe un idUsuario devuelve un list de tareas)
    public List<Tarea> TareasDeUnUsuario(int idUsuario)
    {
        string query =  "SELECT Tarea.id, Tarea.id_tablero , Tarea.nombre, Tarea.descripcion, Tarea.color, Tarea.id_usuario_asignado, Tarea.estado, Usuario.nombre_de_usuario FROM Tarea LEFT JOIN usuario ON Usuario.id = Tarea.id_usuario_asignado WHERE Usuario.id = @idusuario";
        List<Tarea> tareas = new();
        Tarea tarea;
        using (SQLiteConnection connection = new SQLiteConnection(_cadenaConexion) )
        {
            connection.Open();
            var command = new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@idusuario",idUsuario));
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    tarea = new Tarea();
                    tarea.Nombre = reader["nombre"].ToString();
                    tarea.Color = reader["color"].ToString();
                    if(reader["id_usuario_asignado"]!= DBNull.Value)tarea.IdUsuarioAsignado =Convert.ToInt32(reader["id_usuario_asignado"]);
                    else tarea.IdUsuarioAsignado = null;
                    tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                    tarea.Id =  Convert.ToInt32(reader["id"]);
                    tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                    tarea.Descripcion= reader["descripcion"].ToString();
                    if(reader["id_usuario_asignado"] == DBNull.Value)tarea.NombreUsuarioAsignado="Sin asignar";
                    else tarea.NombreUsuarioAsignado= reader["nombre_de_usuario"].ToString();
                    tareas.Add(tarea);
                }
            }
        }
        return tareas;

    }
    //Listar todas las tareas de un tablero específico. (recibe un idTablero, devuelve un list de tareas)

    public List<Tarea> TareasDeUnTablero(int idTablero)
    {
        string query = "SELECT Tarea.id, Tarea.id_tablero , Tarea.nombre, Tarea.descripcion, Tarea.color, Tarea.id_usuario_asignado, Tarea.estado, Usuario.nombre_de_usuario FROM Tarea LEFT JOIN usuario ON Usuario.id = Tarea.id_usuario_asignado WHERE Tarea.id_tablero = @idTablero";
        List<Tarea> tareas = new();
        Tarea tarea;
        using (SQLiteConnection connection = new SQLiteConnection(_cadenaConexion) )
        {
            connection.Open();
            var command = new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@idTablero",idTablero));
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    tarea = new Tarea();
                    tarea.Nombre = reader["nombre"].ToString();
                    tarea.Color = reader["color"].ToString();
                    if(reader["id_usuario_asignado"]!= DBNull.Value)tarea.IdUsuarioAsignado =Convert.ToInt32(reader["id_usuario_asignado"]);
                    else tarea.IdUsuarioAsignado = null;
                    tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                    tarea.Id =  Convert.ToInt32(reader["id"]);
                    tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                    tarea.Descripcion= reader["descripcion"].ToString();
                    if(reader["id_usuario_asignado"] == DBNull.Value)tarea.NombreUsuarioAsignado="Sin asignar";
                    else tarea.NombreUsuarioAsignado= reader["nombre_de_usuario"].ToString();
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

        using (SQLiteConnection connection = new SQLiteConnection(_cadenaConexion))
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
        using (SQLiteConnection connection = new SQLiteConnection(_cadenaConexion))
        {
            connection.Open();
            var command = new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@usuarioAsignado",idUsuario));
            command.Parameters.Add(new SQLiteParameter("@idTarea", idTarea));
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
    public List<Tarea> TareasViaEstado(int idUsuario, EstadoTarea estado)
    {
       string query =  "SELECT Tarea.id, Tarea.id_tablero , Tarea.nombre, Tarea.descripcion, Tarea.color, Tarea.id_usuario_asignado, Tarea.estado, Usuario.nombre_de_usuario FROM Tarea JOIN usuario ON Usuario.id = Tarea.id_usuario_asignado WHERE Tarea.id_usuario_asignado = @idUsuario AND Tarea.estado = @estado";
        List<Tarea> tareas = new();
        Tarea tarea;
        using (SQLiteConnection connection = new SQLiteConnection(_cadenaConexion) )
        {
            connection.Open();
            var command = new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@idUsuario",idUsuario));
             command.Parameters.Add(new SQLiteParameter("@estado",estado));
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    tarea = new Tarea();
                    tarea.Nombre = reader["nombre"].ToString();
                    tarea.Color = reader["color"].ToString();
                    if(reader["id_usuario_asignado"]!= DBNull.Value)tarea.IdUsuarioAsignado =Convert.ToInt32(reader["id_usuario_asignado"]);
                    else tarea.IdUsuarioAsignado = null;
                    tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                    tarea.Id =  Convert.ToInt32(reader["id"]);
                    tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                    tarea.Descripcion= reader["descripcion"].ToString();
                    tarea.NombreUsuarioAsignado= reader["nombre_de_usuario"].ToString();
                    tareas.Add(tarea);
                }
            }
        }
        return tareas;

    }
}