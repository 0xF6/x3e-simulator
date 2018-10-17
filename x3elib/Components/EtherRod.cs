namespace x3e.components
{
    public class EtherRod : EmptyRod
    {
        public EtherRod() => this.Volume = 25;
        public override float MaxVolume { get; } = 240f;
        public override float Power { get; set; } = 14.6f;

    }
}