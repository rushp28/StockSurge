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
            string isQuantityRemoved =  HandlerModel.RemoveStockItemQuantity(_itemManagerViewModel.CodeToUpdateQuantity, _itemManagerViewModel.ChangedQuantityToUpdateQuantity);

            switch (isQuantityRemoved) {
                case "Yes":
                    _itemManagerViewModel.UpdateQuantityStatus = $"Quantity of Stock Item with Code \" {_itemManagerViewModel.CodeToUpdateQuantity} \" decreased by {_itemManagerViewModel.ChangedQuantityToUpdateQuantity}!";
                
                    _itemManagerViewModel.CodeToUpdateQuantity = string.Empty;
                    _itemManagerViewModel.ChangedQuantityToUpdateQuantity = 0;
                    
                    break;
                case "Insufficient":
                    _itemManagerViewModel.UpdateQuantityStatus = $"Stock Item with Code \" {_itemManagerViewModel.CodeToUpdateQuantity} \" does not have sufficient quantity!";
                    
                    break;
                default:
                    _itemManagerViewModel.UpdateQuantityStatus = $"Stock Item with Code \" {_itemManagerViewModel.CodeToUpdateQuantity} \" does not exist!";
                    
                    break;
            }
        }
        else {
            _itemManagerViewModel.UpdateQuantityStatus = "Invalid inputs!";
        }
    }
}