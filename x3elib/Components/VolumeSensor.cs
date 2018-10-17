namespace x3e.components
{
    public class VolumeSensor<Rod> : Sensor<Rod> where Rod : EmptyRod, new()
    {
        public override void Signal()
        {
            var protectionZone = GetComponent<ProtectionZone<Rod>>();
            ActiveRod.ReadTemperature();

            if (ActiveRod.Volume <= 20)
                protectionZone.Extract(false, ActiveZoneStatus.LowerMass);
            if (ActiveRod.Volume >= 440_000)
                protectionZone.Extract(true, ActiveZoneStatus.CriticalMassOverloop);
        }
    }
}