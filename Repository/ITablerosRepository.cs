namespace tl2_tp10_2023_julian_quin;
public interface ITableroRepository
{
    Tablero NuevoTablero(Tablero tablero);
    void ModificarTablero(Tablero tablero, int id);
    Tablero TableroViaId(int id);
    List<Tablero> Tableros();
    List<Tablero> TablerosDeUnUsuario(int? idUsuario);
    void EliminarTablero(int id);
    public List<Tablero> TablerosTareasUsuario(int idUsuario);
    bool ExistenTareasEnTablero(int idTablero);
}