namespace tl2_tp10_2023_julian_quin;
public interface IUsuarioRepository
{
    List<Usuario> GetUsser();
    void NuevoUsuario (Usuario usuario);
    void UpdateUsuario(Usuario usuario, int id);
    Usuario GetUsserById(int id);
    void DeleteUsuario(int id);

}