using StockSurge.Models;
using StockSurge.ViewModels;

namespace StockSurge.Commands; 

public class AddQuantityCommand : BaseCommand {
    
    private readonly ItemManagerViewModel _itemManagerViewModel;
    
    public AddQuantityCommand(ItemManagerViewModel itemManagerViewModel) {
        _itemManagerViewModel = itemManagerViewModel;
    }
    
    public override void Execute(object parameter) {
        if (_itemManagerViewModel.AreValidInputsToUpdateQuantity) {
            bool isQuantityAdded =  HandlerModel.AddStockItemQuantity(_itemManagerViewModel.CodeToUpdateQuantity, _itemManagerViewModel.ChangedQuantityToUpdateQuantity);
            
            if (isQuantityAdded) {
                _itemManagerViewModel.UpdateQuantityStatus = $"Quantity of Stock Item with Code \" {_itemManagerViewModel.CodeToUpdateQuantity} \" increased by {_itemManagerViewModel.ChangedQuantityToUpdateQuantity}!";
                
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