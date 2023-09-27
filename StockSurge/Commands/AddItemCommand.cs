using StockSurge.Models;
using StockSurge.ViewModels;

namespace StockSurge.Commands; 

public class AddItemCommand : BaseCommand {
    
    private readonly ItemManagerViewModel _itemManagerViewModel;
    
    public AddItemCommand(ItemManagerViewModel itemManagerViewModel) {
        _itemManagerViewModel = itemManagerViewModel;
    }

    public override void Execute(object parameter) {
        if (_itemManagerViewModel.AreValidInputsToAddAndRemoveItem) {
            bool isItemAdded =  HandlerModel.AddStockItem(_itemManagerViewModel.CodeToAddAndRemoveItem, _itemManagerViewModel.NameToAddAndRemoveItem,_itemManagerViewModel.QuantityInStockToAddAndRemoveItem);
            
            if (isItemAdded) {
                _itemManagerViewModel.AddAndRemoveItemStatus = $"Stock Item with Code \" {_itemManagerViewModel.CodeToAddAndRemoveItem} \" added!";
                
                _itemManagerViewModel.CodeToAddAndRemoveItem = string.Empty;
                _itemManagerViewModel.NameToAddAndRemoveItem = string.Empty;
                _itemManagerViewModel.QuantityInStockToAddAndRemoveItem = 0;
            }
            else {
                _itemManagerViewModel.AddAndRemoveItemStatus = $"Stock Item with Code \" {_itemManagerViewModel.CodeToAddAndRemoveItem} \" already exists!";
            }
        }
        else {
            _itemManagerViewModel.AddAndRemoveItemStatus = "Invalid inputs!";
        }
    }
}