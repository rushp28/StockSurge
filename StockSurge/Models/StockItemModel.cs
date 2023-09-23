namespace StockSurge.Models; 

public class StockItemModel {
    private readonly string _code;
    private readonly string _name;
    private int _quantityInStock;

    public StockItemModel(int quantityInStock, string code, string name) {
        _code = code;
        _name = name;
        _quantityInStock = quantityInStock;
    }
    
    public string GetCode() {
        return _code;
    }

    public string GetName() {
        return _name;
    }

    public int GetQuantityInStock() {
        return _quantityInStock;
    }

    public void SetQuantityInStock(int newQuantityInStock) {
        _quantityInStock = newQuantityInStock;
    }
}