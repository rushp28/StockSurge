using System;

namespace StockSurge.Models; 

public static class HandlerModel {
    
    // add a transaction log method
    private static void AddTransactionLog(string transactionType, StockItemModel stockItem, int changedQuantity, int newQuantityInStock) {
        
        string logDateTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
        
        TransactionLogModel transactionLog = new TransactionLogModel(logDateTime, transactionType, stockItem, changedQuantity, newQuantityInStock);

        DatabaseHandler.InsertTransactionLog(transactionLog);
    }
    
    // add a stock item method
    public static bool AddStockItem(string code, string name, int quantityInStock) {
        
        StockItemModel stockItem = new StockItemModel(code, name, quantityInStock);
        
        if (DatabaseHandler.GetStockItemByCode(stockItem.GetCode()) == null) {
            DatabaseHandler.InsertStockItem(stockItem);
            AddTransactionLog("Item Added", stockItem, stockItem.GetQuantityInStock(), stockItem.GetQuantityInStock());
            
            return true;
        }

        return false;
    }
    
    // remove a stock item
    public static bool RemoveStockItem(string code) {
        
        StockItemModel? stockItem = DatabaseHandler.GetStockItemByCode(code);

        if (stockItem != null) {
            DatabaseHandler.DeleteStockItem(stockItem);
            AddTransactionLog("Item Removed", stockItem, 0, stockItem.GetQuantityInStock());

            return true;
        }

        return false;
    }
    
    // add quantity to stock item's quantity in stock
    public static bool AddStockItemQuantity(string code, int quantityToAdd) {
        
        StockItemModel? stockItem = DatabaseHandler.GetStockItemByCode(code);
        
        if (stockItem != null) {
            int newQuantityInStock = stockItem.GetQuantityInStock() + quantityToAdd;
            stockItem.SetQuantityInStock(newQuantityInStock);
            
            DatabaseHandler.UpdateStockItemQuantity(stockItem);
            AddTransactionLog("Quantity Added", stockItem, quantityToAdd, newQuantityInStock);
            
            return true;
        }

        return false;
    }
    
    // remove quantity to stock item's quantity in stock
    public static bool RemoveStockItemQuantity(string code, int quantityToRemove) {
        
        StockItemModel? stockItem = DatabaseHandler.GetStockItemByCode(code);
        
        if (stockItem != null) {
            int newQuantityInStock = stockItem.GetQuantityInStock() - quantityToRemove;
            stockItem.SetQuantityInStock(newQuantityInStock);
            
            DatabaseHandler.UpdateStockItemQuantity(stockItem);
            AddTransactionLog("Quantity Removed", stockItem, quantityToRemove, newQuantityInStock);
            
            return true;
        }
        
        return false;
    }
}