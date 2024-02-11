namespace tl2_tp10_2023_julian_quin.ViewModels;

public class IndexTableroViewModel
{
    public List<TableroViewModel> TablerosPropios = new();
    public List<TableroViewModel> TablerosNoPropios = new();

    public IndexTableroViewModel(List<Tablero> tablerosPropios, List<Tablero> tablerosNoPropios)
    {
        foreach (var tablero in tablerosPropios)
        {
            var tableroView = new TableroViewModel(tablero);
            TablerosPropios.Add(tableroView);
        }
        foreach (var tablero in tablerosNoPropios)
        {
            var tableroView = new TableroViewModel(tablero);
            TablerosNoPropios.Add(tableroView);
        }
    }
    public IndexTableroViewModel(List<Tablero> tablerosPropios)
    {
        foreach (var tablero in tablerosPropios)
        {
            var tableroView = new TableroViewModel(tablero);
            TablerosPropios.Add(tableroView);
        }
    }
}