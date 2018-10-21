namespace x3e.components
{
    using System;
    using System.Drawing;
    using etc;
    using RC.Framework.Screens;
    using simulation;
    public class ReactionZone<Rod> : SimulatorObject where Rod : EmptyRod, new()
    {
        private ActiveZone<Rod> Zone => GetComponent<ActiveZone<Rod>>() ?? throw new Exception("Error: Parent ReactionZone is not ActiveZone instance.");

        protected Rod ActiveRod => GetComponent<Rod>();

        public ReactionZone()
        {
            AddComponent<Rod>();
        }


        public override void Update()
        {
            if(Zone.Status == ActiveZoneStatus.Extracted) return;
            if(!Zone.Status.HasFlag(ActiveZoneStatus.Work)) return;
            ActiveRod.Load = CalculateLoad(ActiveRod.Volume);
            var deg = DegradationVolume(ActiveRod.Volume, ActiveRod.Power, out var generated);
            Screen.WriteLine($"[{"G".To(Color.Purple)}] Generated {$"{generated}e/s".To(Color.GreenYellow)}, degradation volume: {deg} in [ZoneID: {Zone.UID.To(Color.Gold)}] {ActiveRod.Temperature}°C");
            ActiveRod.Volume -= deg.Normalize(deg.Percent(75)) * 3;
            ActiveRod.Temperature += 
                ActiveRod.Temperature.
                    Less(0).Then(125f.At(143f).Fixed(0)).
                    Else(10f.At(34f).Fixed(0)).
                    Value<float>();
            ActiveRod.ExtractedPower += generated;
        }



        /// <summary>
        /// |x arctan(x) cos(x)|
        /// </summary>
        private float CalculateLoad(double power) =>
           (float)Math.Abs(power * Math.Atan(power) *
            Math.Cos(power));
        /// <summary>
        /// (x/power)^2 arctan(x) - energy
        /// log(x) - degradation
        /// </summary>
        public float DegradationVolume(float volume, float power, out float generatedEnergy)
        {
            generatedEnergy = (float)(Math.Pow(volume / power, 2) * Math.Atan(volume));
            return (float)(Math.Log(volume));
        }
    }
}