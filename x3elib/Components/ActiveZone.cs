namespace x3e.components
{
    using System;
    using System.Drawing;
    using etc;
    using RC.Framework.Screens;
    
    public class ActiveZone<TRod> : Zone where TRod : EmptyRod, new()
    {
        public ActiveZoneStatus Status { get; private set; } = ActiveZoneStatus.Unknown;

        public ActiveZone()
        {
            AddComponent<ProtectionZone<TRod>>();
            AddComponent<AnalogZone<TRod>>();
            AddComponent<RefrigerationZone<TRod>>();
            AddComponent<ReactionZone<TRod>>();
        }
        public override void Start()
        {
            if (Status != ActiveZoneStatus.Unknown)
                throw new Exception("Invalid status zone.");
            Screen.WriteLine($"[{"WarmUp".To(Color.Red)}] Zone ID: {UID.To(Color.Gold)}");
            Status = ActiveZoneStatus.Warm;
        }
        public void SetStatus(ActiveZoneStatus stat) => Status = stat;
        public void Extract(bool emergency = false, bool auto = true, ActiveZoneStatus? status = null)
        {
            if (emergency)
            {
                Screen.WriteLine($"[{"FAULT".To(Color.Red)}] EMERGENCY EXTRACTED ROD [ZoneID: {UID.To(Color.Gold)}]");
                ActiveRod.Volume -= ActiveRod.Volume.Percent(46.3f);
                Status = ActiveZoneStatus.EmergencyStop;
                if(status != null) Status |= status.Value;
                Console.WriteLine(Status);
                return;
            }

            if (auto)
            {
               Screen.WriteLine($"[{"E".To(Color.Yellow)}] Extracted rod in [ZoneID: {UID.To(Color.Gold)}] due to {status}, last active volume: {ActiveRod.Volume}");
               Screen.WriteLine($"[{"E".To(Color.Yellow)}] Total generated energy in current rod: {ActiveRod.ExtractedPower} e/s in [ZoneID: {UID.To(Color.Gold)}]");
               Screen.WriteLine($"[{"E".To(Color.Yellow)}] Max {ActiveRod.MaxTemperature}°C Min {ActiveRod.MinTemperature}°C");
            }
            Status = ActiveZoneStatus.Extracted;
        }
        public override void NextReaction()
        {
        }

        public TRod ActiveRod => GetComponent<ReactionZone<TRod>>().GetComponent<TRod>();
    }
    [Flags]
    public enum ActiveZoneStatus
    {
        Unknown = 0,
        Extracted = 1 << 1,
        Warm = 1 << 2,
        Work = 1 << 3,
        Overheat = 1 << 4,
        EmergencyStop = 1 << 5,
        CriticalMassOverloop = 1 << 6,
        ActiveSubstanceDanger = 1 << 7,
        LowerMass = 1 << 8
    }
}