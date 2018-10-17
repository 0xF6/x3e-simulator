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
            if(Zone.Status != ActiveZoneStatus.Work) return;
            ActiveRod.Load = CalculateLoad(ActiveRod.Volume.Normalize(2f));
            var deg = DegradationVolume(ActiveRod.Volume, ActiveRod.Power, out var generated);
            Screen.WriteLine($"[{"G".To(Color.Purple)}] Generated {$"{generated}e/s".To(Color.GreenYellow)}, degradation volume: {deg} in [ZoneID: {Zone.UID.To(Color.Gold)}] {ActiveRod.Temperature}°C");
            ActiveRod.Volume -= deg.Normalize(deg.Percent(45));
            ActiveRod.Temperature += 
                ActiveRod.Temperature.
                    Less(0).Then(125f.At(143f).Fixed(0)).
                    Else(10f.At(34f).Fixed(0)).
                    Value<float>();
            ActiveRod.ExtractedPower += generated;
            if (ActiveRod.Volume < 20f) Zone.Extract();
        }



        /// <summary>
        /// x^2 arctan(x^3) cos(x^4^2)
        /// </summary>
        private float CalculateLoad(double power) =>
           (float)(Math.Pow(power, 2) * Math.Atan(Math.Pow(power, 3)) *
            Math.Cos(Math.Pow(Math.Pow(power, 4), 2)));
        /// <summary>
        /// x^2 arctan(x^3) + log(x^power)
        /// </summary>
        public float CalculateVolume(DateTime time, float Power)
        {
            var x = (DateTime.UtcNow - time).TotalSeconds * 10;
            return (float)Math.Abs(Math.Pow(x, 2) * Math.Atan(Math.Pow(x, 3)) + Math.Log(Math.Pow(x, Power)));
        }
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