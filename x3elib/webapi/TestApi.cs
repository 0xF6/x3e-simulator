namespace x3e.webapi
{
    using System;
    using System.IO;
    using System.Linq;
    using components;
    using Nancy;
    using Nancy.Bootstrapper;
    using Nancy.Hosting.Self;
    using Nancy.TinyIoc;
    using RC.Framework.Screens;
    using simulation;

    public class TestModule : Nancy.NancyModule
    {
        public static Simulator<EtherRod> Simulator;
        public TestModule()
        {
            Get["/"] = _ => File.ReadAllText(@"C:\Git\x3e-simulator\graph\index.html");
            Get["/temperature"] = _ => ((int)SimulationStorage.GetEtherRod().Temperature).ToString();
        }
        public static void Run(Simulator<EtherRod> sim)
        {
            Simulator = sim;
            var hostConfigs = new HostConfiguration
            {
                UrlReservations = new UrlReservations() { CreateAutomatically = true }
            };
            hostConfigs.UnhandledExceptionCallback += Console.WriteLine;
            hostConfigs.RewriteLocalhost = true;
            
            var host = new NancyHost(hostConfigs, new Uri("http://localhost/"));
            host.Start();
            Screen.WriteLine("Started.");
        }
    }
}