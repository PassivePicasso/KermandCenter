using KRPC.Client.Services.SpaceCenter;
using PropertyChanged;
using System;
using System.Collections.Generic;

namespace KermandCenter.Model.Controllers
{
    [AddINotifyPropertyChangedInterface]
    public class VesselController
    {
        public AutoPilot AutoPilot { get; private set; }
        public Vessel VesselHandle { get; private set; }
        public Control VesselControl { get; private set; }
        public Dictionary<int, IList<Vessel>> StageVessels;
        public Dictionary<uint, bool> ActionGroupStates;

        public int CurrentStage { get; private set; }

        public float Throttle { get => VesselControl.Throttle; set => VesselControl.Throttle = Math.Min(1, Math.Max(0, value)); }
        public float Pitch { get => VesselControl.Pitch; set => VesselControl.Pitch = Math.Min(1, Math.Max(-1, value)); }
        public float Yaw { get => VesselControl.Yaw; set => VesselControl.Yaw = Math.Min(1, Math.Max(-1, value)); }
        public float Roll { get => VesselControl.Roll; set => VesselControl.Roll = Math.Min(1, Math.Max(-1, value)); }


        ReferenceFrame _localReferenceFrame;
        public VesselController(Vessel activeVessel)
        {
            VesselHandle = activeVessel;
            VesselControl = VesselHandle.Control;
            AutoPilot = activeVessel.AutoPilot;

            _localReferenceFrame = VesselHandle.Orbit.Body.ReferenceFrame;

            StageVessels = new Dictionary<int, IList<Vessel>>();
            ActionGroupStates = new Dictionary<uint, bool>();

            for (uint i = 0; i < 10; i++)
                ActionGroupStates.Add(i, VesselControl.GetActionGroup(i));
        }

        public void Stage()
        {
            if (StageVessels.ContainsKey(VesselControl.CurrentStage)) StageVessels.Clear();

            StageVessels.Add(VesselControl.CurrentStage, VesselControl.ActivateNextStage());
        }

        public void TriggerActionGroup(int groupNumber)
        {
            VesselControl.ToggleActionGroup((uint)groupNumber);
            ActionGroupStates[(uint)groupNumber] = !ActionGroupStates[(uint)groupNumber];
        }

        public void SetStageVesselThrust(int stage, float thrust)
        {
            if (StageVessels.ContainsKey(stage))
                foreach (var vessel in StageVessels[stage])
                    vessel.Control.Throttle = Math.Min(1, Math.Max(0, thrust));
        }


    }
}
