using System.Data.SQLite;

namespace tl2_tp10_2023_julian_quin;
public class TableroRepository:ITableroRepository
{
    private string cadenaConexion;

    public TableroRepository(string CadenaDeConexion)
    {
        this.cadenaConexion = CadenaDeConexion;
    }

    public Tablero NuevoTablero(Tablero tablero)
    {
        var query = "INSERT INTO Tablero (id_usuario_propietario, nombre, descripcion) VALUES (@idProp,@nombre,@descripcion)";
        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
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
    //Modificar un tablero existente (recibe un id y un objeto Tablero)
    public void ModificarTablero(Tablero tablero, int id)
    {
        var query = "UPDATE Tablero SET id_usuario_propietario = @id_usu_pt, nombre = @nombre, descripcion = @descripcion WHERE id = @id";
        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
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

    //Obtener detalles de un tablero por su ID. (recibe un id y devuelve un Tablero)
    public Tablero TableroViaId(int id)
    {
        string query = "SELECT * FROM Tablero WHERE id = @id";
        Tablero tablero=null;
        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion) )
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
        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion) )
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
    //Listar todos los tableros de un usuario específico. (recibe un IdUsuario, devuelve un list de tableros)
    public List<Tablero> TablerosDeUnUsuario(int? idUsuario)
    {
        string query = "SELECT * FROM Tablero WHERE id_usuario_propietario = @idUsuario";
        //si hago un JOIN, creo que deberia tener un campo nombreUsuarioPropietario en la Clase Tablero, y así lograr mostrar el nombre más el id del propietario.
        // string query = "SELECT Tablero.id, Tablero.nombre, Tablero.descripcion,Tablero.id_usuario_propietario, Usuario.nombre_de_usuario FROM Tablero JOIN Usuario ON Tablero.id_usuario_propietario = Usuario.id WHERE id_usuario_propietario = @idUsuario";
        List<Tablero> tableros = new();
        Tablero tablero;
        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion) )
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
                    //tablero.NombreUsuarioPropietario =reader["nombre_de_usuario"].ToString();
                    tableros.Add(tablero);
                }
            }
        }
        return tableros;

    }

     public void EliminarTablero(int id)
    {
        var query = "DELETE FROM Tablero WHERE id = @id";

        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(query,connection);
            command.Parameters.Add(new SQLiteParameter("@id", id));
            command.ExecuteNonQuery(); 
            connection.Close();

        }
    }
    


}