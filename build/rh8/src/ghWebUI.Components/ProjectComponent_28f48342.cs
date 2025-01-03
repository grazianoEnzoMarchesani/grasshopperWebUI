using System;
using SD = System.Drawing;
using SWF = System.Windows.Forms;

using Rhino.Geometry;

using Grasshopper.Kernel;

namespace RhinoCodePlatform.Rhino3D.Projects.Plugin.GH
{
  public sealed class ProjectComponent_28f48342 : ProjectComponent_Base
  {
    static readonly string s_scriptData = "ewogICJ0eXBlIjogInNjcmlwdCIsCiAgInNjcmlwdCI6IHsKICAgICJsYW5ndWFnZSI6IHsKICAgICAgImlkIjogIiouKi5weXRob24iLAogICAgICAidmVyc2lvbiI6ICIzLiouKiIKICAgIH0sCiAgICAidGl0bGUiOiAidGVtcCBmaWxlIGxvY2F0aW9uIiwKICAgICJ0ZXh0IjogImFXMXdiM0owSUhSbGJYQm1hV3hsQ21sdGNHOXlkQ0J2Y3dvS2RHVnRjRjlrYVhJZ1BTQjBaVzF3Wm1sc1pTNW5aWFIwWlcxd1pHbHlLQ2tLY0hKcGJuUW9aaUpFYVhKbFkzUnZjbmtnZEdWdGNHOXlZVzVsWVRvZ2UzUmxiWEJmWkdseWZTSXBDZ29qSUZabGNtbG1hV05oSUd4aElIQnlaWE5sYm5waElHUmxhU0JtYVd4bElITndaV05wWm1samFRcHFjMjl1WDJsdWRHVnlabUZqWlNBOUlHOXpMbkJoZEdndWFtOXBiaWgwWlcxd1gyUnBjaXdnSjJkb1gybHVkR1Z5Wm1GalpWOWtZWFJoTG1wemIyNG5LUXBxYzI5dVgyZGxiMjFsZEhKNUlEMGdiM011Y0dGMGFDNXFiMmx1S0hSbGJYQmZaR2x5TENBbloyaGZaMlZ2YldWMGNubGZaR0YwWVM1cWMyOXVKeWtLYUhSdGJGOTBaVzF3SUQwZ2IzTXVjR0YwYUM1cWIybHVLSFJsYlhCZlpHbHlMQ0FuWjJoZmFXNTBaWEptWVdObExtaDBiV3duS1FvS2NISnBiblFvWmlKY2JrWnBiR1VnWkdWc2JDZHBiblJsY21aaFkyTnBZVG9nZTJwemIyNWZhVzUwWlhKbVlXTmxmU0lwQ25CeWFXNTBLR1lpUlhOcGMzUmxPaUI3YjNNdWNHRjBhQzVsZUdsemRITW9hbk52Ymw5cGJuUmxjbVpoWTJVcGZTSXBDZ3B3Y21sdWRDaG1JbHh1Um1sc1pTQmtaV3hzWVNCblpXOXRaWFJ5YVdFNklIdHFjMjl1WDJkbGIyMWxkSEo1ZlNJcENuQnlhVzUwS0dZaVJYTnBjM1JsT2lCN2IzTXVjR0YwYUM1bGVHbHpkSE1vYW5OdmJsOW5aVzl0WlhSeWVTbDlJaWs9IiwKICAgICJpZCI6ICIyOGY0ODM0Mi0wY2U4LTQxYmQtYTU4YS04Yzc3Y2NjODBlMjciLAogICAgIm5pY2tuYW1lIjogInRlbXAgZmlsZSBsb2NhdGlvbiIKICB9Cn0=";
    static readonly string s_scriptIconData = "iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAEsWlUWHRYTUw6Y29tLmFkb2JlLnhtcAAAAAAAPD94cGFja2V0IGJlZ2luPSLvu78iIGlkPSJXNU0wTXBDZWhpSHpyZVN6TlRjemtjOWQiPz4KPHg6eG1wbWV0YSB4bWxuczp4PSJhZG9iZTpuczptZXRhLyIgeDp4bXB0az0iWE1QIENvcmUgNS41LjAiPgogPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4KICA8cmRmOkRlc2NyaXB0aW9uIHJkZjphYm91dD0iIgogICAgeG1sbnM6dGlmZj0iaHR0cDovL25zLmFkb2JlLmNvbS90aWZmLzEuMC8iCiAgICB4bWxuczpleGlmPSJodHRwOi8vbnMuYWRvYmUuY29tL2V4aWYvMS4wLyIKICAgIHhtbG5zOnBob3Rvc2hvcD0iaHR0cDovL25zLmFkb2JlLmNvbS9waG90b3Nob3AvMS4wLyIKICAgIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyIKICAgIHhtbG5zOnhtcE1NPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvbW0vIgogICAgeG1sbnM6c3RFdnQ9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZUV2ZW50IyIKICAgdGlmZjpJbWFnZUxlbmd0aD0iMjQiCiAgIHRpZmY6SW1hZ2VXaWR0aD0iMjQiCiAgIHRpZmY6UmVzb2x1dGlvblVuaXQ9IjIiCiAgIHRpZmY6WFJlc29sdXRpb249IjIzLzEiCiAgIHRpZmY6WVJlc29sdXRpb249IjIzLzEiCiAgIGV4aWY6UGl4ZWxYRGltZW5zaW9uPSIyNCIKICAgZXhpZjpQaXhlbFlEaW1lbnNpb249IjI0IgogICBleGlmOkNvbG9yU3BhY2U9IjEiCiAgIHBob3Rvc2hvcDpDb2xvck1vZGU9IjMiCiAgIHBob3Rvc2hvcDpJQ0NQcm9maWxlPSJzUkdCIElFQzYxOTY2LTIuMSIKICAgeG1wOk1vZGlmeURhdGU9IjIwMjQtMDctMThUMTU6MTQ6NTAtMDc6MDAiCiAgIHhtcDpNZXRhZGF0YURhdGU9IjIwMjQtMDctMThUMTU6MTQ6NTAtMDc6MDAiPgogICA8eG1wTU06SGlzdG9yeT4KICAgIDxyZGY6U2VxPgogICAgIDxyZGY6bGkKICAgICAgc3RFdnQ6YWN0aW9uPSJwcm9kdWNlZCIKICAgICAgc3RFdnQ6c29mdHdhcmVBZ2VudD0iQWZmaW5pdHkgUGhvdG8gMiAyLjUuMiIKICAgICAgc3RFdnQ6d2hlbj0iMjAyNC0wNy0xOFQxNToxNDo1MC0wNzowMCIvPgogICAgPC9yZGY6U2VxPgogICA8L3htcE1NOkhpc3Rvcnk+CiAgPC9yZGY6RGVzY3JpcHRpb24+CiA8L3JkZjpSREY+CjwveDp4bXBtZXRhPgo8P3hwYWNrZXQgZW5kPSJyIj8+PFPNtgAAAYFpQ0NQc1JHQiBJRUM2MTk2Ni0yLjEAACiRdZG7SwNBEIc/EyWikYiKWFgEUatEokLQxiLBF6hFjGDUJrm8hDyOuwQJtoJtQEG08VXoX6CtYC0IiiKItbWijYZzLglExMwyO9/+dmfYnQVLMKWk9UYPpDM5LTDtcy6HVpy2V2x00E0r3rCiq/OLU0Hq2ucDDWa8c5u16p/711qjMV2BhmbhCUXVcsIzwnMbOdXkXeEuJRmOCp8LuzS5oPC9qUcq/GpyosLfJmvBgB8s7cLOxC+O/GIlqaWF5eX0p1N5pXof8yX2WGZpUWKfeC86Aabx4WSWSfx4GWZcZi9uRhiSFXXyPeX8BbKSq8isUkBjnQRJcrhEzUv1mMS46DEZKQpm///2VY+PjlSq233Q9GIY7wNg24FS0TC+jg2jdALWZ7jK1PKzRzD2IXqxpvUfgmMLLq5rWmQPLreh50kNa+GyZBW3xOPwdgZtIei8hZbVSs+q+5w+QnBTvuoG9g9gUM471n4AjtNn+CCeQOAAAAAJcEhZcwAAA4oAAAOKAaeM9R8AAAH2SURBVEiJ1ZU/ixpBGMZ/M86aI+RMIFuKfgu7Bb9DCCgSAlYHKdIdgTSpQgrJ1zCw/dlb+jHEMkb0skvi/EtxrsypezmXNHlh2Nl333meeWZm54H/PURJXu5a2fey8IDbtWOCbrerWq3WZ+/9lbX28hxkpdStMebbdDr9sFgsbgENoMKidrv9sdPpvB8Oh3WlFNZajDFYa7HW4pzDObfve+/3z+VyeZmm6ZskSdR4PL4G1oAOCSTwbjAY1OfzOXmeI4RASomUct8vy8VxTL/fvxiNRq+BT8AvwMqQQGv9sl6vnw1e5BqNBsaYZ8ATIAJkSAAgrbWVwIUQWGsLnGi3IiIkEADW2krgUkqMMeFy3+8UURCcCy6ECAlEKYFzrhK4lDJcIkoJDhXIo20KBh/Uaa2PatRhIlRQANRqtZOzLkhObHK5AufcEdhDCsK6UwpKCarsw6MUeO8rb3IlBeeoCY5pOQFQ+U8OCPxJAqXUZrVaVQLfbDbkeY5S6ieBH4TH1Dvnxmmavu31ehdxHOO9Z7vd7q9sYwzGGLTW996NMazXa2az2TbLshvuvMABPjQc2Ww2XyRJ8jWKole7W/HRoZTKsiy7mUwmX/I8/w78APJDS4yA58BTgiuXv1tnYZUa+A3k7Azn1MDoAPycCEk0D8zsn5n+H1yr/u4xAeJ3AAAAAElFTkSuQmCC";

