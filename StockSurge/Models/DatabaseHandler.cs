using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Data.Sqlite;
using SQLitePCL;

namespace StockSurge.Models; 

public static class DatabaseHandler {
    
    // get a sqlite connection method
    private static SqliteConnection GetSqliteConnection() {
        
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        for (int i = 0; i < 5; i++)
        {
            baseDirectory = Directory.GetParent(baseDirectory).FullName;
        }
        string absolutePath = Path.Combine(baseDirectory, "StockSurge.sqlite");
        
        string connectionString = new SqliteConnectionStringBuilder {
            DataSource = absolutePath,
            Mode = SqliteOpenMode.ReadWriteCreate
        }.ToString();
        
        Batteries.Init();
        raw.SetProvider(new SQLite3Provider_e_sqlite3());
        
        return new SqliteConnection(connectionString);
    }

    // insert a transaction log into sqlite database method
    public static void InsertTransactionLog(TransactionLogModel transactionLog) {
        
        const string insertTransactionLogQuery = "INSERT INTO transaction_logs (log_date_time, transaction_type, stock_item_code, stock_item_name, changed_quantity, new_quantity_in_stock) VALUES (@logTime, @transactionType, @stockItemCode, @stockItemName, @changedQuantity, @newQuantityInStock)";
        
        using SqliteConnection sqliteConnection = GetSqliteConnection();
        sqliteConnection.Open();
        
        using SqliteCommand sqliteCommand = new SqliteCommand(insertTransactionLogQuery, sqliteConnection);
        sqliteCommand.Parameters.AddWithValue("@logTime", transactionLog.GetLogDateTime());
        sqliteCommand.Parameters.AddWithValue("@transactionType", transactionLog.GetTransactionType());
        sqliteCommand.Parameters.AddWithValue("@stockItemCode", transactionLog.GetStockItemCode());
        sqliteCommand.Parameters.AddWithValue("@stockItemName", transactionLog.GetStockItemName());
        sqliteCommand.Parameters.AddWithValue("@changedQuantity", transactionLog.GetChangedQuantity());
        sqliteCommand.Parameters.AddWithValue("@newQuantityInStock", transactionLog.GetNewQuantityInStock());
        
        try {
            sqliteCommand.ExecuteNonQuery();
        }
        catch (Exception e) {
            Console.WriteLine($"Failed to insert transaction log: {e.Message}");
        }
        finally {
            sqliteConnection.Close();
        }
    }
    
    // insert a stock item into sqlite database method
    public static void InsertStockItem(StockItemModel stockItem) {
        
        const string insertStockItemQuery = "INSERT INTO stock_items (code, name, quantity_in_stock) VALUES (@code, @name, @quantityInStock)";
        
        using SqliteConnection sqliteConnection = GetSqliteConnection();
        sqliteConnection.Open();

        using SqliteCommand sqliteCommand = new SqliteCommand(insertStockItemQuery, sqliteConnection);
        sqliteCommand.Parameters.AddWithValue("@code", stockItem.GetCode());
        sqliteCommand.Parameters.AddWithValue("@name", stockItem.GetName());
        sqliteCommand.Parameters.AddWithValue("@quantityInStock", stockItem.GetQuantityInStock());

        try {
            sqliteCommand.ExecuteNonQuery();
        }
        catch (Exception e) {
            Console.WriteLine($"Failed to insert stock item: {e.Message}");
        }
        finally {
            sqliteConnection.Close();
        }
    }
    
    // delete a stock item from sqlite database method
    public static void DeleteStockItem(StockItemModel stockItem) {

        const string deleteStockItemQuery = "DELETE FROM stock_items WHERE code = @code";

        using SqliteConnection sqliteConnection = GetSqliteConnection();
        sqliteConnection.Open();

        using SqliteCommand sqliteCommand = new SqliteCommand(deleteStockItemQuery, sqliteConnection);
        sqliteCommand.Parameters.AddWithValue("@code", stockItem.GetCode());

        try {
            sqliteCommand.ExecuteNonQuery();
        } 
        catch (Exception e) {
            Console.WriteLine($"Failed to delete stock item: {e.Message}");
        } 
        finally {
            sqliteConnection.Close();
        }
    }
    
    // update a stock item's quantity in stock in the sqlite database method
    public static void UpdateStockItemQuantity(StockItemModel stockItem) {
        
        const string updateStockItemQuery = "UPDATE stock_items SET quantity_in_stock = @newQuantityInStock WHERE code = @code";

        using SqliteConnection sqliteConnection = GetSqliteConnection();
        sqliteConnection.Open();

        using SqliteCommand sqliteCommand = new SqliteCommand(updateStockItemQuery, sqliteConnection);
        sqliteCommand.Parameters.AddWithValue("@code", stockItem.GetCode());
        sqliteCommand.Parameters.AddWithValue("@newQuantityInStock", stockItem.GetQuantityInStock());

        try {
            sqliteCommand.ExecuteNonQuery();
        } 
        catch (Exception e) {
            Console.WriteLine($"Failed to update stock item quantity: {e.Message}");
        } 
        finally {
            sqliteConnection.Close();
        }
    }
    
    // get all the stock items in the sqlite database method
    public static List<StockItemModel>? GetAllStockItems() {
        const string selectStockItemsQuery = "SELECT * FROM stock_items";

        using SqliteConnection sqliteConnection = GetSqliteConnection();
        sqliteConnection.Open();

        using SqliteCommand sqliteCommand = new SqliteCommand(selectStockItemsQuery, sqliteConnection);
        using SqliteDataReader sqliteDataReader = sqliteCommand.ExecuteReader();
        
        List<StockItemModel> stockItems = new List<StockItemModel>();

        try {
            while (sqliteDataReader.Read()) {
                string code = sqliteDataReader.GetString(0);
                string name = sqliteDataReader.GetString(1);
                int quantityInStock = sqliteDataReader.GetInt32(2);

                stockItems.Add(new StockItemModel(code, name, quantityInStock));
            }
        }
        catch (Exception e) {
            Console.WriteLine($"Failed to get all stock items: {e.Message}");
        }
        finally {
            sqliteConnection.Close();
        }

        return stockItems;
    }
    
    // get a stock item using code from the sqlite database method
    public static StockItemModel? GetStockItemByCode(string selectedCode) {
        const string selectStockItemQuery = "SELECT * FROM stock_items WHERE code = @selectedCode";

        using SqliteConnection sqliteConnection = GetSqliteConnection();
        sqliteConnection.Open();

        using SqliteCommand sqliteCommand = new SqliteCommand(selectStockItemQuery, sqliteConnection);
        sqliteCommand.Parameters.AddWithValue("@selectedCode", selectedCode);

        using SqliteDataReader sqliteDataReader = sqliteCommand.ExecuteReader();
        try {
            if (sqliteDataReader.Read()) {
                string code = sqliteDataReader.GetString(0);
                string name = sqliteDataReader.GetString(1);
                int quantityInStock = sqliteDataReader.GetInt32(2);

                return new StockItemModel(code, name, quantityInStock);
            }
        }
        catch (Exception e) {
            Console.WriteLine($"Failed to get stock item by code: {e.Message}");
        }
        finally {
            sqliteConnection.Close();
        }

        return null;
    }
}