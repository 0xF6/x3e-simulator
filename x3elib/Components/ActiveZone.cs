namespace x3e.components
{
    using System;
    using System.Drawing;
    using System.Threading;
    using etc;
    using RC.Framework.Screens;
    
    public class ActiveZone<TRod> : Zone where TRod : EmptyRod, new()
    {
        public ActiveZoneStatus Status { get; private set; } = ActiveZoneStatus.Unknown;
        public DateTime WarmStartTime { get; set; }

        public ActiveZone() => AddComponent<ReactionZone<TRod>>();
        public override void Start()
        {
            WarmUp();
        }

        public void WarmUp()
        {
            if(Status != ActiveZoneStatus.Unknown)
                throw new Exception("Invalid status zone.");
            Screen.WriteLine($"[{"WarmUp".To(Color.Red)}] Zone ID: {UID.To(Color.Gold)}");
            ReactivatedRod();
        }

        public void ReactivatedRod()
        {
            WarmStartTime = DateTime.UtcNow;
            Status = ActiveZoneStatus.Work;

            

            //while (true)
            {
                //Thread.Sleep(20);
                //Screen.WriteLine($"[{"R".To(Color.Red)}] Cooling rod in [ZoneID: {UID.To(Color.Gold)}] V:{Rod.Volume} {Rod.Temperature}°C");
                //if (Rod.Volume >= Rod.MaxVolume.Percent(95))
               //     break;
               // Rod.Volume = CalculateVolume(WarmStartTime, Rod.Power);
             //   Rod.Temperature -= Rod.Temperature.
              //      Percent(1f).
            //        If(0).Then(12.5f).Value<float>().
            //        Less(200f).Then(25.4f).Value<float>();
            }

            //if (Rod.MinTemperature > Rod.Temperature)
            //    Rod.MinTemperature = Rod.Temperature;
            //Status = ActiveZoneStatus.Work;
        }
        public override void Extract(bool emergency = false, bool auto = true)
        {
            //if (Rod.MaxTemperature < Rod.Temperature) Rod.MaxTemperature = Rod.Temperature;
            if (emergency)
            {
                Screen.WriteLine($"[{"FAULT".To(Color.Red)}] EMERGENCY EXTRACTED ROD [ZoneID: {UID.To(Color.Gold)}]");
              //  Rod.Volume -= Rod.Volume.Percent(46.3f);
                Status = ActiveZoneStatus.Overheat | ActiveZoneStatus.EmergencyStop;
                Console.WriteLine(Status);
                return;
            }

            if (auto)
            {
               // Screen.WriteLine($"[{"E".To(Color.Yellow)}] Extracted rod in [ZoneID: {UID.To(Color.Gold)}], last active volume: {Rod.Volume}");
               // Screen.WriteLine($"[{"E".To(Color.Yellow)}] Total generated energy in current rod: {Rod.ExtractedPower} e/s in [ZoneID: {UID.To(Color.Gold)}]");
               // Screen.WriteLine($"[{"E".To(Color.Yellow)}] Max {Rod.MaxTemperature}°C Min {Rod.MinTemperature}°C");
            }
            Status = ActiveZoneStatus.Extracted;
            if(auto)
                ReactivatedRod();
        }
        public override void NextReaction()
        {
            //if (Status.HasFlag(ActiveZoneStatus.EmergencyStop))
            //    return;
            //if (!Status.HasFlag(ActiveZoneStatus.Work))
            //    return;
            //Protector.Protection(Rod);
            GetComponent<ReactionZone<TRod>>().Update();
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
            return (float) Math.Abs(Math.Pow(x, 2) * Math.Atan(Math.Pow(x, 3)) + Math.Log(Math.Pow(x, Power)));
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
    [Flags]
    public enum ActiveZoneStatus
    {
        Unknown = 0,
        Extracted = 1 << 1,
        Warm = 1 << 2,
        Work = 1 << 3,
        Overheat = 1 << 4,
        EmergencyStop = 1 << 5
    }
}