    public override Guid ComponentGuid { get; } = new Guid("28f48342-0ce8-41bd-a58a-8c77ccc80e27");

    public override GH_Exposure Exposure { get; } = GH_Exposure.primary;

    public override bool Obsolete { get; } = false;

    public ProjectComponent_28f48342() : base(s_scriptData, s_scriptIconData,
        name: "temp file location",
        nickname: "temp file location",
        description: "",
        category: "ghWebUI",
        subCategory: "Default"
        )
    {
    }

    protected override void AppendAdditionalComponentMenuItems(SWF.ToolStripDropDown menu)
    {
      base.AppendAdditionalComponentMenuItems(menu);
      if (m_script is null) return;
      m_script.AppendAdditionalMenuItems(this, menu);
    }

    protected override void RegisterInputParams(GH_InputParamManager _) { }

    protected override void RegisterOutputParams(GH_OutputParamManager _) { }

    protected override void BeforeSolveInstance()
    {
      if (m_script is null) return;
      m_script.BeforeSolve(this);
    }

    protected override void SolveInstance(IGH_DataAccess DA)
    {
      if (m_script is null) return;
      m_script.Solve(this, DA);
    }

    protected override void AfterSolveInstance()
    {
      if (m_script is null) return;
      m_script.AfterSolve(this);
    }

    public override void RemovedFromDocument(GH_Document document)
    {
      ProjectComponentPlugin.DisposeScript(this, m_script);
      base.RemovedFromDocument(document);
    }

    public override BoundingBox ClippingBox
    {
      get
      {
        if (m_script is null) return BoundingBox.Empty;
        return m_script.GetClipBox(this);
      }
    }

    public override void DrawViewportWires(IGH_PreviewArgs args)
    {
      if (m_script is null) return;
      m_script.DrawWires(this, args);
    }

    public override void DrawViewportMeshes(IGH_PreviewArgs args)
    {
      if (m_script is null) return;
      m_script.DrawMeshes(this, args);
    }
  }
}
