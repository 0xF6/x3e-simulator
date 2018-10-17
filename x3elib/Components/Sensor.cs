namespace x3e.components
{
    using simulation;
    public abstract class Sensor<Rod> : SimulatorObject where Rod : EmptyRod, new()
    {
        public abstract void Signal();


        public Rod ActiveRod =>
            ActiveZone.GetComponent<ReactionZone<Rod>>().GetComponent<Rod>();

        public ActiveZone<Rod> ActiveZone =>
            GetComponent<ProtectionZone<Rod>>().
            GetComponent<ActiveZone<Rod>>();
    }
}