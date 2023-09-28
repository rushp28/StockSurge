namespace StockSurge.Models; 

public class UserModel {
    // attributes
    private readonly string _username;
    private readonly string _password;

    // constructor
    public UserModel(string username, string password) {
        _username = username;
        _password = password;
    }
    
    // getters
    public string GetUsername() {
        return _username;
    }
    
    public string GetPassword() {
        return _password;
    }
}