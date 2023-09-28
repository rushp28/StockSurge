using System;
using System.Windows.Input;
using StockSurge.Models;
using StockSurge.Stores;
using StockSurge.ViewModels;

namespace StockSurge.Commands; 

public class LoginCommand : BaseCommand{
    
    // attributes
    private readonly LoginViewModel _loginViewModel;
    private readonly NavigateCommand _navigateCommand;

    public LoginCommand(LoginViewModel loginViewModel, NavigationStore navigationStore, Func<HomeViewModel> createHomeViewModel, NavigateCommand navigateCommand)
    {
        _loginViewModel = loginViewModel;
        _navigateCommand = navigateCommand; 
    }
    
    public override void Execute(object parameter) {

        if (_loginViewModel.AreValidInputsToLogin) {
            if (HandlerModel.VerifyUser(_loginViewModel.Username, _loginViewModel .Password)) {
                _navigateCommand.Execute(null);
            }
            else {
                _loginViewModel.LoginStatus = "Incorrect Username or Password!";
            }
        }
        else {
            _loginViewModel.LoginStatus = "Enter the Username or Password!";
        }
 
    }
}