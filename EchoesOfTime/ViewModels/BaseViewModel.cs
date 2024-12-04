using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EchoesOfTime.ViewModels;

public abstract class BaseViewModel : INotifyPropertyChanged
{
    // Event triggered when a property changes
    public event PropertyChangedEventHandler PropertyChanged;

    // Method to raise the PropertyChanged event
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    // Optional: Utility method to set a property and raise PropertyChanged if the value changes
    protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        if (!Equals(field, value))
        {
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        return false;
    }
}
