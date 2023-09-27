namespace StockSurge.Models; 

public class TransactionLogModel {
    
    // attributes
    private readonly string _logDateTime;
    private readonly string _transactionType;
    private readonly string _stockItemCode;
    private readonly string _stockItemName;
    private readonly int _changedQuantity;
    private readonly int _newQuantityInStock;
    
    // constructor
    public TransactionLogModel(string logDateTime, string transactionType, StockItemModel stockItem, int changedQuantity, int newQuantityInStock) {
        _logDateTime = logDateTime;
        _transactionType = transactionType;
        _stockItemCode = stockItem.GetCode();
        _stockItemName = stockItem.GetName();
        _changedQuantity = changedQuantity;
        _newQuantityInStock = newQuantityInStock;
    }
    
    // getters
    public string GetLogDateTime() {
        return _logDateTime;
    }

    public string GetTransactionType() {
        return _transactionType;
    }

    public string GetStockItemCode() {
        return _stockItemCode;
    }

    public string GetStockItemName() {
        return _stockItemName;
    }

    public int GetChangedQuantity() {
        return _changedQuantity;
    }

    public int GetNewQuantityInStock() {
        return _newQuantityInStock;
    }
}