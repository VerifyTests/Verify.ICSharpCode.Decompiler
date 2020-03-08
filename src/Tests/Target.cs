using System.ComponentModel;
using System.Runtime.CompilerServices;

public class Target :
    INotifyPropertyChanged
{
    string? property;

    public string? Property
    {
        get => property;
        set
        {
            property = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}