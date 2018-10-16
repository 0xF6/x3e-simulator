namespace x3e.components
{
    public abstract class EmptyRod
    {
        public float Volume { get; set; }
        public abstract float MaxVolume { get; }
        public abstract float Power { get; set; }
        public float Load { get; set; }
        public float ExtractedPower { get; set; }
        public float Temperature { get; set; } = 21.4f;


        public float MinTemperature { get; set; }
        public float MaxTemperature { get; set; }
    }
}