using StockSurge.Models;

namespace StockSurge.ViewModels; 

public class StockItemViewModel : BaseViewModel {
    
    // attributes
    private readonly StockItemModel _stockItem;

    public string Code => _stockItem.GetCode();
    public string Name => _stockItem.GetName();
    public int QuantityInStock => _stockItem.GetQuantityInStock();
    
    // constructor
    public StockItemViewModel(StockItemModel stockItem) {
        _stockItem = stockItem;
    }
}