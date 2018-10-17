namespace x3e.components
{
    public class TemperatureSensor<Rod> : Sensor<Rod> where Rod : EmptyRod, new()
    {
        public override void Signal()
        {
            var protectionZone = GetComponent<ProtectionZone<Rod>>();
            ActiveRod.ReadTemperature();

            if(ActiveRod.Temperature >= 2100)
                protectionZone.Extract(true, ActiveZoneStatus.Overheat);
            if (ActiveRod.Temperature <= -4700)
                protectionZone.Extract(true, ActiveZoneStatus.ActiveSubstanceDanger);
        }
    }
}