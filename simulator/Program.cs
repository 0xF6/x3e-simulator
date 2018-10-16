using System;

namespace Simulator
{
    using System.Threading;
    using ERSTest;

    class Program
    {
        static void Main(string[] args)
        {
            new Thread(new RodTesting().Test1).Start();
            Console.ReadLine();

        }
    }
}
