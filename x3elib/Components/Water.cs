namespace x3e.components
{
    using Ivy.Measures.Quantities;
    using simulation;
    public class Water : SimulatorObject
    {
        public Volume MaxVolume { get; } = new Volume(200f, Volume.Liter);
        public Volume CurrentVolume { get; } = new Volume(0f, Volume.Liter);
        public Temperature Temp { get; } = new Temperature(0f, Temperature.Celsius);
        public override void Start()
        {
        }
    }
}