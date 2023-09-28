using StockSurge.Models;

namespace StockSurge.ViewModels; 

public class TransactionLogViewModel : BaseViewModel {
    
    // attributes
    private readonly TransactionLogModel _transactionLog;

    public string LogDateTime => _transactionLog.GetLogDateTime();
    public string TransactionType => _transactionLog.GetTransactionType();
    public string StockItemCode => _transactionLog.GetStockItemCode();
    public string StockItemName => _transactionLog.GetStockItemName();
    public int ChangedQuantity => _transactionLog.GetChangedQuantity();
    public int NewQuantityInStock => _transactionLog.GetNewQuantityInStock();
    
    // constructor
    public TransactionLogViewModel(TransactionLogModel transactionLog) {
        _transactionLog = transactionLog;
    }
}