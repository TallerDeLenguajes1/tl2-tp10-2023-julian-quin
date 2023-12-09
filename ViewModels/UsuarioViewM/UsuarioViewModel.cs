namespace tl2_tp10_2023_julian_quin.ViewModels;

public class UsuarioViewModel
{
    public int Id { get; set; } // lo necesito para los link (Eliminar o editar)
    public string NombreDeUsuario { get; set; }
    internal Rol Rol { get; set; }

    public UsuarioViewModel(Usuario usuario)
    {
        Id = usuario.Id;
        NombreDeUsuario = usuario.NombreDeUsuario;
        Rol = usuario.Rol;
    }



}