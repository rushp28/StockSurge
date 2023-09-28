using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using StockSurge.Stores;
using StockSurge.ViewModels;

namespace StockSurge {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {

        private readonly NavigationStore _navigationStore;

        public App() {
            _navigationStore = new NavigationStore();
        }
        
        protected override void OnStartup(StartupEventArgs e) {

            _navigationStore.CurrentViewModel = CreateHomeViewModel();
            
            MainWindow mainWindow = new MainWindow {
                DataContext = new MainViewModel(_navigationStore)
            };
            mainWindow.Show();

            base.OnStartup(e);
        }

        private HomeViewModel CreateHomeViewModel() {
            return new HomeViewModel(_navigationStore, CreateItemManagerViewModel, CreateTransactionLogListViewModel);
        }

        private ItemManagerViewModel CreateItemManagerViewModel() {
            return new ItemManagerViewModel(_navigationStore, CreateHomeViewModel);
        }
        
        private TransactionLogListViewModel CreateTransactionLogListViewModel() {
            return new TransactionLogListViewModel(_navigationStore, CreateHomeViewModel);
        }
    }
}