using System;
using System.IO;
using System.Text;
using SD = System.Drawing;

using Rhino;
using Grasshopper.Kernel;

namespace RhinoCodePlatform.Rhino3D.Projects.Plugin.GH
{
  public sealed class AssemblyInfo : GH_AssemblyInfo
  {
    static readonly string s_assemblyIconData = "[[ASSEMBLY-ICON]]";
    static readonly string s_categoryIconData = "[[ASSEMBLY-CATEGORY-ICON]]";

    public static readonly SD.Bitmap PluginIcon = default;
    public static readonly SD.Bitmap PluginCategoryIcon = default;

    static AssemblyInfo()
    {
      if (!s_assemblyIconData.Contains("ASSEMBLY-ICON"))
      {
        using (var aicon = new MemoryStream(Convert.FromBase64String(s_assemblyIconData)))
          PluginIcon = new SD.Bitmap(aicon);
      }

      if (!s_categoryIconData.Contains("ASSEMBLY-CATEGORY-ICON"))
      {
        using (var cicon = new MemoryStream(Convert.FromBase64String(s_categoryIconData)))
          PluginCategoryIcon = new SD.Bitmap(cicon);
      }
    }

    public override Guid Id { get; } = new Guid("38d85b70-0057-4b41-9dda-ae3b979b7b64");

    public override string AssemblyName { get; } = "ghWebUI.Components";
    public override string AssemblyVersion { get; } = "0.2.1.9134";
    public override string AssemblyDescription { get; } = "A universal web interface for Grasshopper that enables interactive 3D geometry visualization and manipulation through standard web browsers. Built with Python and Three.js, it provides cross-platform compatibility between Windows and Mac operating systems.";
    public override string AuthorName { get; } = "Graziano Enzo Marchesani";
    public override string AuthorContact { get; } = "graziano.marchesani@unicam.it";
    public override GH_LibraryLicense AssemblyLicense { get; } = GH_LibraryLicense.unset;
    public override SD.Bitmap AssemblyIcon { get; } = PluginIcon;
  }

  public class ProjectComponentPlugin : GH_AssemblyPriority
  {
    static readonly Guid s_projectId = new Guid("38d85b70-0057-4b41-9dda-ae3b979b7b64");
    static readonly string s_projectData = "ewogICJob3N0IjogewogICAgIm5hbWUiOiAiUmhpbm8zRCIsCiAgICAidmVyc2lvbiI6ICI4LjE0LjI0MzQ1XHUwMDJCMTUwMDIiLAogICAgIm9zIjogIm1hY09TIiwKICAgICJhcmNoIjogImFybTY0IgogIH0sCiAgImlkIjogIjM4ZDg1YjcwLTAwNTctNGI0MS05ZGRhLWFlM2I5NzliN2I2NCIsCiAgImlkZW50aXR5IjogewogICAgIm5hbWUiOiAiZ2hXZWJVSSIsCiAgICAidmVyc2lvbiI6ICIwLjIuMSIsCiAgICAicHVibGlzaGVyIjogewogICAgICAiZW1haWwiOiAiZ3Jhemlhbm8ubWFyY2hlc2FuaUB1bmljYW0uaXQiLAogICAgICAibmFtZSI6ICJHcmF6aWFubyBFbnpvIE1hcmNoZXNhbmkiLAogICAgICAiY29tcGFueSI6ICJVbmljYW0iLAogICAgICAiY291bnRyeSI6ICJJdGFseSIsCiAgICAgICJ1cmwiOiAiaHR0cHM6Ly93d3cuZ3Jhemlhbm9lbnpvbWFyY2hlc2FuaS54eXovIgogICAgfSwKICAgICJkZXNjcmlwdGlvbiI6ICJBIHVuaXZlcnNhbCB3ZWIgaW50ZXJmYWNlIGZvciBHcmFzc2hvcHBlciB0aGF0IGVuYWJsZXMgaW50ZXJhY3RpdmUgM0QgZ2VvbWV0cnkgdmlzdWFsaXphdGlvbiBhbmQgbWFuaXB1bGF0aW9uIHRocm91Z2ggc3RhbmRhcmQgd2ViIGJyb3dzZXJzLiBCdWlsdCB3aXRoIFB5dGhvbiBhbmQgVGhyZWUuanMsIGl0IHByb3ZpZGVzIGNyb3NzLXBsYXRmb3JtIGNvbXBhdGliaWxpdHkgYmV0d2VlbiBXaW5kb3dzIGFuZCBNYWMgb3BlcmF0aW5nIHN5c3RlbXMuIiwKICAgICJjb3B5cmlnaHQiOiAiQ29weXJpZ2h0IFx1MDBBOSAyMDI1IEdyYXppYW5vIEVuem8gTWFyY2hlc2FuaSIsCiAgICAibGljZW5zZSI6ICJHUEwgMy4wIiwKICAgICJ1cmwiOiAiaHR0cHM6Ly9naXRodWIuY29tL2dyYXppYW5vRW56b01hcmNoZXNhbmkvZ3Jhc3Nob3BwZXJXZWJVSSIKICB9LAogICJzZXR0aW5ncyI6IHsKICAgICJidWlsZFBhdGgiOiAiZmlsZTovLy9Vc2Vycy9ncmF6aWFub2Vuem9tYXJjaGVzYW5pL0RvY3VtZW50cy9HaXRIdWIvZ3Jhc3Nob3BwZXJfaW50ZXJmYWNlL2J1aWxkL3JoOCIsCiAgICAiYnVpbGRUYXJnZXQiOiB7CiAgICAgICJob3N0IjogewogICAgICAgICJuYW1lIjogIlJoaW5vM0QiLAogICAgICAgICJ2ZXJzaW9uIjogIjgiCiAgICAgIH0sCiAgICAgICJ0aXRsZSI6ICJSaGlubzNEICg4LiopIiwKICAgICAgInNsdWciOiAicmg4IgogICAgfSwKICAgICJwdWJsaXNoVGFyZ2V0IjogewogICAgICAidGl0bGUiOiAiTWNOZWVsIFlhayBTZXJ2ZXIiCiAgICB9CiAgfSwKICAiY29kZXMiOiBbXQp9";
    static readonly dynamic s_projectServer = default;
    static readonly object s_project = default;

