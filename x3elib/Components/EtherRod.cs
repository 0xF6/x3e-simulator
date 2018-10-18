namespace x3e.components
{
    using System;

    public class EtherRod : EmptyRod
    {
        public override Guid UID { get; } = Guid.Parse("02194DED-A70A-46C9-B981-0D8B2DE01DE7");
        public EtherRod() => this.Volume = 25;
        public override float MaxVolume { get; } = 280f;
        public override float Power { get; set; } = 14.6f;

    }
}