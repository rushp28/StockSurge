using System;
using StockSurge.Stores;
using StockSurge.ViewModels;

namespace StockSurge.Commands; 

public class NavigateCommand : BaseCommand {
    
    // attributes
    private readonly NavigationStore _navigationStore;
    private readonly Func<BaseViewModel> _createViewModel;

    // constructor
    public NavigateCommand(NavigationStore navigationStore, Func<BaseViewModel> createViewModel) {
        _navigationStore = navigationStore;
        _createViewModel = createViewModel;
    }

    public override void Execute(object parameter) {

        _navigationStore.CurrentViewModel = _createViewModel();
    }
}