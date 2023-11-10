namespace tl2_tp10_2023_julian_quin;
public interface ITableroRepository
{
    Tablero NuevoTablero(Tablero tablero);
    void ModificarTablero(Tablero tablero, int id);
    Tablero TableroId(int id);
    List<Tablero> Tableros();
    List<Tablero> TablerosUsuario(int idUsuario);
    void EliminarTablero(int id);
}