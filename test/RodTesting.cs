namespace ERSTest
{
    using System.Threading;
    using EtherReactorSimulator.Components;
    using Xunit;

    public class RodTesting
    {
        [Fact]
        public void Test1()
        {
            var activeZone1 = new ActiveZone<EtherRod>();
            var activeZone2 = new ActiveZone<EtherRod>();
            var activeZone3 = new ActiveZone<EtherRod>();
            var activeZone4 = new ActiveZone<EtherRod>();
            
            activeZone1.WarmUp();
            activeZone2.WarmUp();
            activeZone3.WarmUp();
            activeZone4.WarmUp();
            while (true)
            {
                activeZone1.NextReaction();
                activeZone2.NextReaction();
                activeZone3.NextReaction();
                activeZone4.NextReaction();
                Thread.Sleep(20);
            }
        }
    }
}
