using KermandCenter.Model.Controllers;
using KRPC.Client;
using KRPC.Client.Services.KRPC;
using KRPC.Client.Services.SpaceCenter;
using KRPC.Schema.KRPC;
using PropertyChanged;

namespace KermandCenter.Model
{
    [AddINotifyPropertyChangedInterface]
    public class KConnector
    {
        private static KConnector s_instance;
        public static KConnector Instance
        {
            get
            {
                if (s_instance == null) s_instance = new KConnector();
                return s_instance;
            }
        }


        Connection _connection;

        KRPC.Client.Services.KRPC.Service _kRPC;
        private AutoPilotController _autoPilotController;
        private VesselController _vesselController;

        public string Version { get; private set; }

        public Status Status { get; private set; }

        public Services AllServices { get; private set; }

        public bool Connected { get; private set; }

        public AutoPilotController AutoPilotController { get; private set; }

        public VesselController VesselController { get; private set; }

        public ReferenceFrame TargetBodyReferenceFrame { get => _spaceCenter.TargetBody.ReferenceFrame; }

        KRPC.Client.Services.SpaceCenter.Service _spaceCenter { get; set; }

        public bool Connect()
        {
            try
            {
                _connection = new Connection(name: "Kermand Center");
                Connected = true;
                _kRPC = _connection.KRPC();
                AllServices = _kRPC.GetServices();
                Status = _kRPC.GetStatus();
                Version = Status.Version;
                _spaceCenter = _connection.SpaceCenter();
                var vessel = _spaceCenter.ActiveVessel;
                var autoPilot = vessel.AutoPilot;
                AutoPilotController = new AutoPilotController(autoPilot, vessel);
                VesselController = new VesselController(vessel);
            }
            catch (System.Exception ex)
            {
                Connected = false;
                return Connected;
            }
            return Connected;
        }

        public bool Disconnect()
        {
            Connected = false;
            _connection?.Dispose();
            Status = null;
            Version = null;
            AllServices = null;
            return Connected;
        }
    }
}
