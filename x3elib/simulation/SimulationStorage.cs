namespace x3e.simulation
{
    using System;
    using Storage = System.Collections.Generic.Dictionary<System.Guid, SimulatorObject> ;
    public static class SimulationStorage
    {
        public static Storage StorageObjects { get; } = new Storage();


        public static void Register(SimulatorObject obj) => StorageObjects.Add(obj.UID, obj);
    }
}