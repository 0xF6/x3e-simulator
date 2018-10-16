using System;

namespace Simulator
{
    using System.Threading;
    using ERSTest;
    using x3e;
    using x3e.components;

    class Program
    {
        static void Main(string[] args)
        {

            var sim = Simulator<EtherRod>.Setup<EtherRod>(4);

            sim.WarmUpZone();

            sim.Simulate();
            Thread.Sleep(2000);
            Console.ReadKey();
            sim.ShutdownReaction();
            Console.ReadKey();
        }
    }
}
