
using System.Data.SQLite;

namespace tl2_tp10_2023_julian_quin;
public class UsuarioRepositorio : IUsuarioRepository
{
    // Listar todos los usuarios registrados. (devuelve un List de Usuarios)
    private string cadenaConexion = "Data Source=DB/Kanban.db;Cache=Shared";
    public List<Usuario> GetUsser()
    {

        var queryString = @"SELECT * FROM Usuario;";
        List<Usuario> usuarios = new List<Usuario>();
        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            SQLiteCommand command = new SQLiteCommand(queryString, connection);
            connection.Open();

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var usuario = new Usuario();
                    usuario.NombreDeUsuario = reader["nombre_de_usuario"].ToString();
                    usuario.Id = Convert.ToInt32(reader["id"]);
                    usuarios.Add(usuario);

                }
            }
            connection.Close();
        }
        return usuarios;
    }
    public void NuevoUsuario(Usuario usuario)
    {
        var query = $"INSERT INTO Usuario (nombre_de_usuario) VALUES (@name)";
        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {

            connection.Open();
            var command = new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@name", usuario.NombreDeUsuario));
            command.ExecuteNonQuery();
            connection.Close();
        }
    }

    //Modificar un usuario existente. (recibe un Id y un objeto Usuario)
    public void UpdateUsuario(Usuario usuario, int id)
    {

        var query = "UPDATE Usuario SET nombre_de_usuario = @name WHERE id = @id";
        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            connection.Open();
            var command = new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@name", usuario.NombreDeUsuario));
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
    //Obtener detalles de un usuario por su ID. (recibe un Id y devuelve un Usuario)
    public Usuario GetUsserById(int id)
    {

        var queryString = @"SELECT * FROM Usuario WHERE id = @id;";
        Usuario usuario = null;
        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            SQLiteCommand command = new SQLiteCommand(queryString, connection);
            connection.Open();
            command.Parameters.Add(new SQLiteParameter("@id", id));

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    usuario = new Usuario();
                    usuario.Id = Convert.ToInt32(reader["id"]);
                    usuario.NombreDeUsuario = reader["nombre_de_usuario"].ToString();

                }
            }
            connection.Close();
        }
        return usuario;
    }
    public void DeleteUsuario(int id)
    {
        var query = "DELETE FROM Usuario WHERE id = @id";

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