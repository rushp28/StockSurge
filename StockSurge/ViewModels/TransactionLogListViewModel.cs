using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Documents;
using System.Windows.Input;
using StockSurge.Commands;
using StockSurge.Models;
using StockSurge.Stores;

namespace StockSurge.ViewModels;

public class TransactionLogListViewModel : BaseViewModel {

    // attributes
    private readonly ObservableCollection<TransactionLogViewModel> _allTransactionsLogList;

    public IEnumerable<TransactionLogViewModel> AllTransactionLogList => _allTransactionsLogList;
    
    public ICommand NavigateToHomeCommand { get; }

    // constructor
    public TransactionLogListViewModel(NavigationStore navigationStore, Func<HomeViewModel> createHomeViewModel) {

        NavigateToHomeCommand = new NavigateCommand(navigationStore, createHomeViewModel);
        
        _allTransactionsLogList = new ObservableCollection<TransactionLogViewModel>();

        List<TransactionLogModel>? allTransactionLogs = HandlerModel.GetAllTransactionLogs();

        if (allTransactionLogs != null) {
            foreach (TransactionLogModel transactionLog in allTransactionLogs) {
                _allTransactionsLogList.Add(new TransactionLogViewModel(transactionLog));
            }
        }
    }
    
}