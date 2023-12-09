using System.ComponentModel.DataAnnotations;

namespace tl2_tp10_2023_julian_quin.ViewModels;
public class TableroViewModel
{
    public int Id {get; set;} //lo necesito para los links(Editar o eliminar)
    public string Nombre { get ; set; }
    public string Descripcion { get ; set; }

    public TableroViewModel(Tablero tablero)
    {
        Id = tablero.Id;
        Nombre = tablero.Nombre;
        Descripcion = tablero.Descripcion;
    }
    
}