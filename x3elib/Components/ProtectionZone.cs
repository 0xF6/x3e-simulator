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
            if(GetComponent<ActiveZone<Rod>>().Status != ActiveZoneStatus.Work)
                return;
            GetComponent<TemperatureSensor<Rod>>().Signal();
            GetComponent<VolumeSensor<Rod>>().Signal();
        }

        public void Extract(bool emergency = false, ActiveZoneStatus? status = null) => GetComponent<ActiveZone<Rod>>().Extract(emergency, true, status);
    }
}