namespace Simulator
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using x3e;
    using x3e.components;

    public class SimulatorBridge : BackgroundService
    {
        private readonly Simulator<EtherRod> _simulator;

        public SimulatorBridge(Simulator<EtherRod> simulator) => _simulator = simulator;

        #region Implementation of IHostedService

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _simulator.Simulate(stoppingToken);
        }

        #endregion
    }
}