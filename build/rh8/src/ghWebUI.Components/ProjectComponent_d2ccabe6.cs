using System;
using SD = System.Drawing;
using SWF = System.Windows.Forms;

using Rhino.Geometry;

using Grasshopper.Kernel;

namespace RhinoCodePlatform.Rhino3D.Projects.Plugin.GH
{
  public sealed class ProjectComponent_d2ccabe6 : ProjectComponent_Base
  {
    static readonly string s_scriptData = "ewogICJ0eXBlIjogInNjcmlwdCIsCiAgInNjcmlwdCI6IHsKICAgICJsYW5ndWFnZSI6IHsKICAgICAgImlkIjogIiouKi5weXRob24iLAogICAgICAidmVyc2lvbiI6ICIzLiouKiIKICAgIH0sCiAgICAidGl0bGUiOiAidmFsdWUgdXBkYXRlIiwKICAgICJ0ZXh0IjogIklpSWlRMjl0Y0c5dVpXNTBaU0J3WlhJZ2JDZGhaMmRwYjNKdVlXMWxiblJ2SUdSbGFTQjJZV3h2Y21raUlpSUthVzF3YjNKMElHOXpDbWx0Y0c5eWRDQjBaVzF3Wm1sc1pRcHBiWEJ2Y25RZ1IzSmhjM05vYjNCd1pYSWdZWE1nWjJnS0NpTWdTVzV3ZFhRNkNpTWdJQ0J0YjI1cGRHOXlPaUJwYzNSaGJucGhJR1JsYkNCV1lXeDFaVTF2Ym1sMGIzSUtJeUFnSUdOdmJuUnliMnhmYVdRNklFbEVJR1JsYkNCamIyNTBjbTlzYkc4Z1pHRWdiVzl1YVhSdmNtRnlaUW9qSUNBZ2RISnBaMmRsY2pvZ2FXNXdkWFFnZEhKcFoyZGxjaUJ3WlhJZ1ptOXllbUZ5WlNCc0oyRm5aMmx2Y201aGJXVnVkRzhnS0c1dmJpQjFjMkYwYnlCa2FYSmxkSFJoYldWdWRHVXBDZ29qSUZaaGNtbGhZbWxzYVNCbmJHOWlZV3hwSUhCbGNpQnNZU0JqWVdOb1pRcGZiR0Z6ZEY5dGIyUnBabWxsWkY5MGFXMWxJRDBnTUFwZlkyRmphR1ZrWDNaaGJIVmxJRDBnVG05dVpRcGZhbk52Ymw5d1lYUm9JRDBnYjNNdWNHRjBhQzVxYjJsdUtIUmxiWEJtYVd4bExtZGxkSFJsYlhCa2FYSW9LU3dnSjJkb1gybHVkR1Z5Wm1GalpWOWtZWFJoTG1wemIyNG5LUW9LWkdWbUlIVndaR0YwWlY5MllXeDFaU2h0YjI1cGRHOXlMQ0JqYjI1MGNtOXNYMmxrS1RvS0lDQWdJR2RzYjJKaGJDQmZiR0Z6ZEY5dGIyUnBabWxsWkY5MGFXMWxMQ0JmWTJGamFHVmtYM1poYkhWbENpQWdJQ0FLSUNBZ0lHbG1JRzF2Ym1sMGIzSWdhWE1nVG05dVpUb0tJQ0FnSUNBZ0lDQnlaWFIxY200Z1RtOXVaUW9nSUNBZ0NpQWdJQ0IwY25rNkNpQWdJQ0FnSUNBZ0l5QldaWEpwWm1sallTQnpaU0JwYkNCbWFXeGxJR1Z6YVhOMFpRb2dJQ0FnSUNBZ0lHbG1JRzV2ZENCdmN5NXdZWFJvTG1WNGFYTjBjeWhmYW5OdmJsOXdZWFJvS1RvS0lDQWdJQ0FnSUNBZ0lDQWdjbVYwZFhKdUlFNXZibVVLSUNBZ0lDQWdJQ0FnSUNBZ0NpQWdJQ0FnSUNBZ0l5QlBkSFJwWlc1bElHbHNJSFJwYldWemRHRnRjQ0JrYVNCMWJIUnBiV0VnYlc5a2FXWnBZMkVnWkdWc0lHWnBiR1VLSUNBZ0lDQWdJQ0JqZFhKeVpXNTBYMjF2WkdsbWFXVmtYM1JwYldVZ1BTQnZjeTV3WVhSb0xtZGxkRzEwYVcxbEtGOXFjMjl1WDNCaGRHZ3BDaUFnSUNBZ0lDQWdDaUFnSUNBZ0lDQWdJeUJUWlNCcGJDQm1hV3hsSU1Pb0lITjBZWFJ2SUcxdlpHbG1hV05oZEc4c0lHRm5aMmx2Y201aElHeGhJR05oWTJobElHVWdabTl5ZW1FZ2JDZGhaMmRwYjNKdVlXMWxiblJ2Q2lBZ0lDQWdJQ0FnYVdZZ1kzVnljbVZ1ZEY5dGIyUnBabWxsWkY5MGFXMWxJRDRnWDJ4aGMzUmZiVzlrYVdacFpXUmZkR2x0WlRvS0lDQWdJQ0FnSUNBZ0lDQWdkbUZzZFdVZ1BTQnRiMjVwZEc5eUxtZGxkRjkyWVd4MVpTaGpiMjUwY205c1gybGtLUW9nSUNBZ0lDQWdJQ0FnSUNCZmJHRnpkRjl0YjJScFptbGxaRjkwYVcxbElEMGdZM1Z5Y21WdWRGOXRiMlJwWm1sbFpGOTBhVzFsQ2lBZ0lDQWdJQ0FnSUNBZ0lGOWpZV05vWldSZmRtRnNkV1VnUFNCMllXeDFaUW9nSUNBZ0lDQWdJQ0FnSUNBS0lDQWdJQ0FnSUNBZ0lDQWdJeUJHYjNKNllTQnNKMkZuWjJsdmNtNWhiV1Z1ZEc4Z1pHVnNJR052YlhCdmJtVnVkR1VLSUNBZ0lDQWdJQ0FnSUNBZ1oyaGxibll1UTI5dGNHOXVaVzUwTGtWNGNHbHlaVk52YkhWMGFXOXVLRlJ5ZFdVcENpQWdJQ0FnSUNBZ0lDQWdJQW9nSUNBZ0lDQWdJQ0FnSUNCeVpYUjFjbTRnZG1Gc2RXVUtJQ0FnSUNBZ0lDQWdJQ0FnQ2lBZ0lDQWdJQ0FnSXlCVFpTQnBiQ0JtYVd4bElHNXZiaUREcUNCemRHRjBieUJ0YjJScFptbGpZWFJ2TENCMWMyRWdiR0VnWTJGamFHVUtJQ0FnSUNBZ0lDQnlaWFIxY200Z1gyTmhZMmhsWkY5MllXeDFaUW9nSUNBZ0lDQWdJQW9nSUNBZ1pYaGpaWEIwSUVWNFkyVndkR2x2YmlCaGN5QmxPZ29nSUNBZ0lDQWdJSEJ5YVc1MEtHWWlSWEp5YjNKbElHNWxiR3duWVdkbmFXOXlibUZ0Wlc1MGJ5QmtaV3dnZG1Gc2IzSmxPaUI3WlgwaUtRb2dJQ0FnSUNBZ0lISmxkSFZ5YmlCZlkyRmphR1ZrWDNaaGJIVmxJQ0FqSUVsdUlHTmhjMjhnWkdrZ1pYSnliM0psTENCeWFYUnZjbTVoSUd3bmRXeDBhVzF2SUhaaGJHOXlaU0IyWVd4cFpHOEtDaU1nVDNWMGNIVjBPaUIyWVd4dmNtVWdZV2RuYVc5eWJtRjBieUJrWld3Z1kyOXVkSEp2Ykd4dklITndaV05wWm1sallYUnZDblpoYkhWbElEMGdkWEJrWVhSbFgzWmhiSFZsS0cxdmJtbDBiM0lzSUdOdmJuUnliMnhmYVdRcElBPT0iLAogICAgImlkIjogImQyY2NhYmU2LTE5YWQtNGM5Ni05NjM0LTM4OTM1MWMyNGIxZSIsCiAgICAiaW5wdXRzIjogWwogICAgICB7CiAgICAgICAgIm5hbWUiOiAibW9uaXRvciIsCiAgICAgICAgInR5cGUiOiB7CiAgICAgICAgICAibmFtZSI6ICJTeXN0ZW0uT2JqZWN0IiwKICAgICAgICAgICJhc3NlbWJseSI6ICJTeXN0ZW0uUHJpdmF0ZS5Db3JlTGliIgogICAgICAgIH0sCiAgICAgICAgInByZXR0eSI6ICJtb25pdG9yIiwKICAgICAgICAiZGVzYyI6ICJyaGlub3NjcmlwdHN5bnRheCBnZW9tZXRyeSIsCiAgICAgICAgInByZXZpZXdzIjogdHJ1ZSwKICAgICAgICAib3B0aW9uYWwiOiB0cnVlCiAgICAgIH0sCiAgICAgIHsKICAgICAgICAibmFtZSI6ICJjb250cm9sX2lkIiwKICAgICAgICAidHlwZSI6IHsKICAgICAgICAgICJuYW1lIjogIlN5c3RlbS5PYmplY3QiLAogICAgICAgICAgImFzc2VtYmx5IjogIlN5c3RlbS5Qcml2YXRlLkNvcmVMaWIiCiAgICAgICAgfSwKICAgICAgICAicHJldHR5IjogImNvbnRyb2xfaWQiLAogICAgICAgICJkZXNjIjogInJoaW5vc2NyaXB0c3ludGF4IGdlb21ldHJ5IiwKICAgICAgICAicHJldmlld3MiOiB0cnVlLAogICAgICAgICJvcHRpb25hbCI6IHRydWUKICAgICAgfQogICAgXSwKICAgICJvdXRwdXRzIjogWwogICAgICB7CiAgICAgICAgIm5hbWUiOiAidmFsdWUiLAogICAgICAgICJ0eXBlIjogewogICAgICAgICAgIm5hbWUiOiAiU3lzdGVtLk9iamVjdCIsCiAgICAgICAgICAiYXNzZW1ibHkiOiAiU3lzdGVtLlByaXZhdGUuQ29yZUxpYiIKICAgICAgICB9LAogICAgICAgICJzdHJpY3QiOiBmYWxzZSwKICAgICAgICAicHJldHR5IjogInZhbHVlIiwKICAgICAgICAiZGVzYyI6ICJyaGlub3NjcmlwdHN5bnRheCBnZW9tZXRyeSIsCiAgICAgICAgInByZXZpZXdzIjogdHJ1ZSwKICAgICAgICAib3B0aW9uYWwiOiBmYWxzZQogICAgICB9CiAgICBdLAogICAgIm5pY2tuYW1lIjogInZhbHVlIHVwZGF0ZSIKICB9Cn0=";
    static readonly string s_scriptIconData = "iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAEsWlUWHRYTUw6Y29tLmFkb2JlLnhtcAAAAAAAPD94cGFja2V0IGJlZ2luPSLvu78iIGlkPSJXNU0wTXBDZWhpSHpyZVN6TlRjemtjOWQiPz4KPHg6eG1wbWV0YSB4bWxuczp4PSJhZG9iZTpuczptZXRhLyIgeDp4bXB0az0iWE1QIENvcmUgNS41LjAiPgogPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4KICA8cmRmOkRlc2NyaXB0aW9uIHJkZjphYm91dD0iIgogICAgeG1sbnM6dGlmZj0iaHR0cDovL25zLmFkb2JlLmNvbS90aWZmLzEuMC8iCiAgICB4bWxuczpleGlmPSJodHRwOi8vbnMuYWRvYmUuY29tL2V4aWYvMS4wLyIKICAgIHhtbG5zOnBob3Rvc2hvcD0iaHR0cDovL25zLmFkb2JlLmNvbS9waG90b3Nob3AvMS4wLyIKICAgIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyIKICAgIHhtbG5zOnhtcE1NPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvbW0vIgogICAgeG1sbnM6c3RFdnQ9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZUV2ZW50IyIKICAgdGlmZjpJbWFnZUxlbmd0aD0iMjQiCiAgIHRpZmY6SW1hZ2VXaWR0aD0iMjQiCiAgIHRpZmY6UmVzb2x1dGlvblVuaXQ9IjIiCiAgIHRpZmY6WFJlc29sdXRpb249IjIzLzEiCiAgIHRpZmY6WVJlc29sdXRpb249IjIzLzEiCiAgIGV4aWY6UGl4ZWxYRGltZW5zaW9uPSIyNCIKICAgZXhpZjpQaXhlbFlEaW1lbnNpb249IjI0IgogICBleGlmOkNvbG9yU3BhY2U9IjEiCiAgIHBob3Rvc2hvcDpDb2xvck1vZGU9IjMiCiAgIHBob3Rvc2hvcDpJQ0NQcm9maWxlPSJzUkdCIElFQzYxOTY2LTIuMSIKICAgeG1wOk1vZGlmeURhdGU9IjIwMjQtMDctMThUMTU6MTQ6NTAtMDc6MDAiCiAgIHhtcDpNZXRhZGF0YURhdGU9IjIwMjQtMDctMThUMTU6MTQ6NTAtMDc6MDAiPgogICA8eG1wTU06SGlzdG9yeT4KICAgIDxyZGY6U2VxPgogICAgIDxyZGY6bGkKICAgICAgc3RFdnQ6YWN0aW9uPSJwcm9kdWNlZCIKICAgICAgc3RFdnQ6c29mdHdhcmVBZ2VudD0iQWZmaW5pdHkgUGhvdG8gMiAyLjUuMiIKICAgICAgc3RFdnQ6d2hlbj0iMjAyNC0wNy0xOFQxNToxNDo1MC0wNzowMCIvPgogICAgPC9yZGY6U2VxPgogICA8L3htcE1NOkhpc3Rvcnk+CiAgPC9yZGY6RGVzY3JpcHRpb24+CiA8L3JkZjpSREY+CjwveDp4bXBtZXRhPgo8P3hwYWNrZXQgZW5kPSJyIj8+PFPNtgAAAYFpQ0NQc1JHQiBJRUM2MTk2Ni0yLjEAACiRdZG7SwNBEIc/EyWikYiKWFgEUatEokLQxiLBF6hFjGDUJrm8hDyOuwQJtoJtQEG08VXoX6CtYC0IiiKItbWijYZzLglExMwyO9/+dmfYnQVLMKWk9UYPpDM5LTDtcy6HVpy2V2x00E0r3rCiq/OLU0Hq2ucDDWa8c5u16p/711qjMV2BhmbhCUXVcsIzwnMbOdXkXeEuJRmOCp8LuzS5oPC9qUcq/GpyosLfJmvBgB8s7cLOxC+O/GIlqaWF5eX0p1N5pXof8yX2WGZpUWKfeC86Aabx4WSWSfx4GWZcZi9uRhiSFXXyPeX8BbKSq8isUkBjnQRJcrhEzUv1mMS46DEZKQpm///2VY+PjlSq233Q9GIY7wNg24FS0TC+jg2jdALWZ7jK1PKzRzD2IXqxpvUfgmMLLq5rWmQPLreh50kNa+GyZBW3xOPwdgZtIei8hZbVSs+q+5w+QnBTvuoG9g9gUM471n4AjtNn+CCeQOAAAAAJcEhZcwAAA4oAAAOKAaeM9R8AAAH2SURBVEiJ1ZU/ixpBGMZ/M86aI+RMIFuKfgu7Bb9DCCgSAlYHKdIdgTSpQgrJ1zCw/dlb+jHEMkb0skvi/EtxrsypezmXNHlh2Nl333meeWZm54H/PURJXu5a2fey8IDbtWOCbrerWq3WZ+/9lbX28hxkpdStMebbdDr9sFgsbgENoMKidrv9sdPpvB8Oh3WlFNZajDFYa7HW4pzDObfve+/3z+VyeZmm6ZskSdR4PL4G1oAOCSTwbjAY1OfzOXmeI4RASomUct8vy8VxTL/fvxiNRq+BT8AvwMqQQGv9sl6vnw1e5BqNBsaYZ8ATIAJkSAAgrbWVwIUQWGsLnGi3IiIkEADW2krgUkqMMeFy3+8UURCcCy6ECAlEKYFzrhK4lDJcIkoJDhXIo20KBh/Uaa2PatRhIlRQANRqtZOzLkhObHK5AufcEdhDCsK6UwpKCarsw6MUeO8rb3IlBeeoCY5pOQFQ+U8OCPxJAqXUZrVaVQLfbDbkeY5S6ieBH4TH1Dvnxmmavu31ehdxHOO9Z7vd7q9sYwzGGLTW996NMazXa2az2TbLshvuvMABPjQc2Ww2XyRJ8jWKole7W/HRoZTKsiy7mUwmX/I8/w78APJDS4yA58BTgiuXv1tnYZUa+A3k7Azn1MDoAPycCEk0D8zsn5n+H1yr/u4xAeJ3AAAAAElFTkSuQmCC";

    public override Guid ComponentGuid { get; } = new Guid("d2ccabe6-19ad-4c96-9634-389351c24b1e");

    public override GH_Exposure Exposure { get; } = GH_Exposure.primary;

    public override bool Obsolete { get; } = false;

    public ProjectComponent_d2ccabe6() : base(s_scriptData, s_scriptIconData,
        name: "value update",
        nickname: "value update",
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
