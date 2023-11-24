namespace tl2_tp10_2023_julian_quin.ViewModels;

public class IndexTableroViewModel
{
    public List<TableroViewModel> tableros;

    public IndexTableroViewModel(List<Tablero> tableros)
    {
        this.tableros = new();
        foreach (var tablero in tableros)
        {
            var tableroView = new TableroViewModel(tablero);
            this.tableros.Add(tableroView);
        }

    }
}