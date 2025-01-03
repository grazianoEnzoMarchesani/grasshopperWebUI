using System;
using System.IO;
using SD = System.Drawing;

using Rhino.Geometry;

using Grasshopper.Kernel;

namespace RhinoCodePlatform.Rhino3D.Projects.Plugin.GH
{
  public abstract class ProjectComponent_Base : GH_Component, IGH_PreviewObject
  {
    protected readonly SD.Bitmap m_icon = default;
    protected readonly dynamic m_script;

    protected override SD.Bitmap Icon => m_icon;

    public ProjectComponent_Base(string scriptData, string scriptIconData, string name, string nickname, string description, string category, string subCategory)
      : base(name, nickname, description, category, subCategory)
    {
      if (ProjectComponentPlugin.TryCreateScript(this, scriptData, out m_script))
      {
        if (!scriptIconData.Contains("COMPONENT-ICON"))
        {
          using (var sicon = new MemoryStream(Convert.FromBase64String(scriptIconData)))
            m_icon = new SD.Bitmap(sicon);
        }
      }
      else
      {
        AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Scripting platform is not ready.");
      }
    }

    #region Syncing Component Enabled and Preview States
    public override bool Locked
    {
      get => m_script.Locked;
      set => m_script.Locked = value;
    }

    bool IGH_PreviewObject.Hidden
    {
      get => m_script.Hidden;
      set => m_script.Hidden = value;
    }
    #endregion

    public override void AddedToDocument(GH_Document document)
    {
      base.AddedToDocument(document);
      m_script?.AddedToDocument(this, document);
    }

    public override void RemovedFromDocument(GH_Document document)
    {
      base.RemovedFromDocument(document);
      m_script?.RemovedFromDocument(this, document);
    }
  }
}