    static ProjectComponentPlugin()
    {
      s_projectServer = ProjectInterop.GetProjectServer();
      if (s_projectServer is null)
      {
        RhinoApp.WriteLine($"Error loading Grasshopper plugin. Missing Rhino3D platform");
        return;
      }

      // get project
      dynamic dctx = ProjectInterop.CreateInvokeContext();
      dctx.Inputs["projectAssembly"] = typeof(ProjectComponentPlugin).Assembly;
      dctx.Inputs["projectId"] = s_projectId;
      dctx.Inputs["projectData"] = s_projectData;

      object project = default;
      if (s_projectServer.TryInvoke("plugins/v1/deserialize", dctx)
            && dctx.Outputs.TryGet("project", out project))
      {
        // server reports errors
        s_project = project;
      }
    }

    public override GH_LoadingInstruction PriorityLoad()
    {
      if (AssemblyInfo.PluginCategoryIcon is SD.Bitmap icon)
      {
        Grasshopper.Instances.ComponentServer.AddCategoryIcon("ghWebUI", icon);
      }
      Grasshopper.Instances.ComponentServer.AddCategorySymbolName("ghWebUI", "ghWebUI"[0]);

      return GH_LoadingInstruction.Proceed;
    }

    public static bool TryCreateScript(GH_Component ghcomponent, string serialized, out object script)
    {
      script = default;

      if (s_projectServer is null) return false;

      dynamic dctx = ProjectInterop.CreateInvokeContext();
      dctx.Inputs["component"] = ghcomponent;
      dctx.Inputs["project"] = s_project;
      dctx.Inputs["scriptData"] = serialized;

      if (s_projectServer.TryInvoke("plugins/v1/gh/deserialize", dctx))
      {
        return dctx.Outputs.TryGet("script", out script);
      }

      return false;
    }

    public static void DisposeScript(GH_Component ghcomponent, object script)
    {
      if (script is null)
        return;

      dynamic dctx = ProjectInterop.CreateInvokeContext();
      dctx.Inputs["component"] = ghcomponent;
      dctx.Inputs["project"] = s_project;
      dctx.Inputs["script"] = script;

      if (!s_projectServer.TryInvoke("plugins/v1/gh/dispose", dctx))
        throw new Exception("Error disposing Grasshopper script component");
    }
  }
}
