namespace x3e.simulation
{
    using System;
    using System.Linq;
    using components;
    using Storage = System.Collections.Generic.Dictionary<System.Guid, SimulatorObject> ;
    public static class SimulationStorage
    {
        public static Storage StorageObjects { get; } = new Storage();


        public static void Register(SimulatorObject obj) => StorageObjects.Add(obj.UID, obj);

        public static EtherRod GetEtherRod() => (EtherRod)StorageObjects
            .FirstOrDefault(x => x.Key == Guid.Parse("02194DED-A70A-46C9-B981-0D8B2DE01DE7")).Value;
    }
}