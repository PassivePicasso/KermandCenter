using KermandCenter.Model;
using KermandCenter.ViewModel.MVVM;
using KermandCenter.ViewModel.Systems;
using PropertyChanged;
using System.Windows.Input;

namespace KermandCenter.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class KermandCenterViewModel : System.IDisposable
    {
        public ICommand ConnectCommand { get; private set; }
        public ICommand DisconnectCommand { get; private set; }
        public ICommand ControlVesselCommand { get; private set; }
        public ICommand FlightPlanCommand { get; private set; }

        public object FocusedSystem { get; private set; }

        public bool Disconnected { get => !Connected; }
        public bool Connected { get; private set; } = false;

        public KermandCenterViewModel()
        {
            ConnectCommand = new DelegateCommand(Connect, HasKConnector);
            DisconnectCommand = new DelegateCommand(Disconnect, HasKConnector);
            ControlVesselCommand = new DelegateCommand(ControlVessel, HasKConnector);
            FlightPlanCommand = new DelegateCommand(FlightPlan, HasKConnector);
        }

        public bool HasKConnector() => KConnector.Instance != null;

        public void Connect()
        {
            Connected = KConnector.Instance.Connect();
        }

        public void Disconnect()
        {
            Connected = KConnector.Instance.Disconnect();
            FocusedSystem = null;
        }

        public void ControlVessel() => FocusedSystem = new VesselControlViewModel();
        public void FlightPlan() => FocusedSystem = new FlightPlanViewModel();

        public void Dispose()
        {
            Disconnect();
        }
    }
}
