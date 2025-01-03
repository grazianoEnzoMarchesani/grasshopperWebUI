using System;
using SD = System.Drawing;
using SWF = System.Windows.Forms;

using Rhino.Geometry;

using Grasshopper.Kernel;

namespace RhinoCodePlatform.Rhino3D.Projects.Plugin.GH
{
  public sealed class ProjectComponent_b3ce532f : ProjectComponent_Base
  {
    static readonly string s_scriptData = "ewogICJ0eXBlIjogInNjcmlwdCIsCiAgInNjcmlwdCI6IHsKICAgICJsYW5ndWFnZSI6IHsKICAgICAgImlkIjogIiouKi5weXRob24iLAogICAgICAidmVyc2lvbiI6ICIzLiouKiIKICAgIH0sCiAgICAidGl0bGUiOiAicmFuZ2Ugc2xpZGVyIGNvbnRyb2wiLAogICAgInRleHQiOiAiSWlJaVEyOXRjRzl1Wlc1MFpTQndaWElnWjJWdVpYSmhjbVVnZFc1dklITnNhV1JsY2lCamIyNGdaSFZsSUcxaGJtbG5iR2xsSUhCbGNpQnlZVzVuWlNJaUlnb0taR1ZtSUdOeVpXRjBaVjl5WVc1blpWOXpiR2xrWlhJb2JHRmlaV3dzSUcxcGJsOTJZV3c5TUN3Z2JXRjRYM1poYkQweE1EQXNJR1JsWm1GMWJIUmZiV2x1UFRJMUxDQmtaV1poZFd4MFgyMWhlRDAzTlN3Z2MzUmxjRDB4TENCamIyNTBjbTlzWDJsa1BTSnlZVzVuWlRFaUxDQmpkWE4wYjIxZlkzTnpQU0lpS1RvS0lDQWdJQ01nUTFOVElHUnBJR1JsWm1GMWJIUUtJQ0FnSUdSbFptRjFiSFJmWTNOeklEMGdJaUlpQ2lBZ0lDQWdJQ0FnTG5KaGJtZGxMV052Ym5SaGFXNWxjaUI3Q2lBZ0lDQWdJQ0FnSUNBZ0lHMWhjbWRwYmpvZ01UQndlQ0F3T3dvZ0lDQWdJQ0FnSUgwS0lDQWdJQ0FnSUNBdWNtRnVaMlV0WTI5dWRHRnBibVZ5SUd4aFltVnNJSHNLSUNBZ0lDQWdJQ0FnSUNBZ1pHbHpjR3hoZVRvZ1lteHZZMnM3Q2lBZ0lDQWdJQ0FnSUNBZ0lHMWhjbWRwYmkxaWIzUjBiMjA2SURWd2VEc0tJQ0FnSUNBZ0lDQjlDaUFnSUNBZ0lDQWdMbkpoYm1kbExYTnNhV1JsY2lCN0NpQWdJQ0FnSUNBZ0lDQWdJSEJ2YzJsMGFXOXVPaUJ5Wld4aGRHbDJaVHNLSUNBZ0lDQWdJQ0FnSUNBZ2QybGtkR2c2SURFd01DVTdDaUFnSUNBZ0lDQWdJQ0FnSUdobGFXZG9kRG9nTXpCd2VEc0tJQ0FnSUNBZ0lDQjlDaUFnSUNBZ0lDQWdMbkpoYm1kbExYTnNhV1JsY2lCcGJuQjFkRnQwZVhCbFBTSnlZVzVuWlNKZElIc0tJQ0FnSUNBZ0lDQWdJQ0FnY0c5emFYUnBiMjQ2SUdGaWMyOXNkWFJsT3dvZ0lDQWdJQ0FnSUNBZ0lDQjNhV1IwYURvZ01UQXdKVHNLSUNBZ0lDQWdJQ0FnSUNBZ2NHOXBiblJsY2kxbGRtVnVkSE02SUc1dmJtVTdDaUFnSUNBZ0lDQWdJQ0FnSUMxM1pXSnJhWFF0WVhCd1pXRnlZVzVqWlRvZ2JtOXVaVHNLSUNBZ0lDQWdJQ0FnSUNBZ2VpMXBibVJsZURvZ01qc0tJQ0FnSUNBZ0lDQWdJQ0FnYUdWcFoyaDBPaUEwY0hnN0NpQWdJQ0FnSUNBZ0lDQWdJRzFoY21kcGJqb2dNRHNLSUNBZ0lDQWdJQ0FnSUNBZ1ltRmphMmR5YjNWdVpEb2dibTl1WlRzS0lDQWdJQ0FnSUNBZ0lDQWdkRzl3T2lBMU1DVTdDaUFnSUNBZ0lDQWdJQ0FnSUhSeVlXNXpabTl5YlRvZ2RISmhibk5zWVhSbFdTZ3ROVEFsS1RzS0lDQWdJQ0FnSUNCOUNpQWdJQ0FnSUNBZ0xuSmhibWRsTFhOc2FXUmxjaUJwYm5CMWRGdDBlWEJsUFNKeVlXNW5aU0pkT2pvdGQyVmlhMmwwTFhOc2FXUmxjaTEwYUhWdFlpQjdDaUFnSUNBZ0lDQWdJQ0FnSUhCdmFXNTBaWEl0WlhabGJuUnpPaUJoYkd3N0NpQWdJQ0FnSUNBZ0lDQWdJSGRwWkhSb09pQXhNbkI0T3dvZ0lDQWdJQ0FnSUNBZ0lDQm9aV2xuYUhRNklERXljSGc3Q2lBZ0lDQWdJQ0FnSUNBZ0lHSnZjbVJsY2kxeVlXUnBkWE02SURKd2VEc0tJQ0FnSUNBZ0lDQWdJQ0FnTFhkbFltdHBkQzFoY0hCbFlYSmhibU5sT2lCdWIyNWxPd29nSUNBZ0lDQWdJQ0FnSUNCaVlXTnJaM0p2ZFc1a09pQWpOalkyT3dvZ0lDQWdJQ0FnSUNBZ0lDQmpkWEp6YjNJNklIQnZhVzUwWlhJN0NpQWdJQ0FnSUNBZ0lDQWdJR0p2Y21SbGNqb2dibTl1WlRzS0lDQWdJQ0FnSUNCOUNpQWdJQ0FnSUNBZ0xuSmhibWRsTFhOc2FXUmxjaUJwYm5CMWRGdDBlWEJsUFNKeVlXNW5aU0pkT2pvdGJXOTZMWEpoYm1kbExYUm9kVzFpSUhzS0lDQWdJQ0FnSUNBZ0lDQWdjRzlwYm5SbGNpMWxkbVZ1ZEhNNklHRnNiRHNLSUNBZ0lDQWdJQ0FnSUNBZ2QybGtkR2c2SURFeWNIZzdDaUFnSUNBZ0lDQWdJQ0FnSUdobGFXZG9kRG9nTVRKd2VEc0tJQ0FnSUNBZ0lDQWdJQ0FnWW05eVpHVnlMWEpoWkdsMWN6b2dNbkI0T3dvZ0lDQWdJQ0FnSUNBZ0lDQmlZV05yWjNKdmRXNWtPaUFqTmpZMk93b2dJQ0FnSUNBZ0lDQWdJQ0JqZFhKemIzSTZJSEJ2YVc1MFpYSTdDaUFnSUNBZ0lDQWdJQ0FnSUdKdmNtUmxjam9nYm05dVpUc0tJQ0FnSUNBZ0lDQjlDaUFnSUNBZ0lDQWdMbkpoYm1kbExYUnlZV05ySUhzS0lDQWdJQ0FnSUNBZ0lDQWdjRzl6YVhScGIyNDZJR0ZpYzI5c2RYUmxPd29nSUNBZ0lDQWdJQ0FnSUNCM2FXUjBhRG9nTVRBd0pUc0tJQ0FnSUNBZ0lDQWdJQ0FnYUdWcFoyaDBPaUEwY0hnN0NpQWdJQ0FnSUNBZ0lDQWdJR0poWTJ0bmNtOTFibVE2SUNOa1pHUTdDaUFnSUNBZ0lDQWdJQ0FnSUhvdGFXNWtaWGc2SURFN0NpQWdJQ0FnSUNBZ0lDQWdJSFJ2Y0RvZ05UQWxPd29nSUNBZ0lDQWdJQ0FnSUNCMGNtRnVjMlp2Y20wNklIUnlZVzV6YkdGMFpWa29MVFV3SlNrN0NpQWdJQ0FnSUNBZ2ZRb2dJQ0FnSWlJaUNpQWdJQ0FLSUNBZ0lHTnpjMTlqYjI1MFpXNTBJRDBnWkdWbVlYVnNkRjlqYzNNS0lDQWdJR2xtSUdOMWMzUnZiVjlqYzNNZ1lXNWtJR04xYzNSdmJWOWpjM011YzNSeWFYQW9LVG9LSUNBZ0lDQWdJQ0JqYzNOZlkyOXVkR1Z1ZENBOUlHTjFjM1J2YlY5amMzTXVjM1J5YVhBb0tRb2dJQ0FnQ2lBZ0lDQm9kRzFzSUQwZ1ppSWlJZ29nSUNBZ1BITjBlV3hsUGdvZ0lDQWdJQ0FnSUh0amMzTmZZMjl1ZEdWdWRIMEtJQ0FnSUR3dmMzUjViR1VcdTAwMkJDaUFnSUNBOFpHbDJJR05zWVhOelBTSmpiMjUwY205c0lISmhibWRsTFdOdmJuUmhhVzVsY2lJZ2FXUTlJbnRqYjI1MGNtOXNYMmxrZlMxamIyNTBZV2x1WlhJaVBnb2dJQ0FnSUNBZ0lEeHNZV0psYkQ1N2JHRmlaV3g5T2lBS0lDQWdJQ0FnSUNBZ0lDQWdQSE53WVc0Z2FXUTlJbnRqYjI1MGNtOXNYMmxrZlMxdGFXNHRkbUZzZFdVaVBudGtaV1poZFd4MFgyMXBibjA4TDNOd1lXNFx1MDAyQklDMGdDaUFnSUNBZ0lDQWdJQ0FnSUR4emNHRnVJR2xrUFNKN1kyOXVkSEp2YkY5cFpIMHRiV0Y0TFhaaGJIVmxJajU3WkdWbVlYVnNkRjl0WVhoOVBDOXpjR0Z1UGdvZ0lDQWdJQ0FnSUR3dmJHRmlaV3dcdTAwMkJDaUFnSUNBZ0lDQWdQR1JwZGlCamJHRnpjejBpY21GdVoyVXRjMnhwWkdWeUlqNEtJQ0FnSUNBZ0lDQWdJQ0FnUEdScGRpQmpiR0Z6Y3owaWNtRnVaMlV0ZEhKaFkyc2lQand2WkdsMlBnb2dJQ0FnSUNBZ0lDQWdJQ0E4YVc1d2RYUWdkSGx3WlQwaWNtRnVaMlVpSUFvZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ2FXUTlJbnRqYjI1MGNtOXNYMmxrZlMxdGFXNGlJQW9nSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnYldsdVBTSjdiV2x1WDNaaGJIMGlJQW9nSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnYldGNFBTSjdiV0Y0WDNaaGJIMGlJQW9nSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnZG1Gc2RXVTlJbnRrWldaaGRXeDBYMjFwYm4waUNpQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lDQnpkR1Z3UFNKN2MzUmxjSDBpQ2lBZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNCdmJtbHVjSFYwUFNKMWNHUmhkR1ZTWVc1blpWWmhiSFZsWDN0amIyNTBjbTlzWDJsa2ZTZ3BJajRLSUNBZ0lDQWdJQ0FnSUNBZ1BHbHVjSFYwSUhSNWNHVTlJbkpoYm1kbElpQUtJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdJR2xrUFNKN1kyOXVkSEp2YkY5cFpIMHRiV0Y0SWlBS0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lHMXBiajBpZTIxcGJsOTJZV3g5SWlBS0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lHMWhlRDBpZTIxaGVGOTJZV3g5SWlBS0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lIWmhiSFZsUFNKN1pHVm1ZWFZzZEY5dFlYaDlJZ29nSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnYzNSbGNEMGllM04wWlhCOUlnb2dJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdiMjVwYm5CMWREMGlkWEJrWVhSbFVtRnVaMlZXWVd4MVpWOTdZMjl1ZEhKdmJGOXBaSDBvS1NJXHUwMDJCQ2lBZ0lDQWdJQ0FnUEM5a2FYWVx1MDAyQkNpQWdJQ0E4TDJScGRqNEtJQ0FnSUR4elkzSnBjSFFcdTAwMkJDaUFnSUNBZ0lDQWdablZ1WTNScGIyNGdabTl5YldGMFRuVnRZbVZ5WDN0amIyNTBjbTlzWDJsa2ZTaDJZV3gxWlNrZ2Uzc0tJQ0FnSUNBZ0lDQWdJQ0FnTHk4Z1JHVjBaWEp0YVc1aElHbHNJRzUxYldWeWJ5QmthU0JrWldOcGJXRnNhU0JpWVhOaGRHOGdjM1ZzYkc4Z2MzUmxjQW9nSUNBZ0lDQWdJQ0FnSUNCamIyNXpkQ0J6ZEdWd0lEMGdlM04wWlhCOU93b2dJQ0FnSUNBZ0lDQWdJQ0JwWmlBb1RuVnRZbVZ5TG1selNXNTBaV2RsY2loemRHVndLU2tnZTNzS0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUhKbGRIVnliaUJOWVhSb0xuSnZkVzVrS0haaGJIVmxLUzUwYjFOMGNtbHVaeWdwT3dvZ0lDQWdJQ0FnSUNBZ0lDQjlmU0JsYkhObElIdDdDaUFnSUNBZ0lDQWdJQ0FnSUNBZ0lDQmpiMjV6ZENCa1pXTnBiV0ZzY3lBOUlITjBaWEF1ZEc5VGRISnBibWNvS1M1emNHeHBkQ2duTGljcFd6RmRQeTVzWlc1bmRHZ2dmSHdnTURzS0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUhKbGRIVnliaUIyWVd4MVpTNTBiMFpwZUdWa0tHUmxZMmx0WVd4ektUc0tJQ0FnSUNBZ0lDQWdJQ0FnZlgwS0lDQWdJQ0FnSUNCOWZRb0tJQ0FnSUNBZ0lDQm1kVzVqZEdsdmJpQjFjR1JoZEdWU1lXNW5aVlpoYkhWbFgzdGpiMjUwY205c1gybGtmU2dwSUh0N0NpQWdJQ0FnSUNBZ0lDQWdJR052Ym5OMElHMXBibE5zYVdSbGNpQTlJR1J2WTNWdFpXNTBMbWRsZEVWc1pXMWxiblJDZVVsa0tDZDdZMjl1ZEhKdmJGOXBaSDB0YldsdUp5azdDaUFnSUNBZ0lDQWdJQ0FnSUdOdmJuTjBJRzFoZUZOc2FXUmxjaUE5SUdSdlkzVnRaVzUwTG1kbGRFVnNaVzFsYm5SQ2VVbGtLQ2Q3WTI5dWRISnZiRjlwWkgwdGJXRjRKeWs3Q2lBZ0lDQWdJQ0FnSUNBZ0lHTnZibk4wSUcxcGJrUnBjM0JzWVhrZ1BTQmtiMk4xYldWdWRDNW5aWFJGYkdWdFpXNTBRbmxKWkNnbmUyTnZiblJ5YjJ4ZmFXUjlMVzFwYmkxMllXeDFaU2NwT3dvZ0lDQWdJQ0FnSUNBZ0lDQmpiMjV6ZENCdFlYaEVhWE53YkdGNUlEMGdaRzlqZFcxbGJuUXVaMlYwUld4bGJXVnVkRUo1U1dRb0ozdGpiMjUwY205c1gybGtmUzF0WVhndGRtRnNkV1VuS1RzS0lDQWdJQ0FnSUNBZ0lDQWdDaUFnSUNBZ0lDQWdJQ0FnSUd4bGRDQnRhVzVXWVd3Z1BTQndZWEp6WlVac2IyRjBLRzFwYmxOc2FXUmxjaTUyWVd4MVpTazdDaUFnSUNBZ0lDQWdJQ0FnSUd4bGRDQnRZWGhXWVd3Z1BTQndZWEp6WlVac2IyRjBLRzFoZUZOc2FXUmxjaTUyWVd4MVpTazdDaUFnSUNBZ0lDQWdJQ0FnSUFvZ0lDQWdJQ0FnSUNBZ0lDQXZMeUJCYzNOcFkzVnlZWFJwSUdOb1pTQnRhVzRnYm05dUlITjFjR1Z5YVNCdFlYZ0tJQ0FnSUNBZ0lDQWdJQ0FnYVdZZ0tHMXBibFpoYkNBXHUwMDJCSUcxaGVGWmhiQ2tnZTNzS0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUdsbUlDaDBhR2x6SUQwOVBTQnRhVzVUYkdsa1pYSXBJSHQ3Q2lBZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ2JXbHVWbUZzSUQwZ2JXRjRWbUZzT3dvZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lHMXBibE5zYVdSbGNpNTJZV3gxWlNBOUlHMXBibFpoYkRzS0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUgxOUlHVnNjMlVnZTNzS0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lDQnRZWGhXWVd3Z1BTQnRhVzVXWVd3N0NpQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdiV0Y0VTJ4cFpHVnlMblpoYkhWbElEMGdiV0Y0Vm1Gc093b2dJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ2ZYMEtJQ0FnSUNBZ0lDQWdJQ0FnZlgwS0lDQWdJQ0FnSUNBZ0lDQWdDaUFnSUNBZ0lDQWdJQ0FnSUM4dklFWnZjbTFoZEhSaElHa2dkbUZzYjNKcElHbHVaR2wyYVdSMVlXeHBJSEJsY2lCcGJDQmthWE53YkdGNUNpQWdJQ0FnSUNBZ0lDQWdJR052Ym5OMElHWnZjbTFoZEhSbFpFMXBiaUE5SUdadmNtMWhkRTUxYldKbGNsOTdZMjl1ZEhKdmJGOXBaSDBvYldsdVZtRnNLVHNLSUNBZ0lDQWdJQ0FnSUNBZ1kyOXVjM1FnWm05eWJXRjBkR1ZrVFdGNElEMGdabTl5YldGMFRuVnRZbVZ5WDN0amIyNTBjbTlzWDJsa2ZTaHRZWGhXWVd3cE93b2dJQ0FnSUNBZ0lDQWdJQ0FLSUNBZ0lDQWdJQ0FnSUNBZ2JXbHVSR2x6Y0d4aGVTNTBaWGgwUTI5dWRHVnVkQ0E5SUdadmNtMWhkSFJsWkUxcGJqc0tJQ0FnSUNBZ0lDQWdJQ0FnYldGNFJHbHpjR3hoZVM1MFpYaDBRMjl1ZEdWdWRDQTlJR1p2Y20xaGRIUmxaRTFoZURzS0lDQWdJQ0FnSUNBZ0lDQWdDaUFnSUNBZ0lDQWdJQ0FnSUM4dklGTmhiSFpoSUdrZ2RtRnNiM0pwSUc1bGJDQnNiMk5oYkZOMGIzSmhaMlVLSUNBZ0lDQWdJQ0FnSUNBZ2JHOWpZV3hUZEc5eVlXZGxMbk5sZEVsMFpXMG9KM3RqYjI1MGNtOXNYMmxrZlY5dGFXNG5MQ0J0YVc1V1lXd3BPd29nSUNBZ0lDQWdJQ0FnSUNCc2IyTmhiRk4wYjNKaFoyVXVjMlYwU1hSbGJTZ25lMk52Ym5SeWIyeGZhV1I5WDIxaGVDY3NJRzFoZUZaaGJDazdDaUFnSUNBZ0lDQWdJQ0FnSUFvZ0lDQWdJQ0FnSUNBZ0lDQXZMeUJEY21WaElHeGhJSE4wY21sdVoyRWdZMjl0WW1sdVlYUmhJRzVsYkNCbWIzSnRZWFJ2SUhKcFkyaHBaWE4wYndvZ0lDQWdJQ0FnSUNBZ0lDQmpiMjV6ZENCamIyMWlhVzVsWkZaaGJIVmxJRDBnWUNSN2UyWnZjbTFoZEhSbFpFMXBibjE5SUZSdklDUjdlMlp2Y20xaGRIUmxaRTFoZUgxOVlEc0tJQ0FnSUNBZ0lDQWdJQ0FnQ2lBZ0lDQWdJQ0FnSUNBZ0lHTnZibk4wSUdSaGRHRWdQU0I3ZXdvZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnWTI5dWRISnZiSE02SUh0N0NpQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdKM3RqYjI1MGNtOXNYMmxrZlY5MllXeDFaU2M2SUdOdmJXSnBibVZrVm1Gc2RXVUtJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lIMTlDaUFnSUNBZ0lDQWdJQ0FnSUgxOU93b2dJQ0FnSUNBZ0lDQWdJQ0IxY0dSaGRHVlRaWEoyWlhJb1pHRjBZU2s3Q2lBZ0lDQWdJQ0FnZlgwS0lDQWdJQ0FnSUNBS0lDQWdJQ0FnSUNCa2IyTjFiV1Z1ZEM1aFpHUkZkbVZ1ZEV4cGMzUmxibVZ5S0NkamIyNTBjbTlzYzBsdWFYUnBZV3hwZW1Wa0p5d2dablZ1WTNScGIyNG9LU0I3ZXdvZ0lDQWdJQ0FnSUNBZ0lDQmpiMjV6ZENCdGFXNVRiR2xrWlhJZ1BTQmtiMk4xYldWdWRDNW5aWFJGYkdWdFpXNTBRbmxKWkNnbmUyTnZiblJ5YjJ4ZmFXUjlMVzFwYmljcE93b2dJQ0FnSUNBZ0lDQWdJQ0JqYjI1emRDQnRZWGhUYkdsa1pYSWdQU0JrYjJOMWJXVnVkQzVuWlhSRmJHVnRaVzUwUW5sSlpDZ25lMk52Ym5SeWIyeGZhV1I5TFcxaGVDY3BPd29nSUNBZ0lDQWdJQ0FnSUNCcFppQW9iV2x1VTJ4cFpHVnlJQ1ltSUcxaGVGTnNhV1JsY2lrZ2Uzc0tJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lDOHZJRkpsWTNWd1pYSmhJR2tnZG1Gc2IzSnBJSE5oYkhaaGRHa0tJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lHTnZibk4wSUhOaGRtVmtUV2x1SUQwZ2JHOWpZV3hUZEc5eVlXZGxMbWRsZEVsMFpXMG9KM3RqYjI1MGNtOXNYMmxrZlY5dGFXNG5LVHNLSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdJR052Ym5OMElITmhkbVZrVFdGNElEMGdiRzlqWVd4VGRHOXlZV2RsTG1kbGRFbDBaVzBvSjN0amIyNTBjbTlzWDJsa2ZWOXRZWGduS1RzS0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUdsbUlDaHpZWFpsWkUxcGJpQWhQVDBnYm5Wc2JDa2dlM3NLSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNCdGFXNVRiR2xrWlhJdWRtRnNkV1VnUFNCellYWmxaRTFwYmpzS0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUgxOUNpQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNCcFppQW9jMkYyWldSTllYZ2dJVDA5SUc1MWJHd3BJSHQ3Q2lBZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ2JXRjRVMnhwWkdWeUxuWmhiSFZsSUQwZ2MyRjJaV1JOWVhnN0NpQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNCOWZRb2dJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ2RYQmtZWFJsVW1GdVoyVldZV3gxWlY5N1kyOXVkSEp2YkY5cFpIMG9LVHNLSUNBZ0lDQWdJQ0FnSUNBZ2ZYMEtJQ0FnSUNBZ0lDQjlmU2s3Q2lBZ0lDQThMM05qY21sd2RENEtJQ0FnSUNJaUlnb2dJQ0FnY21WMGRYSnVJR2gwYld3S0NpTWdUM1YwY0hWME9pQklWRTFNSUdSbGJHeHZJSE5zYVdSbGNpQnlZVzVuWlFweVlXNW5aVjlvZEcxc0lEMGdZM0psWVhSbFgzSmhibWRsWDNOc2FXUmxjaWhzWVdKbGJDd2diV2x1WDNaaGJDd2diV0Y0WDNaaGJDd2daR1ZtWVhWc2RGOXRhVzRzSUdSbFptRjFiSFJmYldGNExDQnpkR1Z3TENCamIyNTBjbTlzWDJsa0xDQmpkWE4wYjIxZlkzTnpLU0E9IiwKICAgICJpZCI6ICJiM2NlNTMyZi0yNTY5LTRhNGEtODcxNi0wYzU4ZTUyZDNjMTgiLAogICAgImlucHV0cyI6IFsKICAgICAgewogICAgICAgICJuYW1lIjogImxhYmVsIiwKICAgICAgICAidHlwZSI6IHsKICAgICAgICAgICJuYW1lIjogIlN5c3RlbS5TdHJpbmciLAogICAgICAgICAgImFzc2VtYmx5IjogIlN5c3RlbS5Qcml2YXRlLkNvcmVMaWIiCiAgICAgICAgfSwKICAgICAgICAicHJldHR5IjogImxhYmVsIiwKICAgICAgICAiZGVzYyI6ICJDb252ZXJ0cyB0byBjb2xsZWN0aW9uIG9mIHRleHQgZnJhZ21lbnRzIiwKICAgICAgICAicHJldmlld3MiOiB0cnVlLAogICAgICAgICJvcHRpb25hbCI6IHRydWUKICAgICAgfSwKICAgICAgewogICAgICAgICJuYW1lIjogInN0ZXAiLAogICAgICAgICJ0eXBlIjogewogICAgICAgICAgIm5hbWUiOiAiU3lzdGVtLk9iamVjdCIsCiAgICAgICAgICAiYXNzZW1ibHkiOiAiU3lzdGVtLlByaXZhdGUuQ29yZUxpYiIKICAgICAgICB9LAogICAgICAgICJwcmV0dHkiOiAic3RlcCIsCiAgICAgICAgImRlc2MiOiAicmhpbm9zY3JpcHRzeW50YXggZ2VvbWV0cnkiLAogICAgICAgICJwcmV2aWV3cyI6IHRydWUsCiAgICAgICAgIm9wdGlvbmFsIjogdHJ1ZQogICAgICB9LAogICAgICB7CiAgICAgICAgIm5hbWUiOiAibWluX3ZhbCIsCiAgICAgICAgInR5cGUiOiB7CiAgICAgICAgICAibmFtZSI6ICJTeXN0ZW0uT2JqZWN0IiwKICAgICAgICAgICJhc3NlbWJseSI6ICJTeXN0ZW0uUHJpdmF0ZS5Db3JlTGliIgogICAgICAgIH0sCiAgICAgICAgInByZXR0eSI6ICJtaW5fdmFsIiwKICAgICAgICAiZGVzYyI6ICJyaGlub3NjcmlwdHN5bnRheCBnZW9tZXRyeSIsCiAgICAgICAgInByZXZpZXdzIjogdHJ1ZSwKICAgICAgICAib3B0aW9uYWwiOiB0cnVlCiAgICAgIH0sCiAgICAgIHsKICAgICAgICAibmFtZSI6ICJtYXhfdmFsIiwKICAgICAgICAidHlwZSI6IHsKICAgICAgICAgICJuYW1lIjogIlN5c3RlbS5PYmplY3QiLAogICAgICAgICAgImFzc2VtYmx5IjogIlN5c3RlbS5Qcml2YXRlLkNvcmVMaWIiCiAgICAgICAgfSwKICAgICAgICAicHJldHR5IjogIm1heF92YWwiLAogICAgICAgICJkZXNjIjogInJoaW5vc2NyaXB0c3ludGF4IGdlb21ldHJ5IiwKICAgICAgICAicHJldmlld3MiOiB0cnVlLAogICAgICAgICJvcHRpb25hbCI6IHRydWUKICAgICAgfSwKICAgICAgewogICAgICAgICJuYW1lIjogImRlZmF1bHRfbWluIiwKICAgICAgICAidHlwZSI6IHsKICAgICAgICAgICJuYW1lIjogIlN5c3RlbS5PYmplY3QiLAogICAgICAgICAgImFzc2VtYmx5IjogIlN5c3RlbS5Qcml2YXRlLkNvcmVMaWIiCiAgICAgICAgfSwKICAgICAgICAicHJldHR5IjogImRlZmF1bHRfbWluIiwKICAgICAgICAiZGVzYyI6ICJyaGlub3NjcmlwdHN5bnRheCBnZW9tZXRyeSIsCiAgICAgICAgInByZXZpZXdzIjogdHJ1ZSwKICAgICAgICAib3B0aW9uYWwiOiB0cnVlCiAgICAgIH0sCiAgICAgIHsKICAgICAgICAibmFtZSI6ICJkZWZhdWx0X21heCIsCiAgICAgICAgInR5cGUiOiB7CiAgICAgICAgICAibmFtZSI6ICJTeXN0ZW0uT2JqZWN0IiwKICAgICAgICAgICJhc3NlbWJseSI6ICJTeXN0ZW0uUHJpdmF0ZS5Db3JlTGliIgogICAgICAgIH0sCiAgICAgICAgInByZXR0eSI6ICJkZWZhdWx0X21heCIsCiAgICAgICAgImRlc2MiOiAicmhpbm9zY3JpcHRzeW50YXggZ2VvbWV0cnkiLAogICAgICAgICJwcmV2aWV3cyI6IHRydWUsCiAgICAgICAgIm9wdGlvbmFsIjogdHJ1ZQogICAgICB9LAogICAgICB7CiAgICAgICAgIm5hbWUiOiAiY29udHJvbF9pZCIsCiAgICAgICAgInR5cGUiOiB7CiAgICAgICAgICAibmFtZSI6ICJTeXN0ZW0uT2JqZWN0IiwKICAgICAgICAgICJhc3NlbWJseSI6ICJTeXN0ZW0uUHJpdmF0ZS5Db3JlTGliIgogICAgICAgIH0sCiAgICAgICAgInByZXR0eSI6ICJjb250cm9sX2lkIiwKICAgICAgICAiZGVzYyI6ICJyaGlub3NjcmlwdHN5bnRheCBnZW9tZXRyeSIsCiAgICAgICAgInByZXZpZXdzIjogdHJ1ZSwKICAgICAgICAib3B0aW9uYWwiOiB0cnVlCiAgICAgIH0sCiAgICAgIHsKICAgICAgICAibmFtZSI6ICJjdXN0b21fY3NzIiwKICAgICAgICAidHlwZSI6IHsKICAgICAgICAgICJuYW1lIjogIlN5c3RlbS5PYmplY3QiLAogICAgICAgICAgImFzc2VtYmx5IjogIlN5c3RlbS5Qcml2YXRlLkNvcmVMaWIiCiAgICAgICAgfSwKICAgICAgICAicHJldHR5IjogImN1c3RvbV9jc3MiLAogICAgICAgICJkZXNjIjogInJoaW5vc2NyaXB0c3ludGF4IGdlb21ldHJ5IiwKICAgICAgICAicHJldmlld3MiOiB0cnVlLAogICAgICAgICJvcHRpb25hbCI6IHRydWUKICAgICAgfQogICAgXSwKICAgICJvdXRwdXRzIjogWwogICAgICB7CiAgICAgICAgIm5hbWUiOiAicmFuZ2VfaHRtbCIsCiAgICAgICAgInR5cGUiOiB7CiAgICAgICAgICAibmFtZSI6ICJTeXN0ZW0uT2JqZWN0IiwKICAgICAgICAgICJhc3NlbWJseSI6ICJTeXN0ZW0uUHJpdmF0ZS5Db3JlTGliIgogICAgICAgIH0sCiAgICAgICAgInN0cmljdCI6IGZhbHNlLAogICAgICAgICJwcmV0dHkiOiAicmFuZ2VfaHRtbCIsCiAgICAgICAgImRlc2MiOiAicmhpbm9zY3JpcHRzeW50YXggZ2VvbWV0cnkiLAogICAgICAgICJwcmV2aWV3cyI6IHRydWUsCiAgICAgICAgIm9wdGlvbmFsIjogZmFsc2UKICAgICAgfQogICAgXSwKICAgICJuaWNrbmFtZSI6ICJyYW5nZSBzbGlkZXIgY29udHJvbCIKICB9Cn0=";
    static readonly string s_scriptIconData = "iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAEsWlUWHRYTUw6Y29tLmFkb2JlLnhtcAAAAAAAPD94cGFja2V0IGJlZ2luPSLvu78iIGlkPSJXNU0wTXBDZWhpSHpyZVN6TlRjemtjOWQiPz4KPHg6eG1wbWV0YSB4bWxuczp4PSJhZG9iZTpuczptZXRhLyIgeDp4bXB0az0iWE1QIENvcmUgNS41LjAiPgogPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4KICA8cmRmOkRlc2NyaXB0aW9uIHJkZjphYm91dD0iIgogICAgeG1sbnM6dGlmZj0iaHR0cDovL25zLmFkb2JlLmNvbS90aWZmLzEuMC8iCiAgICB4bWxuczpleGlmPSJodHRwOi8vbnMuYWRvYmUuY29tL2V4aWYvMS4wLyIKICAgIHhtbG5zOnBob3Rvc2hvcD0iaHR0cDovL25zLmFkb2JlLmNvbS9waG90b3Nob3AvMS4wLyIKICAgIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyIKICAgIHhtbG5zOnhtcE1NPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvbW0vIgogICAgeG1sbnM6c3RFdnQ9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZUV2ZW50IyIKICAgdGlmZjpJbWFnZUxlbmd0aD0iMjQiCiAgIHRpZmY6SW1hZ2VXaWR0aD0iMjQiCiAgIHRpZmY6UmVzb2x1dGlvblVuaXQ9IjIiCiAgIHRpZmY6WFJlc29sdXRpb249IjIzLzEiCiAgIHRpZmY6WVJlc29sdXRpb249IjIzLzEiCiAgIGV4aWY6UGl4ZWxYRGltZW5zaW9uPSIyNCIKICAgZXhpZjpQaXhlbFlEaW1lbnNpb249IjI0IgogICBleGlmOkNvbG9yU3BhY2U9IjEiCiAgIHBob3Rvc2hvcDpDb2xvck1vZGU9IjMiCiAgIHBob3Rvc2hvcDpJQ0NQcm9maWxlPSJzUkdCIElFQzYxOTY2LTIuMSIKICAgeG1wOk1vZGlmeURhdGU9IjIwMjQtMDctMThUMTU6MTQ6NTAtMDc6MDAiCiAgIHhtcDpNZXRhZGF0YURhdGU9IjIwMjQtMDctMThUMTU6MTQ6NTAtMDc6MDAiPgogICA8eG1wTU06SGlzdG9yeT4KICAgIDxyZGY6U2VxPgogICAgIDxyZGY6bGkKICAgICAgc3RFdnQ6YWN0aW9uPSJwcm9kdWNlZCIKICAgICAgc3RFdnQ6c29mdHdhcmVBZ2VudD0iQWZmaW5pdHkgUGhvdG8gMiAyLjUuMiIKICAgICAgc3RFdnQ6d2hlbj0iMjAyNC0wNy0xOFQxNToxNDo1MC0wNzowMCIvPgogICAgPC9yZGY6U2VxPgogICA8L3htcE1NOkhpc3Rvcnk+CiAgPC9yZGY6RGVzY3JpcHRpb24+CiA8L3JkZjpSREY+CjwveDp4bXBtZXRhPgo8P3hwYWNrZXQgZW5kPSJyIj8+PFPNtgAAAYFpQ0NQc1JHQiBJRUM2MTk2Ni0yLjEAACiRdZG7SwNBEIc/EyWikYiKWFgEUatEokLQxiLBF6hFjGDUJrm8hDyOuwQJtoJtQEG08VXoX6CtYC0IiiKItbWijYZzLglExMwyO9/+dmfYnQVLMKWk9UYPpDM5LTDtcy6HVpy2V2x00E0r3rCiq/OLU0Hq2ucDDWa8c5u16p/711qjMV2BhmbhCUXVcsIzwnMbOdXkXeEuJRmOCp8LuzS5oPC9qUcq/GpyosLfJmvBgB8s7cLOxC+O/GIlqaWF5eX0p1N5pXof8yX2WGZpUWKfeC86Aabx4WSWSfx4GWZcZi9uRhiSFXXyPeX8BbKSq8isUkBjnQRJcrhEzUv1mMS46DEZKQpm///2VY+PjlSq233Q9GIY7wNg24FS0TC+jg2jdALWZ7jK1PKzRzD2IXqxpvUfgmMLLq5rWmQPLreh50kNa+GyZBW3xOPwdgZtIei8hZbVSs+q+5w+QnBTvuoG9g9gUM471n4AjtNn+CCeQOAAAAAJcEhZcwAAA4oAAAOKAaeM9R8AAAH2SURBVEiJ1ZU/ixpBGMZ/M86aI+RMIFuKfgu7Bb9DCCgSAlYHKdIdgTSpQgrJ1zCw/dlb+jHEMkb0skvi/EtxrsypezmXNHlh2Nl333meeWZm54H/PURJXu5a2fey8IDbtWOCbrerWq3WZ+/9lbX28hxkpdStMebbdDr9sFgsbgENoMKidrv9sdPpvB8Oh3WlFNZajDFYa7HW4pzDObfve+/3z+VyeZmm6ZskSdR4PL4G1oAOCSTwbjAY1OfzOXmeI4RASomUct8vy8VxTL/fvxiNRq+BT8AvwMqQQGv9sl6vnw1e5BqNBsaYZ8ATIAJkSAAgrbWVwIUQWGsLnGi3IiIkEADW2krgUkqMMeFy3+8UURCcCy6ECAlEKYFzrhK4lDJcIkoJDhXIo20KBh/Uaa2PatRhIlRQANRqtZOzLkhObHK5AufcEdhDCsK6UwpKCarsw6MUeO8rb3IlBeeoCY5pOQFQ+U8OCPxJAqXUZrVaVQLfbDbkeY5S6ieBH4TH1Dvnxmmavu31ehdxHOO9Z7vd7q9sYwzGGLTW996NMazXa2az2TbLshvuvMABPjQc2Ww2XyRJ8jWKole7W/HRoZTKsiy7mUwmX/I8/w78APJDS4yA58BTgiuXv1tnYZUa+A3k7Azn1MDoAPycCEk0D8zsn5n+H1yr/u4xAeJ3AAAAAElFTkSuQmCC";

    public override Guid ComponentGuid { get; } = new Guid("b3ce532f-2569-4a4a-8716-0c58e52d3c18");

    public override GH_Exposure Exposure { get; } = GH_Exposure.primary;

    public override bool Obsolete { get; } = false;

    public ProjectComponent_b3ce532f() : base(s_scriptData, s_scriptIconData,
        name: "range slider control",
        nickname: "range slider control",
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