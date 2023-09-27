using System.ComponentModel;

namespace StockSurge.ViewModels; 

public class BaseViewModel : INotifyPropertyChanged {
    public event PropertyChangedEventHandler PropertyChanged;
    
    protected void OnPropertyChanged(string propertyName) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}