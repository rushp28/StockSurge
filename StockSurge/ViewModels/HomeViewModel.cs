using System;
using System.Windows.Input;
using StockSurge.Commands;
using StockSurge.Stores;

namespace StockSurge.ViewModels; 

public class HomeViewModel : BaseViewModel {
    
    public ICommand NavigateToItemManagerCommand { get; }
    public ICommand NavigateToTransactionListCommand { get; }
    public ICommand NavigateToStockListCommand { get; }

    public HomeViewModel(NavigationStore navigationStore, Func<ItemManagerViewModel> createItemManagerViewModel, Func<TransactionLogListViewModel> createTransactionLogListViewModel) {

        NavigateToItemManagerCommand = new NavigateCommand(navigationStore, createItemManagerViewModel);
        NavigateToTransactionListCommand = new NavigateCommand(navigationStore, createTransactionLogListViewModel);
    }
}