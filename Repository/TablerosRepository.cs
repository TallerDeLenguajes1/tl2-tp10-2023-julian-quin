using System.Data.SQLite;

namespace tl2_tp10_2023_julian_quin;
public class TableroRepository:ITableroRepository
{
    private readonly string _cadenaConexion;

    public TableroRepository(string CadenaDeConexion)
    {
        _cadenaConexion = CadenaDeConexion;
    }

    public Tablero NuevoTablero(Tablero tablero)
    {
        var query = "INSERT INTO Tablero (id_usuario_propietario, nombre, descripcion) VALUES (@idProp,@nombre,@descripcion)";
        using (SQLiteConnection connection = new SQLiteConnection(_cadenaConexion))
        {

            var command = new SQLiteCommand(query, connection);
            connection.Open();
            command.Parameters.Add(new SQLiteParameter("@idProp", tablero.IdUsuarioPropietario));
            command.Parameters.Add(new SQLiteParameter("@nombre", tablero.Nombre));
            command.Parameters.Add(new SQLiteParameter("@descripcion", tablero.Descripcion));
            command.ExecuteNonQuery();
            connection.Close();
        }
        return tablero;
    }
    public void ModificarTablero(Tablero tablero, int id)
    {
        var query = "UPDATE Tablero SET id_usuario_propietario = @id_usu_pt, nombre = @nombre, descripcion = @descripcion WHERE id = @id";
        using (SQLiteConnection connection = new SQLiteConnection(_cadenaConexion))
        {
            connection.Open();
            var command = new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@id_usu_pt", tablero.IdUsuarioPropietario));
            command.Parameters.Add(new SQLiteParameter("@nombre", tablero.Nombre));
            command.Parameters.Add(new SQLiteParameter("@descripcion", tablero.Descripcion));
            command.Parameters.Add(new SQLiteParameter("@id",id));
            command.ExecuteNonQuery();
            connection.Close();
        }

    }

    public Tablero TableroViaId(int id)
    {
        string query = "SELECT * FROM Tablero WHERE id = @id";
        Tablero tablero=null;
        using (SQLiteConnection connection = new SQLiteConnection(_cadenaConexion) )
        {
            connection.Open();
            var command = new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@id",id));
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    tablero = new Tablero();
                    tablero.Id = Convert.ToInt32(reader["id"]);
                    tablero.Nombre = reader["nombre"].ToString();
                    tablero.Descripcion = reader["descripcion"].ToString();
                    tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                }
            }
        }
        if (tablero == null) throw new Exception("Tablero No encontrado");
        return tablero;

    }
    public List<Tablero> Tableros()
    {
        string query = "SELECT * FROM Tablero";
        List<Tablero> tableros = new();
        Tablero tablero;
        using (SQLiteConnection connection = new SQLiteConnection(_cadenaConexion) )
        {
            connection.Open();
            var command = new SQLiteCommand(query, connection);
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    tablero = new();
                    tablero.Id = Convert.ToInt32(reader["id"]);
                    tablero.Nombre = reader["nombre"].ToString();
                    tablero.Descripcion = reader["descripcion"].ToString();
                    tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                    tableros.Add(tablero);
                }
            }
        }
        return tableros;

    }
  
    public List<Tablero> TablerosDeUnUsuario(int? idUsuario)
    {
        string query = "SELECT * FROM Tablero WHERE id_usuario_propietario = @idUsuario";
        List<Tablero> tableros = new();
        Tablero tablero;
        using (SQLiteConnection connection = new SQLiteConnection(_cadenaConexion) )
        {
            connection.Open();
            var command = new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@idUsuario",idUsuario));
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    tablero = new();
                    tablero.Id = Convert.ToInt32(reader["id"]);
                    tablero.Nombre = reader["nombre"].ToString();
                    tablero.Descripcion = reader["descripcion"].ToString();
                    tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                    tableros.Add(tablero);
                }
            }
        }
        return tableros;

    }

    public List<Tablero> TablerosTareasUsuario(int idUsuario)
    {
        string query ="SELECT DISTINCT tablero.* FROM tablero JOIN Tarea ON tablero.id = tarea.id_tablero WHERE tarea.id_usuario_asignado = @idUsuario AND tablero.id_usuario_propietario != @idUsuario;";

        //selecioname registros distintos de la tabla tablero, relacionadolo con la tabla tarea 
        //donde el id de tablero deber ser igual a un id de una tarea, pero no cualquier tarea, la tarea debe ser mia.
        // aparte debe pasar que ese tablero que me estas trayenado no sea mio.

        List<Tablero> tableros = new();
        Tablero tablero;
        using (SQLiteConnection connection = new SQLiteConnection(_cadenaConexion) )
        {
            connection.Open();
            var command = new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@idUsuario",idUsuario));
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    tablero = new();
                    tablero.Id = Convert.ToInt32(reader["id"]);
                    tablero.Nombre = reader["nombre"].ToString();
                    tablero.Descripcion = reader["descripcion"].ToString();
                    tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                    tableros.Add(tablero);
                }
            }
        }
        return tableros;

    }

     public void EliminarTablero(int id)
    {
        var query = "DELETE FROM Tablero WHERE id = @id";

        using (SQLiteConnection connection = new SQLiteConnection(_cadenaConexion))
        {
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(query,connection);
            command.Parameters.Add(new SQLiteParameter("@id", id));
            command.ExecuteNonQuery(); 
            connection.Close();

        }
    }
    public bool ExistenTareasEnTablero(int idTablero)
    {
        string query = "SELECT COUNT (id) FROM Tarea WHERE id_tablero = @idTablero;";
        int respuesta;
        using (SQLiteConnection connection = new SQLiteConnection(_cadenaConexion))
        {
            connection.Open();
            var command = new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@idTablero",idTablero));
            respuesta = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();
        }
        return respuesta > 0;

    }
    public List<Tablero> TablerosRestantes(int idUsuario)
    {
        string query = "SELECT tablero.id_usuario_propietario, tablero.nombre, tablero.descripcion, tablero.id, usuario.nombre_de_usuario FROM Tablero JOIN usuario on usuario.id = tablero.id_usuario_propietario WHERE id_usuario_propietario != @idUsuario";
        List<Tablero> tableros = new();
        Tablero tablero;
        using (SQLiteConnection connection = new SQLiteConnection(_cadenaConexion) )
        {
            connection.Open();
            var command = new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@idUsuario",idUsuario));
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    tablero = new();
                    tablero.Id = Convert.ToInt32(reader["id"]);
                    tablero.Nombre = reader["nombre"].ToString();
                    tablero.Descripcion = reader["descripcion"].ToString();
                    tablero.NombrePropietario = reader["nombre_de_usuario"].ToString();
                    tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                    tableros.Add(tablero);
                }
            }
        }
        return tableros;

    }
    


}