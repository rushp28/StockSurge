using System;
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
    
    // insert a stock item into sqlite database method
    public static void InsertStockItem(string code, string name, int quantityInStock) {
        
        const string insertStockItemQuery = "INSERT INTO stock_items (code, name, quantity_in_stock) VALUES (@code, @name, @quantityInStock)";
        
        using SqliteConnection sqliteConnection = GetSqliteConnection();
        sqliteConnection.Open();

        using SqliteCommand sqliteCommand = new SqliteCommand(insertStockItemQuery, sqliteConnection);
        sqliteCommand.Parameters.AddWithValue("@code", code);
        sqliteCommand.Parameters.AddWithValue("@name", name);
        sqliteCommand.Parameters.AddWithValue("@quantityInStock", quantityInStock);
                
        sqliteCommand.ExecuteNonQuery();
    }
    
    
    
}