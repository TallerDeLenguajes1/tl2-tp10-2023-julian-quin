using tl2_tp10_2023_julian_quin.ViewModels;
namespace tl2_tp10_2023_julian_quin;

public enum Rol
{
    administrador=1,
    operador=2, 
}
public class Usuario 
{
    private int id;
    private string nombreDeUsuario;
    private Rol rol;
    private string contrasenia;

    public int Id { get => id; set => id = value; }
    public string NombreDeUsuario { get => nombreDeUsuario; set => nombreDeUsuario = value; }
    public string Contrasenia { get => contrasenia; set => contrasenia = value; }
    internal Rol Rol { get => rol; set => rol = value; }

    public Usuario(CrearUsuarioViewModel usuario)
    {   
        rol = usuario.Rol;         
        NombreDeUsuario = usuario.NombreDeUsuario;
        Contrasenia = usuario.Contrasenia;
    }
    public Usuario(ModificarUsuarioViewModel usuario)
    {  
        Id = usuario.Id; 
        rol = usuario.Rol;         
        NombreDeUsuario = usuario.NombreDeUsuario;
        Contrasenia = usuario.Contrasenia;
    }
    public Usuario(){}
}