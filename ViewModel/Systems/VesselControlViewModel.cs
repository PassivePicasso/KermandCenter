using KermandCenter.Model;
using KermandCenter.Model.Controllers;
using KermandCenter.ViewModel.Commands;
using KermandCenter.ViewModel.Commands.Vessel;
using PropertyChanged;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace KermandCenter.ViewModel.Systems
{
    [AddINotifyPropertyChangedInterface]
    public class VesselControlViewModel
    {
        public VesselController Controller { get => KConnector.Instance.VesselController; }
        public StageCommand StageCommand { get; private set; }
        public ObservableCollection<ToggleGroupCommand> ToggleActionGroupCommands { get; private set; }

        public ObservableCollection<KeyValuePair<string, ICommand>> FlightPlan { get; set; }

        public float Throttle { set => Controller.Throttle = value; get => Controller.Throttle; }

        public float Pitch { set => Controller.Pitch = value; get => Controller.Pitch; }
        public float Yaw { set => Controller.Yaw = value; get => Controller.Yaw; }
        public float Roll { set => Controller.Roll = value; get => Controller.Roll; }

        public VesselControlViewModel()
        {
            StageCommand = new StageCommand { };
            ToggleActionGroupCommands = new ObservableCollection<ToggleGroupCommand>();

            Enumerable.Range(0, 9).ToList().ForEach(i => ToggleActionGroupCommands.Add(new ToggleGroupCommand { Group = i }));
        }


    }
}
