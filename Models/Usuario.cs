using tl2_tp10_2023_julian_quin.ViewModels;
namespace tl2_tp10_2023_julian_quin;

public enum Rol
{
    administrador=1,
    operador=2, 
}
public class Usuario 
{

    public int Id {get; set;}
    public string NombreDeUsuario  {get; set;}
    public string Contrasenia  {get; set;}
    internal Rol Rol  {get; set;}

    public Usuario(CrearUsuarioViewModel usuario)
    {   
        Rol = usuario.Rol;         
        NombreDeUsuario = usuario.NombreDeUsuario;
        Contrasenia = usuario.Contrasenia;
    }
    public Usuario(ModificarUsuarioViewModel usuario)
    {  
        Id = usuario.Id; 
        Rol = usuario.Rol;         
        NombreDeUsuario = usuario.NombreDeUsuario;
        Contrasenia = usuario.Contrasenia;
    }
    public Usuario(){}
}