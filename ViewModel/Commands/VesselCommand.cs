using System;
using KermandCenter.Model;
using KermandCenter.Model.Controllers;

namespace KermandCenter.ViewModel.Commands
{
    public abstract class VesselCommand : FlightCommand
    {
        public VesselController Controller { get => KConnector.Instance.VesselController; }

    }
}
