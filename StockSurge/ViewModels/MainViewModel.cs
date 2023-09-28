using StockSurge.Stores;

namespace StockSurge.ViewModels; 

public class MainViewModel : BaseViewModel {
    
    private NavigationStore _navigationStore;

    public BaseViewModel CurrentViewModel => _navigationStore.CurrentViewModel;
    
    public MainViewModel(NavigationStore navigationStore) {
        _navigationStore = navigationStore;

        _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
    }

    private void OnCurrentViewModelChanged() {
        OnPropertyChanged(nameof(CurrentViewModel));
    }
}