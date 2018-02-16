using KermandCenter.Model;
using KermandCenter.Model.Controllers;
using KermandCenter.ViewModel.MVVM;
using KRPC.Client.Services.SpaceCenter;
using PropertyChanged;
using System.Threading;
using System.Threading.Tasks;

namespace KermandCenter.ViewModel.Commands
{
    [AddINotifyPropertyChangedInterface]
    public class AutoPilotCommand : FlightCommand
    {
        public AutoPilotController Controller => KConnector.Instance.AutoPilotController;

        public float TargetPitch { get; set; }
        public float TargetHeading { get; set; }
        public float TargetRoll { get; set; }
        public bool PitchAndHeading { get; set; }
        public bool Roll { get; set; }
        public bool SAS { get; set; }
        public bool Engage { get; set; }
        public SASMode SASMode { get; set; }

        public override string Name => "Auto Pilot";

        public override bool CanExecute(object parameter) => !Controller.Engaged;

        public override void Execute(object parameter)
        {
            if (SAS)
            {
                Controller.SASMode = SASMode;

                Controller.SAS = SAS;
            }
            else if (Engage)
            {
                Task.Run(() =>
                {
                    lock (Controller)
                    {
                        Controller.AutoPilotReferenceFrame = Controller.VesselSurfaceReferenceFrame;

                        Controller.Engage();

                        if (PitchAndHeading)
                            Controller.TargetPitchAndHeading(TargetPitch, TargetHeading);

                        if (Roll)
                            Controller.TargetRoll = TargetRoll;
                    }
                    Controller.Wait();

                });
            }
            else Controller.Disengage();
        }
    }
}
