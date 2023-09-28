using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using StockSurge.Commands;
using StockSurge.Models;
using StockSurge.Stores;

namespace StockSurge.ViewModels; 

public class StockListViewModel : BaseViewModel {
    
    // attributes
    private readonly ObservableCollection<StockItemViewModel> _allStockItems;

    public IEnumerable<StockItemViewModel> AllStockItems => _allStockItems;
    
    public ICommand NavigateToHomeCommand { get; }
    
    // constructor
    public StockListViewModel(NavigationStore navigationStore, Func<HomeViewModel> createHomeViewModel) {

        NavigateToHomeCommand = new NavigateCommand(navigationStore, createHomeViewModel);
        
        _allStockItems = new ObservableCollection<StockItemViewModel>();

        List<StockItemModel>? allStockItems = HandlerModel.GetAllStockItems();

        if (allStockItems != null) {
            foreach (StockItemModel stockItem in allStockItems) {
                _allStockItems.Add(new StockItemViewModel(stockItem));
            }
        }
    }
}