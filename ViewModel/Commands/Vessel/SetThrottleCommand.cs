using PropertyChanged;

namespace KermandCenter.ViewModel.Commands.Vessel
{
    [AddINotifyPropertyChangedInterface]
    public class SetThrottleCommand : VesselCommand
    {
        public float Throttle { get; set; }

        public override string Name => "SetThrottle";
        
        public override bool CanExecute(object parameter) => Controller != null && Throttle >= 0 && Throttle <= 1;

        public override void Execute(object parameter) => Controller.Throttle = Throttle;
    }
}
