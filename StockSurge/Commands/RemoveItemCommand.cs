using StockSurge.Models;
using StockSurge.ViewModels;

namespace StockSurge.Commands; 

public class RemoveItemCommand : BaseCommand{
    
    private readonly ItemManagerViewModel _itemManagerViewModel;
    
    public RemoveItemCommand(ItemManagerViewModel itemManagerViewModel) {
        _itemManagerViewModel = itemManagerViewModel;
    }
    
    public override void Execute(object parameter) {
        if (_itemManagerViewModel.AreValidInputsToAddAndRemoveItem) {
            bool isItemRemoved = HandlerModel.RemoveStockItem(_itemManagerViewModel.CodeToAddAndRemoveItem);
            
            if (isItemRemoved) {
                _itemManagerViewModel.AddAndRemoveItemStatus = $"Stock Item with Code \" {_itemManagerViewModel.CodeToAddAndRemoveItem} \" removed!";
                
                _itemManagerViewModel.CodeToAddAndRemoveItem = string.Empty;
                _itemManagerViewModel.NameToAddAndRemoveItem = string.Empty;
                _itemManagerViewModel.QuantityInStockToAddAndRemoveItem = 0;
            }
            else {
                _itemManagerViewModel.AddAndRemoveItemStatus = $"Stock Item with Code \" {_itemManagerViewModel.CodeToAddAndRemoveItem} \" does not exist!";
            }
        }
        else {
            _itemManagerViewModel.AddAndRemoveItemStatus = "Invalid inputs!";
        }
    }
}