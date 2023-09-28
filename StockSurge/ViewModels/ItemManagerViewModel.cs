using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using StockSurge.Commands;
using StockSurge.Models;
using StockSurge.Stores;

namespace StockSurge.ViewModels; 

public class ItemManagerViewModel : BaseViewModel {
    
    // attributes
    private string _codeToAddAndRemoveItem;
    private string _nameToAddAndRemoveItem;
    private int _quantityInStockToAddAndRemoveItem;
    private string _addAndRemoveItemStatus;
    
    private string _codeToUpdateQuantity;
    private int _changedQuantityToUpdateQuantity;
    private string _updateQuantityStatus;

    // getters and setters
    public string CodeToAddAndRemoveItem {
        get => _codeToAddAndRemoveItem;
        set {
            if (string.IsNullOrEmpty(value)) {
                NameToAddAndRemoveItem = string.Empty;
                QuantityInStockToAddAndRemoveItem = 0;
            } 
            else {
                _codeToAddAndRemoveItem = value;

                StockItemModel stockItem = HandlerModel.GetStockItemByCode(value);
                if (stockItem != null) {
                    NameToAddAndRemoveItem = stockItem.GetName();
                    QuantityInStockToAddAndRemoveItem = stockItem.GetQuantityInStock();
                }

                OnPropertyChanged(nameof(CodeToAddAndRemoveItem));
            }
        }
    }

    public string NameToAddAndRemoveItem {
        get => _nameToAddAndRemoveItem;
        set {
            _nameToAddAndRemoveItem = value;
                OnPropertyChanged(nameof(NameToAddAndRemoveItem));
        }
    }

    public int QuantityInStockToAddAndRemoveItem {
        get => _quantityInStockToAddAndRemoveItem;
        set {
            _quantityInStockToAddAndRemoveItem = value;
            OnPropertyChanged(nameof(QuantityInStockToAddAndRemoveItem));
        }
    }
    
    public string CodeToUpdateQuantity {
        get => _codeToUpdateQuantity;
        set {
            _codeToUpdateQuantity = value;
                OnPropertyChanged(nameof(CodeToUpdateQuantity));
        }
    }

    public int ChangedQuantityToUpdateQuantity {
        get => _changedQuantityToUpdateQuantity;
        set {
            _changedQuantityToUpdateQuantity = value;
            OnPropertyChanged(nameof(ChangedQuantityToUpdateQuantity));
        }
    }
    
    // statuses
    public string AddAndRemoveItemStatus {
        get => _addAndRemoveItemStatus;
        set {
            _addAndRemoveItemStatus = value;
            OnPropertyChanged(nameof(AddAndRemoveItemStatus));
            ClearStatusAfterDelay(nameof(AddAndRemoveItemStatus));
        }
    }

    public string UpdateQuantityStatus {
        get => _updateQuantityStatus;
        set {
            _updateQuantityStatus = value;
            OnPropertyChanged(nameof(UpdateQuantityStatus));
            ClearStatusAfterDelay(nameof(UpdateQuantityStatus));
        }
    }
    
    // clear status after delay method
    private async void ClearStatusAfterDelay(string propertyName)
    {
        await Task.Delay(1500);
        switch (propertyName)
        {
            case nameof(AddAndRemoveItemStatus):
                _addAndRemoveItemStatus = string.Empty;
                OnPropertyChanged(nameof(AddAndRemoveItemStatus));
                break;
            case nameof(UpdateQuantityStatus):
                _updateQuantityStatus = string.Empty;
                OnPropertyChanged(nameof(UpdateQuantityStatus));
                break;
        }
    }
    
    // regex expressions
    private const string AlphabetPattern = @"^[A-Za-z]+$";

    // null, empty and negative input checks
    public bool AreValidInputsToAddAndRemoveItem => 
        !string.IsNullOrWhiteSpace(_codeToAddAndRemoveItem) &&
        !string.IsNullOrWhiteSpace(_nameToAddAndRemoveItem) &&
        Regex.IsMatch(_nameToAddAndRemoveItem, AlphabetPattern) &&
        int.TryParse(Convert.ToString(_quantityInStockToAddAndRemoveItem), out int quantity) && quantity > 0;

    public bool AreValidInputsToUpdateQuantity => 
        !string.IsNullOrWhiteSpace(_codeToUpdateQuantity) &&
        int.TryParse(Convert.ToString(_changedQuantityToUpdateQuantity), out int quantity) && quantity > 0;
    
    // commands
    public ICommand AddItemCommand { get; }
    public ICommand RemoveItemCommand { get; }
    public ICommand AddQuantityCommand { get; }
    public ICommand RemoveQuantityCommand { get; }
    public ICommand NavigateToHomeCommand { get; }
    

    public void UpdateNameAndQuantityInStockToAddAndRemoveItem(object sender, TextChangedEventArgs e) {
        
        string currentCode = ((TextBox)sender).Text;
        
        StockItemModel stockItem = HandlerModel.GetStockItemByCode(currentCode);
        
        if (stockItem != null) {
            NameToAddAndRemoveItem = stockItem.GetName();
            QuantityInStockToAddAndRemoveItem = stockItem.GetQuantityInStock();
        }
    }

    // constructor
    public ItemManagerViewModel(NavigationStore navigationStore, Func<HomeViewModel> createHomeViewModel) {
        
        AddItemCommand = new AddItemCommand(this);
        RemoveItemCommand = new RemoveItemCommand(this);
        AddQuantityCommand = new AddQuantityCommand(this);
        RemoveQuantityCommand = new RemoveQuantityCommand(this);
        
        NavigateToHomeCommand = new NavigateCommand(navigationStore, createHomeViewModel);
    }
}