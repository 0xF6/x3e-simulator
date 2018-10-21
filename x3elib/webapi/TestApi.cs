namespace x3e.webapi
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading;
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
            Get["/"] = _ => File.ReadAllText(@".\index.html");
            Get["/temperature"] = _ => ((int)SimulationStorage.GetEtherRod().Temperature).ToString();
            Get["/load"] = _ => ((int) Math.Round(SimulationStorage.GetEtherRod().Load, 2)).ToString();
            Get["/volume"] = _ => ((int)SimulationStorage.GetEtherRod().Volume).ToString();
            Get["/stop_reaction"] = _ =>
            {
                SimulationStorage.GetEtherRod().
                    GetComponent<ReactionZone<EtherRod>>().
                    GetComponent<ActiveZone<EtherRod>>().
                    GetComponent<AnalogZone<EtherRod>>().Shutdown();
                return "ok";
            };
            Get["/start_reaction"] = _ =>
            {
                SimulationStorage.GetEtherRod().
                    GetComponent<ReactionZone<EtherRod>>().
                    GetComponent<ActiveZone<EtherRod>>().
                    GetComponent<AnalogZone<EtherRod>>().Start();
                return "ok";
            };
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
            Thread.Sleep(200);
            Process.Start("http://localhost/");
        }
    }
}