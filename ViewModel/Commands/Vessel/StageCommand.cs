using PropertyChanged;

namespace KermandCenter.ViewModel.Commands.Vessel
{
    [AddINotifyPropertyChangedInterface]
    public class StageCommand : VesselCommand
    {
        public override string Name => "Stage";
        
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter) => Controller.Stage();
    }
}
