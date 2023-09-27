namespace StockSurge.ViewModels; 

public class MainViewModel : BaseViewModel {
    public BaseViewModel CurrentViewModel { get; }
    
    public MainViewModel() {
        CurrentViewModel = new ItemManagerViewModel();
    }
}