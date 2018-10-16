namespace x3e.components
{
    public class ProtectionZone<TZone> where TZone : Zone
    {
        private readonly TZone _zone;

        public ProtectionZone(TZone zone) => _zone = zone;

        public void Protection<Rod>(Rod rod) where Rod : EmptyRod
        {
            if (rod.Volume > 400f)
            {
                _zone.Extract(true);
                return;
            }
            if (rod.Temperature <= -2200)
            {
                _zone.Extract(true);
                return;
            }
        }
    }
}