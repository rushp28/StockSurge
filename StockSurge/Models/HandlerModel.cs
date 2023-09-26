using System;

namespace StockSurge.Models; 

public static class HandlerModel {
    
    // add a transaction log method
    private static void AddTransactionLog(string transactionType, StockItemModel stockItem, int changedQuantity, int newQuantityInStock) {
        
        string logDateTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
        
        TransactionLogModel transactionLog = new TransactionLogModel(logDateTime, transactionType, stockItem, changedQuantity, newQuantityInStock);

        DatabaseHandlerModel.InsertTransactionLog(transactionLog);
    }
    
    // add a stock item method
    public static bool AddStockItem(string code, string name, int quantityInStock) {
        
        StockItemModel stockItem = new StockItemModel(code, name, quantityInStock);
        
        if (DatabaseHandlerModel.GetStockItemByCode(stockItem.GetCode()) == null) {
            
            DatabaseHandlerModel.InsertStockItem(stockItem);
            AddTransactionLog("Item Added", stockItem, stockItem.GetQuantityInStock(), stockItem.GetQuantityInStock());
            
            return true;
        }

        return false;
    }
    
    // remove a stock item method
    public static bool RemoveStockItem(string code) {
        
        StockItemModel? stockItem = DatabaseHandlerModel.GetStockItemByCode(code);

        if (stockItem != null) {
            
            DatabaseHandlerModel.DeleteStockItem(stockItem);
            AddTransactionLog("Item Removed", stockItem, 0, stockItem.GetQuantityInStock());

            return true;
        }

        return false;
    }
    
    // add quantity to stock item's quantity in stock method
    public static bool AddStockItemQuantity(string code, int quantityToAdd) {
        
        StockItemModel? stockItem = DatabaseHandlerModel.GetStockItemByCode(code);
        
        if (stockItem != null) {
            
            int newQuantityInStock = stockItem.GetQuantityInStock() + quantityToAdd;
            stockItem.SetQuantityInStock(newQuantityInStock);
            
            DatabaseHandlerModel.UpdateStockItemQuantity(stockItem);
            AddTransactionLog("Quantity Added", stockItem, quantityToAdd, newQuantityInStock);
            
            return true;
        }

        return false;
    }
    
    // remove quantity to stock item's quantity in stock method
    public static bool RemoveStockItemQuantity(string code, int quantityToRemove) {
        
        StockItemModel? stockItem = DatabaseHandlerModel.GetStockItemByCode(code);
        
        if (stockItem != null) {
            
            int newQuantityInStock = stockItem.GetQuantityInStock() - quantityToRemove;
            stockItem.SetQuantityInStock(newQuantityInStock);
            
            DatabaseHandlerModel.UpdateStockItemQuantity(stockItem);
            AddTransactionLog("Quantity Removed", stockItem, quantityToRemove, newQuantityInStock);
            
            return true;
        }
        
        return false;
    }
}