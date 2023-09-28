﻿using System.Windows;
using System.Windows.Controls;

namespace StockSurge.Views; 

public partial class HomeUserControl : UserControl {
    
    // constructor
    public HomeUserControl() {
        InitializeComponent();
    }
    
    private void MinimizeCommand(object sender, RoutedEventArgs e)
    {
        if (Application.Current.MainWindow is { } mainWindow)
        {
            mainWindow.WindowState = WindowState.Minimized;
        }
    }

    private void CloseButton(object sender, RoutedEventArgs e)
    {
        if (Application.Current.MainWindow is { } mainWindow)
        {
            mainWindow.Close();
        }
    }
}