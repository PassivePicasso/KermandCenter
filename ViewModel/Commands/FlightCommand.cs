using KermandCenter.ViewModel.MVVM;
using PropertyChanged;
using System;
using System.Windows.Input;

namespace KermandCenter.ViewModel.Commands
{
    [AddINotifyPropertyChangedInterface]
    public abstract class FlightCommand : ICommand
    {
        public abstract string Name { get; }

        public event EventHandler CanExecuteChanged;

        public abstract bool CanExecute(object parameter);

        public abstract void Execute(object parameter);
    }
}
