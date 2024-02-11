namespace tl2_tp10_2023_julian_quin;
public interface IUsuarioRepository
{
    List<Usuario> Usuarios();
    void NuevoUsuario (Usuario usuario);
    bool ActualizarUsuario(Usuario usuario, int id);
    Usuario UsuarioViaId(int id);
    bool EliminarUsuario(int id);
    Usuario Logueo(string contrasenia, string usser);
    public bool AutenticarUsuario(string nombreUsuario, string contrasenia);

}