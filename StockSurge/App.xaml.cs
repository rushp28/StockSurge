using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using StockSurge.Commands;
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

            _navigationStore.CurrentViewModel = CreateLoginViewModel();
            
            MainWindow mainWindow = new MainWindow {
                DataContext = new MainViewModel(_navigationStore)
            };
            mainWindow.Show();

            base.OnStartup(e);
        }

        private HomeViewModel CreateHomeViewModel() {
            return new HomeViewModel(_navigationStore, CreateItemManagerViewModel, CreateTransactionLogListViewModel, CreateStockListViewModel);
        }

        private ItemManagerViewModel CreateItemManagerViewModel() {
            return new ItemManagerViewModel(_navigationStore, CreateHomeViewModel);
        }
        
        private TransactionLogListViewModel CreateTransactionLogListViewModel() {
            return new TransactionLogListViewModel(_navigationStore, CreateHomeViewModel);
        }

        private StockListViewModel CreateStockListViewModel() {
            return new StockListViewModel(_navigationStore, CreateHomeViewModel);
        }

        private LoginViewModel CreateLoginViewModel() {
            return new LoginViewModel(_navigationStore, CreateHomeViewModel);
        }
    }
}