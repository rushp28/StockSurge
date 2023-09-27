using System.Windows.Controls;
using StockSurge.ViewModels;

namespace StockSurge.Views;

public partial class ItemManagerUserControl : UserControl {
    public ItemManagerUserControl() {
        InitializeComponent();
    }

    private void CodeToAddAndRemoveItemTextBoxChanged(object sender, TextChangedEventArgs e) {
        
        ItemManagerViewModel itemManagerViewModel = (ItemManagerViewModel) DataContext;
        itemManagerViewModel.UpdateNameAndQuantityInStockToAddAndRemoveItem(sender, e);
    }
}