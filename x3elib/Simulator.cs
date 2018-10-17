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
    using simulation;

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

        public void Simulate()
        {
            foreach (var o in SimulationStorage.StorageObjects)
                o.Value.Start();
            while (!IsStop)
            {
                foreach (var o in SimulationStorage.StorageObjects)
                    o.Value.Update();
                Thread.Sleep(20);
            }
            foreach (var o in SimulationStorage.StorageObjects)
                o.Value.Clear();
        }
        public bool IsStop { get; set; }
    }
}
