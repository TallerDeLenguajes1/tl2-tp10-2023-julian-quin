using tl2_tp10_2023_julian_quin.ViewModels;

namespace tl2_tp10_2023_julian_quin;
public class Tablero 
{
    private int id;
    private int idUsuarioPropietario;
    private string nombre;
    private string descripcion;

    public int Id { get => id; set => id = value; }
    public int IdUsuarioPropietario { get => idUsuarioPropietario; set => idUsuarioPropietario = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }

    public Tablero (CrearTableroViewModel tableroView)
    {
        this.idUsuarioPropietario = tableroView.IdUsuarioPropietario;
        this.nombre = tableroView.Nombre;
        this.descripcion = tableroView.Descripcion;
    }
    public Tablero (ModificarTableroViewModel tableroView)
    {
        this.id = tableroView.Id;
        this.idUsuarioPropietario = tableroView.IdUsuarioPropietario;
        this.nombre = tableroView.Nombre;
        this.descripcion = tableroView.Descripcion;
    }
    public Tablero(){}

    
}