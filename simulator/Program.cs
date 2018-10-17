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

            var sim = Simulator<EtherRod>.Setup<EtherRod>(12);
            

            new Thread(sim.Simulate).Start();
            Thread.Sleep(2000);
            Console.ReadKey();
            sim.IsStop = true;
            Console.ReadKey();
        }
    }
}
