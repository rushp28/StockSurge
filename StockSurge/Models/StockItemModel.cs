namespace StockSurge.Models; 

public class StockItemModel {
    
    // attributes
    private readonly string _code;
    private readonly string _name;
    private int _quantityInStock;
    
    // constructor method
    public StockItemModel(string code, string name, int quantityInStock) {
        _code = code;
        _name = name;
        _quantityInStock = quantityInStock;
    }
    
    // get methods
    public string GetCode() {
        return _code;
    }

    public string GetName() {
        return _name;
    }

    public int GetQuantityInStock() {
        return _quantityInStock;
    }
    
    // set methods
    public void SetQuantityInStock(int newQuantityInStock) {
        _quantityInStock = newQuantityInStock;
    }
}