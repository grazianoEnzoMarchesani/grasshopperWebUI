using System;
using SD = System.Drawing;
using SWF = System.Windows.Forms;

using Rhino.Geometry;

using Grasshopper.Kernel;

namespace RhinoCodePlatform.Rhino3D.Projects.Plugin.GH
{
  public sealed class ProjectComponent_73facf70 : ProjectComponent_Base
  {
    static readonly string s_scriptData = "ewogICJ0eXBlIjogInNjcmlwdCIsCiAgInNjcmlwdCI6IHsKICAgICJsYW5ndWFnZSI6IHsKICAgICAgImlkIjogIiouKi5weXRob24iLAogICAgICAidmVyc2lvbiI6ICIzLiouKiIKICAgIH0sCiAgICAidGl0bGUiOiAiaHRtbCBidWlsZGVyIiwKICAgICJ0ZXh0IjogIklpSWlRMjl0Y0c5dVpXNTBaU0J3WlhJZ1kyOXpkSEoxYVhKbElHd25TRlJOVENCa1pXeHNZU0J3WVdkcGJtRWdkMlZpSWlJaUNnb2pJRWx1Y0hWME9nb2pJQ0FnWTI5dWRISnZiSE5mYUhSdGJEb2diR2x6ZEdFZ1pHa2djM1J5YVc1bmFHVWdTRlJOVENCd1pYSWdhU0JqYjI1MGNtOXNiR2tLSXlBZ0lHbHVZMngxWkdWZk0yUmZkbWxsZDJWeU9pQmliMjlzWldGdWJ5QndaWElnYVc1amJIVmtaWEpsSUdsc0lIWnBjM1ZoYkdsNmVtRjBiM0psSURORUNncGtaV1lnWW5WcGJHUmZjR0ZuWlY5b2RHMXNLR052Ym5SeWIyeHpYMmgwYld3OVcxMHNJR2x1WTJ4MVpHVmZNMlJmZG1sbGQyVnlQVlJ5ZFdVcE9nb2dJQ0FnSXlCRmMzUnlZV2tnZEhWMGRHa2daMnhwSUhOMGFXeHBJRU5UVXlCa1lXa2dZMjl1ZEhKdmJHeHBDaUFnSUNCamMzTmZZbXh2WTJ0eklEMGdXMTBLSUNBZ0lHaDBiV3hmWW14dlkydHpJRDBnVzEwS0lDQWdJR3B6WDJKc2IyTnJjeUE5SUZ0ZENpQWdJQ0FLSUNBZ0lHWnZjaUJqYjI1MGNtOXNJR2x1SUdOdmJuUnliMnh6WDJoMGJXdzZDaUFnSUNBZ0lDQWdJeUJGYzNSeVlXa2dhV3dnUTFOVElDaDBkWFIwYnlCamFjT3lJR05vWlNERHFDQjBjbUVnUEhOMGVXeGxQaUJsSUR3dmMzUjViR1VcdTAwMkJLUW9nSUNBZ0lDQWdJR056YzE5emRHRnlkQ0E5SUdOdmJuUnliMnd1Wm1sdVpDZ25QSE4wZVd4bFBpY3BDaUFnSUNBZ0lDQWdZM056WDJWdVpDQTlJR052Ym5SeWIyd3VabWx1WkNnblBDOXpkSGxzWlQ0bktRb2dJQ0FnSUNBZ0lHbG1JR056YzE5emRHRnlkQ0FoUFNBdE1TQmhibVFnWTNOelgyVnVaQ0FoUFNBdE1Ub0tJQ0FnSUNBZ0lDQWdJQ0FnWTNOeklEMGdZMjl1ZEhKdmJGdGpjM05mYzNSaGNuUWdLeUEzT21OemMxOWxibVJkTG5OMGNtbHdLQ2tLSUNBZ0lDQWdJQ0FnSUNBZ2FXWWdZM056SUc1dmRDQnBiaUJqYzNOZllteHZZMnR6T2lBZ0l5QkZkbWwwWVNCa2RYQnNhV05oZEdrS0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUdOemMxOWliRzlqYTNNdVlYQndaVzVrS0dOemN5a0tJQ0FnSUNBZ0lDQUtJQ0FnSUNBZ0lDQWpJRVZ6ZEhKaGFTQnNKMGhVVFV3Z0tIUjFkSFJ2SUdOcHc3SWdZMmhsSU1Pb0lIUnlZU0E4TDNOMGVXeGxQaUJsSUR4elkzSnBjSFFcdTAwMkJLUW9nSUNBZ0lDQWdJR2gwYld4ZmMzUmhjblFnUFNCamIyNTBjbTlzTG1acGJtUW9Kend2YzNSNWJHVVx1MDAyQkp5a2dLeUE0Q2lBZ0lDQWdJQ0FnYUhSdGJGOWxibVFnUFNCamIyNTBjbTlzTG1acGJtUW9Kenh6WTNKcGNIUVx1MDAyQkp5a0tJQ0FnSUNBZ0lDQnBaaUJvZEcxc1gzTjBZWEowSUNFOUlDMHhJR0Z1WkNCb2RHMXNYMlZ1WkNBaFBTQXRNVG9LSUNBZ0lDQWdJQ0FnSUNBZ2FIUnRiQ0E5SUdOdmJuUnliMnhiYUhSdGJGOXpkR0Z5ZERwb2RHMXNYMlZ1WkYwdWMzUnlhWEFvS1FvZ0lDQWdJQ0FnSUNBZ0lDQm9kRzFzWDJKc2IyTnJjeTVoY0hCbGJtUW9hSFJ0YkNrS0lDQWdJQ0FnSUNBS0lDQWdJQ0FnSUNBaklFVnpkSEpoYVNCcGJDQktZWFpoVTJOeWFYQjBJQ2gwZFhSMGJ5QmphY095SUdOb1pTRERxQ0IwY21FZ1BITmpjbWx3ZEQ0Z1pTQThMM05qY21sd2RENHBDaUFnSUNBZ0lDQWdhbk5mYzNSaGNuUWdQU0JqYjI1MGNtOXNMbVpwYm1Rb0p6eHpZM0pwY0hRXHUwMDJCSnlrZ0t5QTRDaUFnSUNBZ0lDQWdhbk5mWlc1a0lEMGdZMjl1ZEhKdmJDNW1hVzVrS0NjOEwzTmpjbWx3ZEQ0bktRb2dJQ0FnSUNBZ0lHbG1JR3B6WDNOMFlYSjBJQ0U5SUMweElHRnVaQ0JxYzE5bGJtUWdJVDBnTFRFNkNpQWdJQ0FnSUNBZ0lDQWdJR3B6SUQwZ1kyOXVkSEp2YkZ0cWMxOXpkR0Z5ZERwcWMxOWxibVJkTG5OMGNtbHdLQ2tLSUNBZ0lDQWdJQ0FnSUNBZ2FuTmZZbXh2WTJ0ekxtRndjR1Z1WkNocWN5a0tJQ0FnSUFvZ0lDQWdJeUJEVTFNZ1ltRnpaU0JsSUdSbGFTQmpiMjUwY205c2JHa2dZMjl0WW1sdVlYUnBDaUFnSUNCaVlYTmxYMk56Y3lBOUlDSWlJZ29nSUNBZ0lDQWdJR0p2WkhrZ2V5QUtJQ0FnSUNBZ0lDQWdJQ0FnWm05dWRDMW1ZVzFwYkhrNklFRnlhV0ZzTENCellXNXpMWE5sY21sbU95QUtJQ0FnSUNBZ0lDQWdJQ0FnYldGeVoybHVPaUF3T3dvZ0lDQWdJQ0FnSUNBZ0lDQmthWE53YkdGNU9pQm1iR1Y0T3dvZ0lDQWdJQ0FnSUNBZ0lDQm1iR1Y0TFdScGNtVmpkR2x2YmpvZ1kyOXNkVzF1T3dvZ0lDQWdJQ0FnSUNBZ0lDQm9aV2xuYUhRNklERXdNSFpvT3dvZ0lDQWdJQ0FnSUgwS0lDQWdJQ0FnSUNBalkyOXVkSEp2YkhNZ2V5QUtJQ0FnSUNBZ0lDQWdJQ0FnY0dGa1pHbHVaem9nTWpCd2VEc0tJQ0FnSUNBZ0lDQWdJQ0FnWW1GamEyZHliM1Z1WkMxamIyeHZjam9nSTJZMVpqVm1OVHNLSUNBZ0lDQWdJQ0I5Q2lBZ0lDQWdJQ0FnTG1OdmJuUnliMndnZXlBS0lDQWdJQ0FnSUNBZ0lDQWdiV0Z5WjJsdU9pQXhNSEI0SURBN0NpQWdJQ0FnSUNBZ0lDQWdJSEJoWkdScGJtYzZJREUxY0hnN0NpQWdJQ0FnSUNBZ0lDQWdJR0poWTJ0bmNtOTFibVF0WTI5c2IzSTZJSGRvYVhSbE93b2dJQ0FnSUNBZ0lDQWdJQ0JpYjNKa1pYSXRjbUZrYVhWek9pQTRjSGc3Q2lBZ0lDQWdJQ0FnSUNBZ0lHSnZlQzF6YUdGa2IzYzZJREFnTW5CNElEUndlQ0J5WjJKaEtEQXNNQ3d3TERBdU1TazdDaUFnSUNBZ0lDQWdmUW9nSUNBZ0lDQWdJQ04yYVdWM1pYSWdld29nSUNBZ0lDQWdJQ0FnSUNCbWJHVjRMV2R5YjNjNklERTdDaUFnSUNBZ0lDQWdJQ0FnSUhkcFpIUm9PaUF4TURBbE93b2dJQ0FnSUNBZ0lIMEtJQ0FnSUNJaUlnb2dJQ0FnQ2lBZ0lDQWpJRU52YzNSeWRXbHpZMmtnYVd3Z1ExTlRJR052YldKcGJtRjBid29nSUNBZ1kyOXRZbWx1WldSZlkzTnpJRDBnWmlJaUlnb2dJQ0FnUEhOMGVXeGxQZ29nSUNBZ0lDQWdJSHRpWVhObFgyTnpjMzBLSUNBZ0lDQWdJQ0I3SnlBbkxtcHZhVzRvWTNOelgySnNiMk5yY3lsOUNpQWdJQ0E4TDNOMGVXeGxQZ29nSUNBZ0lpSWlDaUFnSUNBS0lDQWdJQ01nUTI5emRISjFhWE5qYVNCc0owaFVUVXdnWkdWcElHTnZiblJ5YjJ4c2FRb2dJQ0FnWTI5dWRISnZiSE5mYzJWamRHbHZiaUE5SUdZaUlpSUtJQ0FnSUR4a2FYWWdhV1E5SW1OdmJuUnliMnh6SWo0S0lDQWdJQ0FnSUNCN0p5QW5MbXB2YVc0b2FIUnRiRjlpYkc5amEzTXBmUW9nSUNBZ1BDOWthWFlcdTAwMkJDaUFnSUNBaUlpSUtJQ0FnSUFvZ0lDQWdJeUJIWlhOMGFYTmphU0JwYkNCMmFXVjNaWElnTTBRS0lDQWdJSFpwWlhkbGNsOW9kRzFzSUQwZ0lpSWlDaUFnSUNBOFpHbDJJR2xrUFNKMmFXVjNaWElpUGp3dlpHbDJQZ29nSUNBZ1BITmpjbWx3ZENCemNtTTlJbWgwZEhCek9pOHZZMlJ1YW5NdVkyeHZkV1JtYkdGeVpTNWpiMjB2WVdwaGVDOXNhV0p6TDNSb2NtVmxMbXB6TDNJeE1qZ3ZkR2h5WldVdWJXbHVMbXB6SWo0OEwzTmpjbWx3ZEQ0S0lDQWdJRHh6WTNKcGNIUWdjM0pqUFNKb2RIUndjem92TDJOa2JpNXFjMlJsYkdsMmNpNXVaWFF2Ym5CdEwzUm9jbVZsUURBdU1USTRMakF2WlhoaGJYQnNaWE12YW5NdlkyOXVkSEp2YkhNdlQzSmlhWFJEYjI1MGNtOXNjeTVxY3lJXHUwMDJCUEM5elkzSnBjSFFcdTAwMkJDaUFnSUNBaUlpSWdhV1lnYVc1amJIVmtaVjh6WkY5MmFXVjNaWElnWld4elpTQWlJZ29nSUNBZ0NpQWdJQ0FqSUVOdmJXSnBibUVnZEhWMGRHOGdhV3dnU21GMllWTmpjbWx3ZEFvZ0lDQWdZMjl0WW1sdVpXUmZhbk1nUFNCbUlpSWlDaUFnSUNBOGMyTnlhWEIwUGdvZ0lDQWdJQ0FnSUM4dklGWmhjbWxoWW1sc1pTQm5iRzlpWVd4bElIQmxjaUJ0WVc1MFpXNWxjbVVnYkc4Z2MzUmhkRzhnWkdrZ2RIVjBkR2tnYVNCamIyNTBjbTlzYkdrS0lDQWdJQ0FnSUNCc1pYUWdaMnh2WW1Gc1EyOXVkSEp2YkhOVGRHRjBaU0E5SUh0N2ZYMDdDZ29nSUNBZ0lDQWdJQzh2SUVaMWJucHBiMjVsSUhCbGNpQnBibWw2YVdGc2FYcDZZWEpsSUdrZ2RtRnNiM0pwSUdSbGFTQmpiMjUwY205c2JHa0tJQ0FnSUNBZ0lDQmhjM2x1WXlCbWRXNWpkR2x2YmlCcGJtbDBhV0ZzYVhwbFEyOXVkSEp2YkhNb0tTQjdld29nSUNBZ0lDQWdJQ0FnSUNCMGNua2dlM3NLSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdJR052Ym5OMElISmxjM0J2Ym5ObElEMGdZWGRoYVhRZ1ptVjBZMmdvSnk5blpYUmZhVzVwZEdsaGJGOTJZV3gxWlhNbktUc0tJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lHTnZibk4wSUdSaGRHRWdQU0JoZDJGcGRDQnlaWE53YjI1elpTNXFjMjl1S0NrN0NpQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNBS0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUdsbUlDaGtZWFJoTG5OMFlYUjFjeUE5UFQwZ0ozTjFZMk5sYzNNbktTQjdld29nSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUdOdmJuTjBJSFpoYkhWbGN5QTlJR1JoZEdFdWRtRnNkV1Z6T3dvZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lDOHZJRUZuWjJsdmNtNWhJR3h2SUhOMFlYUnZJR2RzYjJKaGJHVUtJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0JuYkc5aVlXeERiMjUwY205c2MxTjBZWFJsSUQwZ2RtRnNkV1Z6T3dvZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lBb2dJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdJQzh2SUVsMFpYSmhJSE4xSUhSMWRIUnBJR2tnWTI5dWRISnZiR3hwQ2lBZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ1QySnFaV04wTG10bGVYTW9kbUZzZFdWektTNW1iM0pGWVdOb0tHdGxlU0E5UGlCN2V3b2dJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNCamIyNXpkQ0IyWVd4MVpTQTlJSFpoYkhWbGMxdHJaWGxkT3dvZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0JqYjI1emRDQmpiMjUwY205c1NXUWdQU0JyWlhrdWNtVndiR0ZqWlNnblgzWmhiSFZsSnl3Z0p5Y3BPd29nSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lDQmpiMjV6ZENCbGJHVnRaVzUwSUQwZ1pHOWpkVzFsYm5RdVoyVjBSV3hsYldWdWRFSjVTV1FvWTI5dWRISnZiRWxrS1RzS0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnWTI5dWMzUWdaR2x6Y0d4aGVVVnNaVzFsYm5RZ1BTQmtiMk4xYldWdWRDNW5aWFJGYkdWdFpXNTBRbmxKWkNoamIyNTBjbTlzU1dRZ0t5QW5MWFpoYkhWbEp5azdDaUFnSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lBb2dJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNCcFppQW9aV3hsYldWdWRDa2dlM3NLSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUM4dklFbHRjRzl6ZEdFZ2FXd2dkbUZzYjNKbElHbHVJR0poYzJVZ1lXd2dkR2x3YnlCa2FTQmpiMjUwY205c2JHOEtJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdJR2xtSUNobGJHVnRaVzUwTG5SNWNHVWdQVDA5SUNkeVlXNW5aU2NnZkh3Z1pXeGxiV1Z1ZEM1MGVYQmxJRDA5UFNBbmJuVnRZbVZ5SnlrZ2Uzc0tJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNCbGJHVnRaVzUwTG5aaGJIVmxJRDBnZG1Gc2RXVTdDaUFnSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0x5OGdRV2RuYVc5eWJtRWdhV3dnWkdsemNHeGhlU0JrWld3Z2RtRnNiM0psQ2lBZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdhV1lnS0dScGMzQnNZWGxGYkdWdFpXNTBLU0I3ZXdvZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNCa2FYTndiR0Y1Uld4bGJXVnVkQzUwWlhoMFEyOXVkR1Z1ZENBOUlIWmhiSFZsT3dvZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdJSDE5Q2lBZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNCOWZTQmxiSE5sSUdsbUlDaGxiR1Z0Wlc1MExuUmhaMDVoYldVZ1BUMDlJQ2RUUlV4RlExUW5LU0I3ZXdvZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdJR1ZzWlcxbGJuUXVkbUZzZFdVZ1BTQjJZV3gxWlRzS0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lIMTlJR1ZzYzJVZ2FXWWdLR1ZzWlcxbGJuUXVkSGx3WlNBOVBUMGdKMk5vWldOclltOTRKeWtnZTNzS0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0JsYkdWdFpXNTBMbU5vWldOclpXUWdQU0IyWVd4MVpTQTlQVDBnZEhKMVpTQjhmQ0IyWVd4MVpTQTlQVDBnSjNSeWRXVW5Pd29nSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnZlgwS0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnZlgwS0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lDQjlmU2s3Q2dvZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lDOHZJRTV2ZEdsbWFXTmhJR05vWlNCcElHTnZiblJ5YjJ4c2FTQnpiMjV2SUhOMFlYUnBJR2x1YVhwcFlXeHBlbnBoZEdrS0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lDQmtiMk4xYldWdWRDNWthWE53WVhSamFFVjJaVzUwS0c1bGR5QkRkWE4wYjIxRmRtVnVkQ2duWTI5dWRISnZiSE5KYm1sMGFXRnNhWHBsWkNjcEtUc0tJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lIMTlDaUFnSUNBZ0lDQWdJQ0FnSUgxOUlHTmhkR05vSUNobGNuSnZjaWtnZTNzS0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUdOdmJuTnZiR1V1WlhKeWIzSW9KMFZ5Y205eVpTQnVaV3dnWTJGeWFXTmhiV1Z1ZEc4Z1pHVnBJSFpoYkc5eWFTQnBibWw2YVdGc2FUb25MQ0JsY25KdmNpazdDaUFnSUNBZ0lDQWdJQ0FnSUgxOUNpQWdJQ0FnSUNBZ2ZYMEtDaUFnSUNBZ0lDQWdMeThnUm5WdWVtbHZibVVnWVdkbmFXOXlibUYwWVNCd1pYSWdiQ2QxY0dSaGRHVWdaR1ZzSUhObGNuWmxjZ29nSUNBZ0lDQWdJR1oxYm1OMGFXOXVJSFZ3WkdGMFpWTmxjblpsY2loa1lYUmhLU0I3ZXdvZ0lDQWdJQ0FnSUNBZ0lDQXZMeUJCWjJkcGIzSnVZU0JzYnlCemRHRjBieUJuYkc5aVlXeGxDaUFnSUNBZ0lDQWdJQ0FnSUU5aWFtVmpkQzVoYzNOcFoyNG9aMnh2WW1Gc1EyOXVkSEp2YkhOVGRHRjBaU3dnWkdGMFlTNWpiMjUwY205c2N5azdDaUFnSUNBZ0lDQWdJQ0FnSUFvZ0lDQWdJQ0FnSUNBZ0lDQXZMeUJKYm5acFlTQnNieUJ6ZEdGMGJ5QmpiMjF3YkdWMGJ5QmhiQ0J6WlhKMlpYSUtJQ0FnSUNBZ0lDQWdJQ0FnWm1WMFkyZ29KeTkxY0dSaGRHVW5MQ0I3ZXdvZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnYldWMGFHOWtPaUFuVUU5VFZDY3NDaUFnSUNBZ0lDQWdJQ0FnSUNBZ0lDQm9aV0ZrWlhKek9pQjdld29nSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNkRGIyNTBaVzUwTFZSNWNHVW5PaUFuWVhCd2JHbGpZWFJwYjI0dmFuTnZiaWNzQ2lBZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0I5ZlN3S0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUdKdlpIazZJRXBUVDA0dWMzUnlhVzVuYVdaNUtIdDdDaUFnSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnWTI5dWRISnZiSE02SUdkc2IySmhiRU52Ym5SeWIyeHpVM1JoZEdVS0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUgxOUtRb2dJQ0FnSUNBZ0lDQWdJQ0I5ZlNrN0NpQWdJQ0FnSUNBZ2ZYMEtDaUFnSUNBZ0lDQWdMeThnU1c1cGVtbGhiR2w2ZW1FZ2FTQmpiMjUwY205c2JHa2dZV3dnWTJGeWFXTmhiV1Z1ZEc4Z1pHVnNiR0VnY0dGbmFXNWhDaUFnSUNBZ0lDQWdaRzlqZFcxbGJuUXVZV1JrUlhabGJuUk1hWE4wWlc1bGNpZ25SRTlOUTI5dWRHVnVkRXh2WVdSbFpDY3NJR2x1YVhScFlXeHBlbVZEYjI1MGNtOXNjeWs3Q2lBZ0lDQWdJQ0FnQ2lBZ0lDQWdJQ0FnTHk4Z1EyOWthV05sSUdSbGFTQmpiMjUwY205c2JHa0tJQ0FnSUNBZ0lDQjdKeUFuTG1wdmFXNG9hbk5mWW14dlkydHpLWDBLSUNBZ0lEd3ZjMk55YVhCMFBnb2dJQ0FnSWlJaUNpQWdJQ0FLSUNBZ0lDTWdRMjl6ZEhKMWFYTmphU0JzWVNCd1lXZHBibUVnWTI5dGNHeGxkR0VLSUNBZ0lHaDBiV3dnUFNCbUlpSWlDaUFnSUNBOElVUlBRMVJaVUVVZ2FIUnRiRDRLSUNBZ0lEeG9kRzFzUGdvZ0lDQWdQR2hsWVdRXHUwMDJCQ2lBZ0lDQWdJQ0FnUEhScGRHeGxQa2R5WVhOemFHOXdjR1Z5SUVsdWRHVnlabUZqWlR3dmRHbDBiR1VcdTAwMkJDaUFnSUNBZ0lDQWdlMk52YldKcGJtVmtYMk56YzMwS0lDQWdJRHd2YUdWaFpENEtJQ0FnSUR4aWIyUjVQZ29nSUNBZ0lDQWdJSHRqYjI1MGNtOXNjMTl6WldOMGFXOXVmUW9nSUNBZ0lDQWdJSHQyYVdWM1pYSmZhSFJ0YkgwS0lDQWdJQ0FnSUNCN1kyOXRZbWx1WldSZmFuTjlDaUFnSUNBOEwySnZaSGtcdTAwMkJDaUFnSUNBOEwyaDBiV3dcdTAwMkJDaUFnSUNBaUlpSUtJQ0FnSUFvZ0lDQWdjbVYwZFhKdUlHaDBiV3dLQ2lNZ1QzVjBjSFYwT2lCSVZFMU1JR052YlhCc1pYUnZJR1JsYkd4aElIQmhaMmx1WVFwd1lXZGxYMmgwYld3Z1BTQmlkV2xzWkY5d1lXZGxYMmgwYld3b1kyOXVkSEp2YkhOZmFIUnRiQ3dnYVc1amJIVmtaVjh6WkY5MmFXVjNaWElwSUE9PSIsCiAgICAiaWQiOiAiNzNmYWNmNzAtYjJhMS00NDI3LThlMDEtZjVlYjMxYzc5ZGM2IiwKICAgICJpbnB1dHMiOiBbCiAgICAgIHsKICAgICAgICAibmFtZSI6ICJjb250cm9sc19odG1sIiwKICAgICAgICAidHlwZSI6IHsKICAgICAgICAgICJuYW1lIjogIlN5c3RlbS5PYmplY3QiLAogICAgICAgICAgImFzc2VtYmx5IjogIlN5c3RlbS5Qcml2YXRlLkNvcmVMaWIiCiAgICAgICAgfSwKICAgICAgICAicHJldHR5IjogImNvbnRyb2xzX2h0bWwiLAogICAgICAgICJkZXNjIjogInJoaW5vc2NyaXB0c3ludGF4IGdlb21ldHJ5IiwKICAgICAgICAiYWNjZXNzIjogImxpc3QiLAogICAgICAgICJwcmV2aWV3cyI6IHRydWUsCiAgICAgICAgIm9wdGlvbmFsIjogdHJ1ZQogICAgICB9LAogICAgICB7CiAgICAgICAgIm5hbWUiOiAiaW5jbHVkZV8zZF92aWV3ZXIiLAogICAgICAgICJ0eXBlIjogewogICAgICAgICAgIm5hbWUiOiAiU3lzdGVtLkJvb2xlYW4iLAogICAgICAgICAgImFzc2VtYmx5IjogIlN5c3RlbS5Qcml2YXRlLkNvcmVMaWIiCiAgICAgICAgfSwKICAgICAgICAicHJldHR5IjogImluY2x1ZGVfM2Rfdmlld2VyIiwKICAgICAgICAiZGVzYyI6ICJDb252ZXJ0cyB0byBjb2xsZWN0aW9uIG9mIGJvb2xlYW4gdmFsdWVzIiwKICAgICAgICAicHJldmlld3MiOiB0cnVlLAogICAgICAgICJvcHRpb25hbCI6IHRydWUKICAgICAgfQogICAgXSwKICAgICJvdXRwdXRzIjogWwogICAgICB7CiAgICAgICAgIm5hbWUiOiAicGFnZV9odG1sIiwKICAgICAgICAidHlwZSI6IHsKICAgICAgICAgICJuYW1lIjogIlN5c3RlbS5PYmplY3QiLAogICAgICAgICAgImFzc2VtYmx5IjogIlN5c3RlbS5Qcml2YXRlLkNvcmVMaWIiCiAgICAgICAgfSwKICAgICAgICAic3RyaWN0IjogZmFsc2UsCiAgICAgICAgInByZXR0eSI6ICJwYWdlX2h0bWwiLAogICAgICAgICJkZXNjIjogInJoaW5vc2NyaXB0c3ludGF4IGdlb21ldHJ5IiwKICAgICAgICAicHJldmlld3MiOiB0cnVlLAogICAgICAgICJvcHRpb25hbCI6IGZhbHNlCiAgICAgIH0KICAgIF0sCiAgICAibmlja25hbWUiOiAiaHRtbCBidWlsZGVyIgogIH0KfQ==";
    static readonly string s_scriptIconData = "iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAEsWlUWHRYTUw6Y29tLmFkb2JlLnhtcAAAAAAAPD94cGFja2V0IGJlZ2luPSLvu78iIGlkPSJXNU0wTXBDZWhpSHpyZVN6TlRjemtjOWQiPz4KPHg6eG1wbWV0YSB4bWxuczp4PSJhZG9iZTpuczptZXRhLyIgeDp4bXB0az0iWE1QIENvcmUgNS41LjAiPgogPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4KICA8cmRmOkRlc2NyaXB0aW9uIHJkZjphYm91dD0iIgogICAgeG1sbnM6dGlmZj0iaHR0cDovL25zLmFkb2JlLmNvbS90aWZmLzEuMC8iCiAgICB4bWxuczpleGlmPSJodHRwOi8vbnMuYWRvYmUuY29tL2V4aWYvMS4wLyIKICAgIHhtbG5zOnBob3Rvc2hvcD0iaHR0cDovL25zLmFkb2JlLmNvbS9waG90b3Nob3AvMS4wLyIKICAgIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyIKICAgIHhtbG5zOnhtcE1NPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvbW0vIgogICAgeG1sbnM6c3RFdnQ9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZUV2ZW50IyIKICAgdGlmZjpJbWFnZUxlbmd0aD0iMjQiCiAgIHRpZmY6SW1hZ2VXaWR0aD0iMjQiCiAgIHRpZmY6UmVzb2x1dGlvblVuaXQ9IjIiCiAgIHRpZmY6WFJlc29sdXRpb249IjIzLzEiCiAgIHRpZmY6WVJlc29sdXRpb249IjIzLzEiCiAgIGV4aWY6UGl4ZWxYRGltZW5zaW9uPSIyNCIKICAgZXhpZjpQaXhlbFlEaW1lbnNpb249IjI0IgogICBleGlmOkNvbG9yU3BhY2U9IjEiCiAgIHBob3Rvc2hvcDpDb2xvck1vZGU9IjMiCiAgIHBob3Rvc2hvcDpJQ0NQcm9maWxlPSJzUkdCIElFQzYxOTY2LTIuMSIKICAgeG1wOk1vZGlmeURhdGU9IjIwMjQtMDctMThUMTU6MTQ6NTAtMDc6MDAiCiAgIHhtcDpNZXRhZGF0YURhdGU9IjIwMjQtMDctMThUMTU6MTQ6NTAtMDc6MDAiPgogICA8eG1wTU06SGlzdG9yeT4KICAgIDxyZGY6U2VxPgogICAgIDxyZGY6bGkKICAgICAgc3RFdnQ6YWN0aW9uPSJwcm9kdWNlZCIKICAgICAgc3RFdnQ6c29mdHdhcmVBZ2VudD0iQWZmaW5pdHkgUGhvdG8gMiAyLjUuMiIKICAgICAgc3RFdnQ6d2hlbj0iMjAyNC0wNy0xOFQxNToxNDo1MC0wNzowMCIvPgogICAgPC9yZGY6U2VxPgogICA8L3htcE1NOkhpc3Rvcnk+CiAgPC9yZGY6RGVzY3JpcHRpb24+CiA8L3JkZjpSREY+CjwveDp4bXBtZXRhPgo8P3hwYWNrZXQgZW5kPSJyIj8+PFPNtgAAAYFpQ0NQc1JHQiBJRUM2MTk2Ni0yLjEAACiRdZG7SwNBEIc/EyWikYiKWFgEUatEokLQxiLBF6hFjGDUJrm8hDyOuwQJtoJtQEG08VXoX6CtYC0IiiKItbWijYZzLglExMwyO9/+dmfYnQVLMKWk9UYPpDM5LTDtcy6HVpy2V2x00E0r3rCiq/OLU0Hq2ucDDWa8c5u16p/711qjMV2BhmbhCUXVcsIzwnMbOdXkXeEuJRmOCp8LuzS5oPC9qUcq/GpyosLfJmvBgB8s7cLOxC+O/GIlqaWF5eX0p1N5pXof8yX2WGZpUWKfeC86Aabx4WSWSfx4GWZcZi9uRhiSFXXyPeX8BbKSq8isUkBjnQRJcrhEzUv1mMS46DEZKQpm///2VY+PjlSq233Q9GIY7wNg24FS0TC+jg2jdALWZ7jK1PKzRzD2IXqxpvUfgmMLLq5rWmQPLreh50kNa+GyZBW3xOPwdgZtIei8hZbVSs+q+5w+QnBTvuoG9g9gUM471n4AjtNn+CCeQOAAAAAJcEhZcwAAA4oAAAOKAaeM9R8AAAH2SURBVEiJ1ZU/ixpBGMZ/M86aI+RMIFuKfgu7Bb9DCCgSAlYHKdIdgTSpQgrJ1zCw/dlb+jHEMkb0skvi/EtxrsypezmXNHlh2Nl333meeWZm54H/PURJXu5a2fey8IDbtWOCbrerWq3WZ+/9lbX28hxkpdStMebbdDr9sFgsbgENoMKidrv9sdPpvB8Oh3WlFNZajDFYa7HW4pzDObfve+/3z+VyeZmm6ZskSdR4PL4G1oAOCSTwbjAY1OfzOXmeI4RASomUct8vy8VxTL/fvxiNRq+BT8AvwMqQQGv9sl6vnw1e5BqNBsaYZ8ATIAJkSAAgrbWVwIUQWGsLnGi3IiIkEADW2krgUkqMMeFy3+8UURCcCy6ECAlEKYFzrhK4lDJcIkoJDhXIo20KBh/Uaa2PatRhIlRQANRqtZOzLkhObHK5AufcEdhDCsK6UwpKCarsw6MUeO8rb3IlBeeoCY5pOQFQ+U8OCPxJAqXUZrVaVQLfbDbkeY5S6ieBH4TH1Dvnxmmavu31ehdxHOO9Z7vd7q9sYwzGGLTW996NMazXa2az2TbLshvuvMABPjQc2Ww2XyRJ8jWKole7W/HRoZTKsiy7mUwmX/I8/w78APJDS4yA58BTgiuXv1tnYZUa+A3k7Azn1MDoAPycCEk0D8zsn5n+H1yr/u4xAeJ3AAAAAElFTkSuQmCC";

    public override Guid ComponentGuid { get; } = new Guid("73facf70-b2a1-4427-8e01-f5eb31c79dc6");

    public override GH_Exposure Exposure { get; } = GH_Exposure.primary;

    public override bool Obsolete { get; } = false;

    public ProjectComponent_73facf70() : base(s_scriptData, s_scriptIconData,
        name: "html builder",
        nickname: "html builder",
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