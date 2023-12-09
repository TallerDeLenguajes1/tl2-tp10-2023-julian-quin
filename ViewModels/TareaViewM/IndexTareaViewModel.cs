namespace tl2_tp10_2023_julian_quin.ViewModels;
public class IndexTareaViewModel
{
    public List<TareaViewModel> tareasView;
    public IndexTareaViewModel(List<Tarea> tareas)
    {
        tareasView = new();
        foreach (var tarea in tareas)
        {
            var tareaView = new TareaViewModel(tarea);
            tareasView.Add(tareaView);  
        }
    }
}