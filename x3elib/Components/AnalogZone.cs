namespace x3e.components
{
    using simulation;
    public class AnalogZone<Rod> : SimulatorObject where Rod : EmptyRod, new()
    {
        public bool IsHibernate = false;
        public Rod ActiveRod =>
            GetComponent<ActiveZone<Rod>>().
            GetComponent<ReactionZone<Rod>>().
            GetComponent<Rod>();

        public ActiveZone<Rod> Zone =>
            GetComponent<ActiveZone<Rod>>();

        public override void Update()
        {
            if(IsHibernate) return;
            if(Zone.Status == ActiveZoneStatus.Extracted)
                Zone.SetStatus(ActiveZoneStatus.Warm);
        }

        public void Shutdown()
        {
            if(IsHibernate)
                return;
            IsHibernate = true;
        }

        public new void Start()
        {
            base.Start();
            if(!IsHibernate)
                return;
            IsHibernate = false;
        }
    }
}