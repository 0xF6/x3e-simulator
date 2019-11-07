namespace x3e.components
{
    using System;
    using System.Drawing;
    using etc;
    using simulation;
    public class ReactionZone<Rod> : SimulatorObject where Rod : EmptyRod, new()
    {
        private ActiveZone<Rod> Zone => 
            GetComponent<ActiveZone<Rod>>() ?? 
            throw new Exception("Error: Parent ReactionZone is not ActiveZone instance.");

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
            Console.WriteLine($"[{"G".To(Color.Purple)}] Generated {$"{generated}e/s".To(Color.GreenYellow)}, degradation volume: {deg} in [ZoneID: {Zone.UID.To(Color.Gold)}] {ActiveRod.Temperature}°C");
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
        private float CalculateLoad(float power) =>
           MathF.Abs(power * MathF.Atan(power) *
            MathF.Cos(power));
        /// <summary>
        /// (x/power)^2 arctan(x) - energy
        /// log(x) - degradation
        /// </summary>
        public float DegradationVolume(float volume, float power, out float generatedEnergy)
        {
            generatedEnergy = (MathF.Pow(volume / power, 2) * MathF.Atan(volume));
            return MathF.Log(volume);
        }
    }
}