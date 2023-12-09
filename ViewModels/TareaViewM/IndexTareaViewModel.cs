namespace tl2_tp10_2023_julian_quin.ViewModels;
public class IndexTareaViewModel
{
    public List<TareaViewModel> tareasView;
    public int idTablero;
    public IndexTareaViewModel(List<Tarea> tareas,int idTablero)
    {
        tareasView = new();
        foreach (var tarea in tareas)
        {
            var tareaView = new TareaViewModel(tarea);
            tareasView.Add(tareaView);  
        }
        this.idTablero = idTablero;
    }
}