using System;
using System.Threading.Tasks;
using System.Windows.Input;
using StockSurge.Commands;
using StockSurge.Stores;

namespace StockSurge.ViewModels; 

public class LoginViewModel : BaseViewModel {
    
    // attributes
    private string _username;
    private string _password;

    private string _loginStatus;
    
    // getters
    public string Username {
        get => _username;
        set {
            _username = value;
            OnPropertyChanged(nameof(Username));
        }
    }
    public string Password {
        get => _password;
        set {
            _password = value;
            OnPropertyChanged(nameof(Password));
        }
    }
    
    // statuses
    public string LoginStatus {
        get => _loginStatus;
        set {
            _loginStatus = value;
            OnPropertyChanged(nameof(LoginStatus));
            ClearStatusAfterDelay(nameof(LoginStatus));
        }
    }
    
    // null, empty and negative input checks
    public bool AreValidInputsToLogin => 
        !string.IsNullOrWhiteSpace(_username) &&
        !string.IsNullOrWhiteSpace(_password);

    // clear status after delay method
    private async void ClearStatusAfterDelay(string propertyName)
    {
        await Task.Delay(1500);
        _loginStatus = string.Empty;
        OnPropertyChanged(nameof(_loginStatus));
    }
    
    // commands
    public ICommand LoginCommand { get; }

    public LoginViewModel(NavigationStore navigationStore, Func<HomeViewModel> createHomeViewModel) {
        
        NavigateCommand navigateCommand = new NavigateCommand(navigationStore, createHomeViewModel);
        
        LoginCommand = new LoginCommand(this, navigationStore, createHomeViewModel, navigateCommand);
    }
}