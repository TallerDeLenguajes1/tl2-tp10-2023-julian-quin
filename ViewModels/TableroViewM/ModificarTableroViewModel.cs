using System.ComponentModel.DataAnnotations;

namespace tl2_tp10_2023_julian_quin.ViewModels;

public class ModificarTableroViewModel
{

    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Nombre")]
    public string Nombre { get ; set; }

    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Descripcion")]
     public string Descripcion { get ; set; }

    [Required(ErrorMessage = "Este campo es requerido")]
    [Display(Name = "Propietario")]
     public int IdUsuarioPropietario { get; set; } //tengo que conservarlo aunque no lo use, pues si no está... tendrá un cero en este campo y cuando se llegue al endpoint [post] de modifcar se guardar propietario "0" reemplazando el que tenia


    public List<Usuario> Usuarios;
    public int Id {get; set;}  // lo necesito para el link que lleva al get (se manda el ID para identificar el tablero desde ahí)
    

    // o sea por lo general tienen que estar todos los campos del modelo original
    // es solo que no muestro o pido algunos de ellos!

    public ModificarTableroViewModel(Tablero tablero)
    {
        Id = tablero.Id;  // lo necesito para el link que me lleva al [get] modificar en donde se manda el id al metodo para que se identifique el tablero a modificar
        IdUsuarioPropietario = tablero.IdUsuarioPropietario;
        Nombre = tablero.Nombre;
        Descripcion = tablero.Descripcion;
    }
    public ModificarTableroViewModel(){}
  // public int IdUsuarioPropietario { get; set; } si no pusiese este campo, cuando cree una instancia de esta clase voy a perder el datos del idPropietario que llega al constructor por medio del objeto Tablero,
  //luego cuando quiero convertir este objeto desde la clase Tablero el campo idUsuarioPropietario será cero, pues se pondrá por defecto reemplazando el que ya tenia
    
    
}