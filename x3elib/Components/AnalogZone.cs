namespace x3e.components
{
    using simulation;
    public class AnalogZone<Rod> : SimulatorObject where Rod : EmptyRod, new()
    {
        public Rod ActiveRod =>
            GetComponent<ActiveZone<Rod>>().
            GetComponent<ReactionZone<Rod>>().
            GetComponent<Rod>();

        public ActiveZone<Rod> Zone =>
            GetComponent<ActiveZone<Rod>>();

        public override void Update()
        {
            if(Zone.Status == ActiveZoneStatus.Extracted)
                Zone.SetStatus(ActiveZoneStatus.Warm);
        }
    }
}