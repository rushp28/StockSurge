using System;

namespace StockSurge.Models; 

public static class HandlerModel {
    
    // add a transaction log method
    private static void AddTransactionLog(string transactionType, StockItemModel stockItem, int changedQuantity, int newQuantityInStock) {
        string logDateTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
        
        TransactionLogModel transactionLog = new TransactionLogModel(logDateTime, transactionType, stockItem, changedQuantity, newQuantityInStock);
    }
    
    //
}