using System;
using StockSurge.ViewModels;

namespace StockSurge.Stores;

public class NavigationStore {
    
    // attributes
    private BaseViewModel _currentViewModel;
    
    public BaseViewModel CurrentViewModel {
        get => _currentViewModel;
        set {
            _currentViewModel = value;
            OnCurrentViewModelChanged();
        }
    }

    public event Action CurrentViewModelChanged;

    protected virtual void OnCurrentViewModelChanged() {
        CurrentViewModelChanged?.Invoke();
    }
}