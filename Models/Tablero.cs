using tl2_tp10_2023_julian_quin.ViewModels;

namespace tl2_tp10_2023_julian_quin;
public class Tablero 
{
    public int Id { get; set; }
    public int IdUsuarioPropietario { get; set; }
    public string NombrePropietario {get;set;} // simpre está en null, se completa solo en un traerTableros()
    public string Nombre { get; set; }
    public string Descripcion { get; set ; }

    public Tablero (CrearTableroViewModel tableroView)
    {
        IdUsuarioPropietario = tableroView.IdUsuarioPropietario; 
        Nombre = tableroView.Nombre;
        Descripcion = tableroView.Descripcion;
    }
    public Tablero (ModificarTableroViewModel tableroView)
    {
        Id = tableroView.Id;
        IdUsuarioPropietario = tableroView.IdUsuarioPropietario;
        Nombre = tableroView.Nombre;
        Descripcion = tableroView.Descripcion;
    }
    public Tablero(){}

    
}