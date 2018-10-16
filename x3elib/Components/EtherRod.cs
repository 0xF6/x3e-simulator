namespace EtherReactorSimulator.Components
{
    public class EtherRod : EmptyRod
    {
        public override float MaxVolume { get; } = 240f;
        public override float Power { get; set; } = 14.6f;

    }
}