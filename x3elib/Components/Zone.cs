﻿namespace x3e.components
{
    using System;
    using System.Linq;
    using simulation;

    public abstract class Zone : SimulatorObject
    {
        public new string UID { get; } = Guid.NewGuid().ToString().Split('-').First();
        public abstract void NextReaction();
        public override void Update() => NextReaction();
    }
}