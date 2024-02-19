using tl2_tp10_2023_julian_quin.Models;
namespace tl2_tp10_2023_julian_quin.ViewModels;
public class IndexUsuarioViewModel
{
    public List<UsuarioViewModel> usuarios = new();
    public IndexUsuarioViewModel(List<Usuario> usuariosPrimitivos)
    {
        usuarios = new();
        foreach (var usuario in usuariosPrimitivos)
        {
            var nuevoUsuario = new UsuarioViewModel(usuario);
            usuarios.Add(nuevoUsuario);    
        }

    }
}