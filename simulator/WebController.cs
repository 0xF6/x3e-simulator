namespace Simulator
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using x3e.simulation;

    public class WebController : Controller
    {
        public IActionResult Index() => View();
        [HttpGet("/api/v1/gate/temperature")]
        public IActionResult Temperature() => Json(((int)SimulationStorage.GetEtherRod().Temperature).ToString());

        [HttpGet("/api/v1/gate/volume")]
        public IActionResult Volume() => Json(((int) SimulationStorage.GetEtherRod().Volume).ToString());

        [HttpGet("/api/v1/gate/load")]
        public IActionResult Load() => Json(((int) Math.Round(SimulationStorage.GetEtherRod().Load, 2)).ToString());
    }
}