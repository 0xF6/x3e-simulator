namespace x3e.components
{
    using simulation;
    public class ProtectionZone<Rod> : SimulatorObject where Rod : EmptyRod, new()
    {
        public ProtectionZone() : base()
        {
            AddComponent<TemperatureSensor<Rod>>();
            AddComponent<VolumeSensor<Rod>>();
        }
        public override void Update()
        {

            var status = GetComponent<ActiveZone<Rod>>().Status;
            
            if(status.HasFlag(ActiveZoneStatus.EmergencyStop)) return;
            if(status.HasFlag(ActiveZoneStatus.Extracted)) return;
            GetComponent<TemperatureSensor<Rod>>().Signal();
            GetComponent<VolumeSensor<Rod>>().Signal();
        }

        public void Extract(bool emergency = false, ActiveZoneStatus? status = null) => GetComponent<ActiveZone<Rod>>().Extract(emergency, true, status);
    }
}