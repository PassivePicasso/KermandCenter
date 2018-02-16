using KermandCenter.ViewModel.Commands;
using KermandCenter.ViewModel.Commands.Vessel;
using KermandCenter.ViewModel.MVVM;
using PropertyChanged;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;

namespace KermandCenter.ViewModel.Systems
{
    [AddINotifyPropertyChangedInterface]
    public class Phase
    {
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public ObservableCollection<FlightCommand> PhaseCommands { get; set; }

        public String Name { get; set; }

        public ICommand AddSetThrottleCommand { get; set; }
        public ICommand AddAutoPilotCommand { get; set; }
        public ICommand AddStageCommand { get; set; }
        public ICommand AddToggleGroupCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        public ICommand MoveUpCommand { get; set; }
        public ICommand MoveDownCommand { get; set; }
        public ICommand ExecutePhaseCommand { get; set; }

        public Phase()
        {
            PhaseCommands = new ObservableCollection<FlightCommand>();

            AddSetThrottleCommand = new DelegateCommand(() => PhaseCommands.Add(new SetThrottleCommand()));
            AddAutoPilotCommand = new DelegateCommand(() => PhaseCommands.Add(new AutoPilotCommand()));
            AddStageCommand = new DelegateCommand(() => PhaseCommands.Add(new StageCommand()));
            AddToggleGroupCommand = new DelegateCommand(() => PhaseCommands.Add(new ToggleGroupCommand()));
            RemoveCommand = new DelegateCommand<FlightCommand>(o => PhaseCommands.Remove(o));
            MoveUpCommand = new DelegateCommand<FlightCommand>(fc =>
            {
                var old = PhaseCommands.IndexOf(fc);
                if (old - 1 >= 0)
                    PhaseCommands.Move(old, old - 1);
            });
            MoveDownCommand = new DelegateCommand<FlightCommand>(fc =>
            {
                var old = PhaseCommands.IndexOf(fc);
                if (old + 1 < PhaseCommands.Count)
                    PhaseCommands.Move(old, old + 1);
            });
            ExecutePhaseCommand = new DelegateCommand(() =>
            {
                foreach (var pc in PhaseCommands)
                    pc.Execute(null);
            });
        }
    }
}
