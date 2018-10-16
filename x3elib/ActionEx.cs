namespace EtherReactorSimulator
{
    using System;
    using System.Threading;

    public static class ActionEx
    {
        public static void Thread(this Action act) => new Thread(() => { act(); }).Start();
    }
}