//[assembly: AspMvcViewLocationFormat("~/Features/{1}/{0}.cshtml")]
//[assembly: AspMvcViewLocationFormat("~/Features/Shared/{0}.cshtml")]
namespace Simulator
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.Razor;

    public class FolderLocationExpander : IViewLocationExpander
    {
        public void PopulateValues(ViewLocationExpanderContext context)
        {
        }

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            return new[]
            {
                "/Pages/{1}/{0}.cshtml",
                "/Pages/Shared/{0}.cshtml"
            };
        }
    }
}