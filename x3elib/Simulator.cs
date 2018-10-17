namespace x3e
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using components;
    using Cureos.Measures.Quantities;
    using MoreLinq;
    using RC.Framework.Screens;

    public class Simulator<SRod> where SRod : EmptyRod, new()
    {
        private readonly IEnumerable<ActiveZone<SRod>> _collection;
        private readonly CancellationTokenSource SimulationTokenSource = new CancellationTokenSource();

        public static Simulator<TRod> Setup<TRod>(int activeZone) where TRod : EmptyRod, new()
        {
            if(activeZone % 2 != 0)
                throw new Exception("Error: ActiveZones grid is not symmetric.");

            var listActiveZones = new List<ActiveZone<TRod>>();

            for (;activeZone != 0; activeZone--)
                listActiveZones.Add(new ActiveZone<TRod>());

            return new Simulator<TRod>(listActiveZones);
        }

        private Simulator(IEnumerable<ActiveZone<SRod>> collection) => _collection = collection;


        public void WarmUpZone()
        {
            foreach (var activeZone in _collection)
                activeZone.WarmUp();
        }

        public void NextReaction()
        {
            foreach (var activeZone in _collection)
                activeZone.NextReaction();
        }


        public void Simulate()
        {
            var task = new Task(() =>
            {
                while (!SimulationTokenSource.IsCancellationRequested)
                {
                    NextReaction();
                    Thread.Sleep(20);
                }

                foreach (var activeZone in _collection)
                    activeZone.Extract(false, false);

            }, SimulationTokenSource.Token);
            task.Start();
        }

        public void ShutdownReaction()
        {
            SimulationTokenSource.Cancel();
            while (_collection.All(x => x.Status != ActiveZoneStatus.Extracted))  { Thread.Sleep(200); }

            var b = _collection.Sum(x => x.GetComponent<EtherRod>().ExtractedPower);
            var genpower = new Power(b, Power.GigaWatt);
            var rod1 = _collection.MaxBy(x => x.GetComponent<EtherRod>().MaxTemperature).First();
            var rod2 = _collection.MinBy(x => x.GetComponent<EtherRod>().MinTemperature).First();
            var maxtmp = new Temperature(rod1.GetComponent<EtherRod>().MaxTemperature, Temperature.Celsius);
            var mintmp = new Temperature(rod2.GetComponent<EtherRod>().MinTemperature, Temperature.Celsius);
            Screen.WriteLine($"[{"W".To(Color.Fuchsia)}] Generated power: {$"{genpower[Power.GigaWatt].ToString("##.##")}".To(Color.DeepSkyBlue)}");
            Screen.WriteLine($"[{"W".To(Color.Fuchsia)}] Maximal Temperature: {$"{maxtmp[Temperature.Celsius].ToString("##.##")}".To(Color.Orange)} in [ZoneID: {rod1.UID.To(Color.Yellow)}]");
            Screen.WriteLine($"[{"W".To(Color.Fuchsia)}] Minimal Temperature: {$"{mintmp[Temperature.Celsius].ToString("##.##")}".To(Color.CornflowerBlue)} in [ZoneID: {rod2.UID.To(Color.Yellow)}]");
            Screen.WriteLine($"Molten rods: {_collection.Count(x => x.Status.HasFlag(ActiveZoneStatus.EmergencyStop))}".To(Color.Red));
        }
    }
}
