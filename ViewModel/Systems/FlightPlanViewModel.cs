using KermandCenter.ViewModel.MVVM;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace KermandCenter.ViewModel.Systems
{
    [AddINotifyPropertyChangedInterface]
    public class FlightPlanViewModel
    {
        public ObservableCollection<Phase> FlightPlan { get; set; }

        public Phase CurrentPhase { get; set; }

        public ICommand AddPhaseCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        public ICommand MoveUpCommand { get; set; }
        public ICommand MoveDownCommand { get; set; }

        public FlightPlanViewModel()
        {
            FlightPlan = new ObservableCollection<Phase>();

            AddPhaseCommand = new DelegateCommand(() => FlightPlan.Add(new Phase()));
            RemoveCommand = new DelegateCommand<Phase>(o => FlightPlan.Remove(o));
            MoveUpCommand = new DelegateCommand<Phase>(phase =>
            {
                var old = FlightPlan.IndexOf(phase);
                if (old - 1 >= 0)
                    FlightPlan.Move(old, old - 1);
            });
            MoveDownCommand = new DelegateCommand<Phase>(phase =>
            {
                var old = FlightPlan.IndexOf(phase);
                if (old + 1 < FlightPlan.Count)
                    FlightPlan.Move(old, old + 1);
            });
        }
    }
}
