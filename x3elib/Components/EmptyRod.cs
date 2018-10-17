namespace x3e.components
{
    using simulation;
    public abstract class EmptyRod : SimulatorObject
    {
        public float Volume { get; set; }
        public abstract float MaxVolume { get; }
        public abstract float Power { get; set; }
        public float Load { get; set; }
        public float ExtractedPower { get; set; }
        public float Temperature { get; set; } = 21.4f;

        //
        public float MinTemperature { get; set; }
        public float MaxTemperature { get; set; }
        //

        public override void Update()
        {
            if(Volume > 0) Temperature += 0.14f.Normalize(0.03f);
        }
    }
}