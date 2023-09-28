using System;
using System.Collections.Generic;

namespace StockSurge.Models; 

public static class HandlerModel {
    
    // verify user method
    public static bool VerifyUser(string username, string password) {

        UserModel user = new UserModel(username, password);

        if (DatabaseHandlerModel.VerifyUser(user)) {
            return true;
        }
        else {
            return false;
        }
    }
    
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
    public static string RemoveStockItemQuantity(string code, int quantityToRemove) {
        
        StockItemModel? stockItem = DatabaseHandlerModel.GetStockItemByCode(code);
        
        if (stockItem != null) {

            if (stockItem.GetQuantityInStock() < quantityToRemove) {
                return "Insufficient";
            }
            else {
                int newQuantityInStock = stockItem.GetQuantityInStock() - quantityToRemove;
                stockItem.SetQuantityInStock(newQuantityInStock);
            
                DatabaseHandlerModel.UpdateStockItemQuantity(stockItem);
                AddTransactionLog("Quantity Removed", stockItem, quantityToRemove, newQuantityInStock);
            
                return "Yes";
            }
        }
        
        return "No";
    }
    
    // get all stock items method
    public static List<StockItemModel>? GetAllStockItems() {
        
        return DatabaseHandlerModel.GetAllStockItems();
    }
    
    // get all transaction logs method
    public static List<TransactionLogModel>? GetAllTransactionLogs() {
        
        return DatabaseHandlerModel.GetAllTransactionLogs();
    }
    
    // get stock item by code method
    public static StockItemModel? GetStockItemByCode(string code) {
        
        return DatabaseHandlerModel.GetStockItemByCode(code);
    }
}