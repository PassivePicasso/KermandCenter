using PropertyChanged;

namespace KermandCenter.ViewModel.Commands.Vessel
{
    [AddINotifyPropertyChangedInterface]
    public class ToggleGroupCommand : VesselCommand
    {
        public int Group { get; set; }

        public override string Name => "Toggle Group";
        
        public override bool CanExecute(object parameter) => Group >= 0 && Group <= 9;

        public override void Execute(object parameter) => Controller.TriggerActionGroup(Group);
    }
}
