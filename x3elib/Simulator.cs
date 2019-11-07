namespace x3e
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using components;
    using simulation;

    public class Simulator<SRod> where SRod : EmptyRod, new()
    {
        private readonly IEnumerable<ActiveZone<SRod>> _collection;
        private readonly CancellationTokenSource SimulationTokenSource = new CancellationTokenSource();

        public static Simulator<TRod> Setup<TRod>(int activeZone) where TRod : EmptyRod, new()
        {
            //if(activeZone % 2 != 0)
            //    throw new Exception("Error: ActiveZones grid is not symmetric.");

            var listActiveZones = new List<ActiveZone<TRod>>();

            for (;activeZone != 0; activeZone--)
                listActiveZones.Add(new ActiveZone<TRod>());

            return new Simulator<TRod>(listActiveZones);
        }

        private Simulator(IEnumerable<ActiveZone<SRod>> collection) => _collection = collection;

        public async Task Simulate(CancellationToken cancellationToken)
        {
            foreach (var o in SimulationStorage.StorageObjects)
                o.Value.Start();
            while (!cancellationToken.IsCancellationRequested)
            {
                foreach (var o in SimulationStorage.StorageObjects)
                    o.Value.Update();
                await Task.Delay(100, cancellationToken);
            }
            foreach (var o in SimulationStorage.StorageObjects)
                o.Value.Clear();
        }
    }
}
