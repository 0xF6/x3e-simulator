using System;

namespace Simulator
{
    using System.Threading;
    using ERSTest;
    using x3e;
    using x3e.components;
    using x3e.webapi;

    class Program
    {
        static void Main(string[] args)
        {

            var sim = Simulator<EtherRod>.Setup<EtherRod>(1);


            TestModule.Run(sim);
            Thread.Sleep(5000);
            new Thread(sim.Simulate).Start();
            Thread.Sleep(2000);
            Console.ReadKey();
            sim.IsStop = true;
            Console.ReadKey();
        }
    }
}
