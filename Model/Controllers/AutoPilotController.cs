using KRPC.Client.Services.SpaceCenter;
using PropertyChanged;
using System;
using System.Threading.Tasks;

namespace KermandCenter.Model.Controllers
{
    [AddINotifyPropertyChangedInterface]
    public class AutoPilotController
    {
        private AutoPilot autoPilot;
        private Vessel vessel;

        public bool Engaged { get; private set; }

        public float TargetHeading { get => autoPilot.TargetHeading; set => autoPilot.TargetHeading = value; }
        public float TargetPitch { get => autoPilot.TargetPitch; set => autoPilot.TargetPitch = value; }
        public float TargetRoll { get => autoPilot.TargetRoll; set => autoPilot.TargetRoll = value; }
        public ReferenceFrame AutoPilotReferenceFrame { get => autoPilot.ReferenceFrame; set => autoPilot.ReferenceFrame = value; }
        public ReferenceFrame VesselSurfaceReferenceFrame { get => autoPilot.ReferenceFrame; set => autoPilot.ReferenceFrame = value; }
        public bool SAS { get => autoPilot.SAS; set => autoPilot.SAS = value; }
        public SASMode SASMode { get => autoPilot.SASMode; set => autoPilot.SASMode = value; }

        public AutoPilotController(AutoPilot autoPilot, Vessel vessel)
        {
            this.autoPilot = autoPilot;
            this.vessel = vessel;
            autoPilot.AutoTune = true;
            
        }

        public void TargetPitchAndHeading(float pitch, float heading)
        {
            autoPilot.TargetPitchAndHeading(pitch, heading);
        }

        public void Engage()
        {
            autoPilot.Engage();
            Engaged = true;
        }

        public void Disengage()
        {
            autoPilot.Disengage();
            Engaged = false;
        }

        public void Wait()
        {
            autoPilot.Wait();
        }
    }
}
