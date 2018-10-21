namespace x3e.components
{
    using System;
    using System.Drawing;
    using etc;
    using RC.Framework.Screens;
    using simulation;
    public class RefrigerationZone<Rod> : SimulatorObject where Rod : EmptyRod, new()
    {
        public Rod ActiveRod => 
            GetComponent<ActiveZone<Rod>>().
            GetComponent<ReactionZone<Rod>>().
            GetComponent<Rod>();

        public ActiveZone<Rod> Zone =>
            GetComponent<ActiveZone<Rod>>();

        public override void Update()
        {
            if(Zone.Status != ActiveZoneStatus.Warm) return;
            if(Zone.Status == ActiveZoneStatus.Extracted) return;
            if (WarmStart == null) WarmStart = DateTime.UtcNow;
            
            if (ActiveRod.Volume >= ActiveRod.MaxVolume.Percent(95))
            {
                Zone.SetStatus(ActiveZoneStatus.Work);
                WarmStart = null;
                return;
            }
            Screen.WriteLine($"[{"R".To(Color.Red)}] Cooling rod in [ZoneID: {Zone.UID.To(Color.Gold)}] V:{ActiveRod.Volume} {ActiveRod.Temperature}°C");
            ActiveRod.Volume = CalculateVolume(WarmStart.Value, ActiveRod.Power);
            ActiveRod.Temperature -= ActiveRod.Temperature.
                Percent(15f).
                If(0).Then(12.5f).Value<float>().
                Less(200f).Then(25.4f).Value<float>();
        }


        /// <summary>
        /// x^2 arctan(x^3) + log(x^power)
        /// </summary>
        public float CalculateVolume(DateTime time, float Power)
        {
            var x = (DateTime.UtcNow - time).TotalSeconds * 10;
            return (float)Math.Abs(Math.Pow(x, 2) * Math.Atan(Math.Pow(x, 3)) + Math.Log(Math.Pow(x, Power)));
        }

        private DateTime? WarmStart = null;
    }
}