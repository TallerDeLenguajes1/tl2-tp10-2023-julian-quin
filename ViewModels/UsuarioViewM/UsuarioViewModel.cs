namespace tl2_tp10_2023_julian_quin.ViewModels;

public class UsuarioViewModel
{
    public int Id { get; set; }
    public string NombreDeUsuario { get; set; }
    public string Contrasenia { get; set; }
    internal Rol Rol { get; set; }

    public UsuarioViewModel(Usuario usuario)
    {
        Id = usuario.Id;
        NombreDeUsuario = usuario.NombreDeUsuario;
        Contrasenia = usuario.Contrasenia;
        Rol = usuario.Rol;
    }



}