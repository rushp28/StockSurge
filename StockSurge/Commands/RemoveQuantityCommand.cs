using StockSurge.Models;
using StockSurge.ViewModels;

namespace StockSurge.Commands; 

public class RemoveQuantityCommand : BaseCommand {
    
    private readonly ItemManagerViewModel _itemManagerViewModel;
    
    public RemoveQuantityCommand(ItemManagerViewModel itemManagerViewModel) {
        _itemManagerViewModel = itemManagerViewModel;
    }
    
    public override void Execute(object parameter) {
        if (_itemManagerViewModel.AreValidInputsToUpdateQuantity) {
            bool isQuantityAdded =  HandlerModel.RemoveStockItemQuantity(_itemManagerViewModel.CodeToUpdateQuantity, _itemManagerViewModel.ChangedQuantityToUpdateQuantity);
            
            if (isQuantityAdded) {
                _itemManagerViewModel.UpdateQuantityStatus = $"Quantity of Stock Item with Code \" {_itemManagerViewModel.CodeToUpdateQuantity} \" decreased by {_itemManagerViewModel.ChangedQuantityToUpdateQuantity}!";
                
                _itemManagerViewModel.CodeToUpdateQuantity = string.Empty;
                _itemManagerViewModel.ChangedQuantityToUpdateQuantity = 0;
            }
            else {
                _itemManagerViewModel.UpdateQuantityStatus = $"Stock Item with Code \" {_itemManagerViewModel.CodeToUpdateQuantity} \" does not exist!";
            }
        }
        else {
            _itemManagerViewModel.UpdateQuantityStatus = "Invalid inputs!";
        }
    }
}