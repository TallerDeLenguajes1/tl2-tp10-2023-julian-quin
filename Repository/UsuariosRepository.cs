
using System.Data.SQLite;

namespace tl2_tp10_2023_julian_quin;
public class UsuarioRepositorio : IUsuarioRepository
{
    // Listar todos los usuarios registrados. (devuelve un List de Usuarios)
    private string cadenaConexion = "Data Source=DB/Kanban.db;Cache=Shared";
    public List<Usuario> Usuarios()
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
                    usuario.Contrasenia = reader["pass"].ToString();
                    usuario.Id = Convert.ToInt32(reader["id"]);
                    usuario.Rol = (Rol)Convert.ToInt32(reader["rol"]);
                    usuarios.Add(usuario);

                }
            }
            connection.Close();
        }
        return usuarios;
    }
    public void NuevoUsuario(Usuario usuario)
    {
        var query = $"INSERT INTO Usuario (nombre_de_usuario,rol,pass) VALUES (@name,@rol,@pass)";
        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {

            connection.Open();
            var command = new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@name", usuario.NombreDeUsuario));
            command.Parameters.Add(new SQLiteParameter("@rol", usuario.Rol));
            command.Parameters.Add(new SQLiteParameter("@pass", usuario.Contrasenia));
            command.ExecuteNonQuery();
            connection.Close();
        }
    }

    //Obtener detalles de un usuario por su ID. (recibe un Id y devuelve un Usuario)
    public Usuario UsuarioViaId(int id)
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
                while (reader.Read()) // cuando no encuentra el id, reader no entra al while HasRow = false
                {
                    usuario = new Usuario();
                    usuario.Id = Convert.ToInt32(reader["id"]);
                    usuario.NombreDeUsuario = reader["nombre_de_usuario"].ToString();
                    usuario.Rol = (Rol)Convert.ToInt32(reader["rol"]);

                }
            }
            connection.Close();
        }
        return usuario;
    }
    /// EN ESTOS DOS ULTIMOS METODOS ESTOY DEVOLVIENDO UN VALOR BOOLEANO PARA PODER INFORMAR SI SE REALIZÓ
    /// LA OPERACION QUE HACE CADA UNO, PERO CREO QUE NO TENDRIA QUE DEVORVER NADA NO SÉ ¡ PREGUNTAR ! 
    public bool EliminarUsuario(int id)
    {
        var query = "DELETE FROM Usuario WHERE id = @id"; // si no encuentra el id no se rompe el codigo
        bool flag = false;

        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(query,connection);
            command.Parameters.Add(new SQLiteParameter("@id", id));
            var row = command.ExecuteNonQuery();
            if (row>0) flag = true;
            connection.Close();
        }
        return flag;
    }
    //Modificar un usuario existente. (recibe un Id y un objeto Usuario)
    public bool ActualizarUsuario(Usuario usuario, int id)
    {

        var query = "UPDATE Usuario SET nombre_de_usuario = @name, rol = @rol, pass = @contras WHERE id = @id";
        bool flag = false;
        using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        {
            connection.Open();
            var command = new SQLiteCommand(query, connection);
            command.Parameters.Add(new SQLiteParameter("@name", usuario.NombreDeUsuario));
            command.Parameters.Add(new SQLiteParameter("@id",id));
            command.Parameters.Add(new SQLiteParameter("@rol",usuario.Rol));
            command.Parameters.Add(new SQLiteParameter("@pass",usuario.Contrasenia));
            var row = command.ExecuteNonQuery();
            if (row>0) flag = true;
            connection.Close();
        }
        return flag;
    }


}