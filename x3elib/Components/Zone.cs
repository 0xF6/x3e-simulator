namespace EtherReactorSimulator.Components
{
    using System;
    using System.Linq;

    public abstract class Zone
    {
        public string UID { get; } = Guid.NewGuid().ToString().Split('-').First();
        public abstract void NextReaction();
        public abstract void Extract(bool emergency = false);
    }
}