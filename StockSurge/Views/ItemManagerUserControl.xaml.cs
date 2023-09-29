using System;
using System.Windows;
using System.Windows.Controls;
using StockSurge.ViewModels;

namespace StockSurge.Views;

public partial class ItemManagerUserControl : UserControl {
    public ItemManagerUserControl() {
        InitializeComponent();
    }

    private void CodeToAddAndRemoveItemTextBoxChanged(object sender, TextChangedEventArgs e) {
        var itemManagerViewModel = (ItemManagerViewModel)DataContext;
        try {
            itemManagerViewModel.UpdateNameAndQuantityInStockToAddAndRemoveItem(sender, e);
        }
        catch (Exception exception) {
        }
    }

    private void MinimizeCommand(object sender, RoutedEventArgs e) {
        if (Application.Current.MainWindow is { } mainWindow) mainWindow.WindowState = WindowState.Minimized;
    }

    private void CloseButton(object sender, RoutedEventArgs e) {
        if (Application.Current.MainWindow is { } mainWindow) mainWindow.Close();
    }
}