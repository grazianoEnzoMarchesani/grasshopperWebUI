using System;
using SD = System.Drawing;
using SWF = System.Windows.Forms;

using Rhino.Geometry;

using Grasshopper.Kernel;

namespace RhinoCodePlatform.Rhino3D.Projects.Plugin.GH
{
  public sealed class ProjectComponent_81a58691 : ProjectComponent_Base
  {
    static readonly string s_scriptData = "ewogICJ0eXBlIjogInNjcmlwdCIsCiAgInNjcmlwdCI6IHsKICAgICJsYW5ndWFnZSI6IHsKICAgICAgImlkIjogIiouKi5weXRob24iLAogICAgICAidmVyc2lvbiI6ICIzLiouKiIKICAgIH0sCiAgICAidGl0bGUiOiAic2xpZGVyIGNvbnRyb2wiLAogICAgInRleHQiOiAiSWlJaVEyOXRjRzl1Wlc1MFpTQndaWElnWjJWdVpYSmhjbVVnZFc1dklITnNhV1JsY2lJaUlnb0tJeUJKYm5CMWREb0tJeUFnSUd4aFltVnNPaUJsZEdsamFHVjBkR0VnWkdWc2JHOGdjMnhwWkdWeUlDaHpkSEpwYm1kaEtRb2pJQ0FnYldsdVgzWmhiRG9nZG1Gc2IzSmxJRzFwYm1sdGJ5QW9iblZ0WlhKdktRb2pJQ0FnYldGNFgzWmhiRG9nZG1Gc2IzSmxJRzFoYzNOcGJXOGdLRzUxYldWeWJ5a0tJeUFnSUdSbFptRjFiSFJmZG1Gc09pQjJZV3h2Y21VZ2NISmxaR1ZtYVc1cGRHOGdLRzUxYldWeWJ5a0tJeUFnSUhOMFpYQTZJR2x1WTNKbGJXVnVkRzhnWkdWc2JHOGdjMnhwWkdWeUlDaHVkVzFsY204cENpTWdJQ0JqYjI1MGNtOXNYMmxrT2lCcFpHVnVkR2xtYVdOaGRHOXlaU0IxYm1sMmIyTnZJR1JsYkd4dklITnNhV1JsY2lBb2MzUnlhVzVuWVNrS0l5QWdJR04xYzNSdmJWOWpjM002SUVOVFV5QndaWEp6YjI1aGJHbDZlbUYwYnlCd1pYSWdiRzhnYzJ4cFpHVnlJQ2h6ZEhKcGJtZGhMQ0J2Y0hwcGIyNWhiR1VwQ2dwa1pXWWdZM0psWVhSbFgzTnNhV1JsY2loc1lXSmxiQ3dnYldsdVgzWmhiRDB3TENCdFlYaGZkbUZzUFRFd01Dd2daR1ZtWVhWc2RGOTJZV3c5TlRBc0lITjBaWEE5TVN3Z1kyOXVkSEp2YkY5cFpEMGljMnhwWkdWeU1TSXNJR04xYzNSdmJWOWpjM005SWlJcE9nb2dJQ0FnSXlCRFUxTWdaR2tnWkdWbVlYVnNkQ0J3WlhJZ2JHOGdjMnhwWkdWeUlDaHRhVzVwYlc4c0lITnZiRzhnYkdGNWIzVjBJR0poYzJVcENpQWdJQ0JrWldaaGRXeDBYMk56Y3lBOUlDSWlJZ29nSUNBZ0lDQWdJQzV6Ykdsa1pYSXRZMjl1ZEdGcGJtVnlJSHNLSUNBZ0lDQWdJQ0FnSUNBZ2JXRnlaMmx1T2lBeE1IQjRJREE3Q2lBZ0lDQWdJQ0FnZlFvZ0lDQWdJQ0FnSUM1emJHbGtaWEl0WTI5dWRHRnBibVZ5SUd4aFltVnNJSHNLSUNBZ0lDQWdJQ0FnSUNBZ1pHbHpjR3hoZVRvZ1lteHZZMnM3Q2lBZ0lDQWdJQ0FnSUNBZ0lHMWhjbWRwYmkxaWIzUjBiMjA2SURWd2VEc0tJQ0FnSUNBZ0lDQjlDaUFnSUNBZ0lDQWdMbk5zYVdSbGNpMWpiMjUwWVdsdVpYSWdhVzV3ZFhSYmRIbHdaVDBpY21GdVoyVWlYU0I3Q2lBZ0lDQWdJQ0FnSUNBZ0lIZHBaSFJvT2lBeE1EQWxPd29nSUNBZ0lDQWdJQ0FnSUNBdGQyVmlhMmwwTFdGd2NHVmhjbUZ1WTJVNklHNXZibVU3Q2lBZ0lDQWdJQ0FnSUNBZ0lHaGxhV2RvZERvZ05IQjRPd29nSUNBZ0lDQWdJQ0FnSUNCaVlXTnJaM0p2ZFc1a09pQWpaR1JrT3dvZ0lDQWdJQ0FnSUNBZ0lDQmliM0prWlhJdGNtRmthWFZ6T2lBd093b2dJQ0FnSUNBZ0lDQWdJQ0J0WVhKbmFXNDZJREV3Y0hnZ01Ec0tJQ0FnSUNBZ0lDQjlDaUFnSUNBZ0lDQWdMbk5zYVdSbGNpMWpiMjUwWVdsdVpYSWdhVzV3ZFhSYmRIbHdaVDBpY21GdVoyVWlYVG82TFhkbFltdHBkQzF6Ykdsa1pYSXRkR2gxYldJZ2V3b2dJQ0FnSUNBZ0lDQWdJQ0F0ZDJWaWEybDBMV0Z3Y0dWaGNtRnVZMlU2SUc1dmJtVTdDaUFnSUNBZ0lDQWdJQ0FnSUhkcFpIUm9PaUF4TW5CNE93b2dJQ0FnSUNBZ0lDQWdJQ0JvWldsbmFIUTZJREV5Y0hnN0NpQWdJQ0FnSUNBZ0lDQWdJR0poWTJ0bmNtOTFibVE2SUNNMk5qWTdDaUFnSUNBZ0lDQWdJQ0FnSUdKdmNtUmxjaTF5WVdScGRYTTZJREp3ZURzS0lDQWdJQ0FnSUNBZ0lDQWdZM1Z5YzI5eU9pQndiMmx1ZEdWeU93b2dJQ0FnSUNBZ0lDQWdJQ0JpYjNKa1pYSTZJRzV2Ym1VN0NpQWdJQ0FnSUNBZ2ZRb2dJQ0FnSUNBZ0lDNXpiR2xrWlhJdFkyOXVkR0ZwYm1WeUlHbHVjSFYwVzNSNWNHVTlJbkpoYm1kbElsMDZPaTF0YjNvdGNtRnVaMlV0ZEdoMWJXSWdld29nSUNBZ0lDQWdJQ0FnSUNCM2FXUjBhRG9nTVRKd2VEc0tJQ0FnSUNBZ0lDQWdJQ0FnYUdWcFoyaDBPaUF4TW5CNE93b2dJQ0FnSUNBZ0lDQWdJQ0JpWVdOclozSnZkVzVrT2lBak5qWTJPd29nSUNBZ0lDQWdJQ0FnSUNCaWIzSmtaWEl0Y21Ga2FYVnpPaUF5Y0hnN0NpQWdJQ0FnSUNBZ0lDQWdJR04xY25OdmNqb2djRzlwYm5SbGNqc0tJQ0FnSUNBZ0lDQWdJQ0FnWW05eVpHVnlPaUJ1YjI1bE93b2dJQ0FnSUNBZ0lIMEtJQ0FnSUNBZ0lDQXVjMnhwWkdWeUxXTnZiblJoYVc1bGNpQnBibkIxZEZ0MGVYQmxQU0p5WVc1blpTSmRPbVp2WTNWeklIc0tJQ0FnSUNBZ0lDQWdJQ0FnYjNWMGJHbHVaVG9nYm05dVpUc0tJQ0FnSUNBZ0lDQjlDaUFnSUNBaUlpSUtJQ0FnSUFvZ0lDQWdJeUJIWlhOMGFYTmphU0JwYkNCRFUxTWdjR1Z5YzI5dVlXeHBlbnBoZEc4S0lDQWdJR056YzE5amIyNTBaVzUwSUQwZ1pHVm1ZWFZzZEY5amMzTUtJQ0FnSUdsbUlHTjFjM1J2YlY5amMzTWdZVzVrSUdOMWMzUnZiVjlqYzNNdWMzUnlhWEFvS1RvS0lDQWdJQ0FnSUNCamMzTmZZMjl1ZEdWdWRDQTlJR04xYzNSdmJWOWpjM011YzNSeWFYQW9LUW9nSUNBZ0NpQWdJQ0JvZEcxc0lEMGdaaUlpSWdvZ0lDQWdQSE4wZVd4bFBnb2dJQ0FnSUNBZ0lIdGpjM05mWTI5dWRHVnVkSDBLSUNBZ0lEd3ZjM1I1YkdVXHUwMDJCQ2lBZ0lDQThaR2wySUdOc1lYTnpQU0pqYjI1MGNtOXNJSE5zYVdSbGNpMWpiMjUwWVdsdVpYSWlJR2xrUFNKN1kyOXVkSEp2YkY5cFpIMHRZMjl1ZEdGcGJtVnlJajRLSUNBZ0lDQWdJQ0E4YkdGaVpXd2dabTl5UFNKN1kyOXVkSEp2YkY5cFpIMGlQbnRzWVdKbGJIMDZJRHh6Y0dGdUlHbGtQU0o3WTI5dWRISnZiRjlwWkgwdGRtRnNkV1VpUG50a1pXWmhkV3gwWDNaaGJIMDhMM053WVc0XHUwMDJCUEM5c1lXSmxiRDRLSUNBZ0lDQWdJQ0E4YVc1d2RYUWdkSGx3WlQwaWNtRnVaMlVpSUFvZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0JwWkQwaWUyTnZiblJ5YjJ4ZmFXUjlJaUFLSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdiV2x1UFNKN2JXbHVYM1poYkgwaUlBb2dJQ0FnSUNBZ0lDQWdJQ0FnSUNCdFlYZzlJbnR0WVhoZmRtRnNmU0lnQ2lBZ0lDQWdJQ0FnSUNBZ0lDQWdJSFpoYkhWbFBTSjdaR1ZtWVhWc2RGOTJZV3g5SWdvZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0J6ZEdWd1BTSjdjM1JsY0gwaUNpQWdJQ0FnSUNBZ0lDQWdJQ0FnSUc5dWFXNXdkWFE5SW5Wd1pHRjBaVk5zYVdSbGNsWmhiSFZsWDN0amIyNTBjbTlzWDJsa2ZTaDBhR2x6S1NJXHUwMDJCQ2lBZ0lDQThMMlJwZGo0S0lDQWdJRHh6WTNKcGNIUVx1MDAyQkNpQWdJQ0FnSUNBZ1puVnVZM1JwYjI0Z1ptOXliV0YwVG5WdFltVnlYM3RqYjI1MGNtOXNYMmxrZlNoMllXeDFaU2tnZTNzS0lDQWdJQ0FnSUNBZ0lDQWdMeThnUkdWMFpYSnRhVzVoSUdsc0lHNTFiV1Z5YnlCa2FTQmtaV05wYldGc2FTQmlZWE5oZEc4Z2MzVnNiRzhnYzNSbGNBb2dJQ0FnSUNBZ0lDQWdJQ0JqYjI1emRDQnpkR1Z3SUQwZ2UzTjBaWEI5T3dvZ0lDQWdJQ0FnSUNBZ0lDQnBaaUFvVG5WdFltVnlMbWx6U1c1MFpXZGxjaWh6ZEdWd0tTa2dlM3NLSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdJSEpsZEhWeWJpQk5ZWFJvTG5KdmRXNWtLSFpoYkhWbEtTNTBiMU4wY21sdVp5Z3BPd29nSUNBZ0lDQWdJQ0FnSUNCOWZTQmxiSE5sSUh0N0NpQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNCamIyNXpkQ0JrWldOcGJXRnNjeUE5SUhOMFpYQXVkRzlUZEhKcGJtY29LUzV6Y0d4cGRDZ25MaWNwV3pGZFB5NXNaVzVuZEdnZ2ZId2dNRHNLSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdJSEpsZEhWeWJpQjJZV3gxWlM1MGIwWnBlR1ZrS0dSbFkybHRZV3h6S1RzS0lDQWdJQ0FnSUNBZ0lDQWdmWDBLSUNBZ0lDQWdJQ0I5ZlFvS0lDQWdJQ0FnSUNCbWRXNWpkR2x2YmlCMWNHUmhkR1ZUYkdsa1pYSldZV3gxWlY5N1kyOXVkSEp2YkY5cFpIMG9jMnhwWkdWeUtTQjdld29nSUNBZ0lDQWdJQ0FnSUNCamIyNXpkQ0IyWVd4MVpTQTlJSEJoY25ObFJteHZZWFFvYzJ4cFpHVnlMblpoYkhWbEtUc0tJQ0FnSUNBZ0lDQWdJQ0FnTHk4Z1FXZG5hVzl5Ym1FZ2FXd2daR2x6Y0d4aGVTQmtaV3dnZG1Gc2IzSmxDaUFnSUNBZ0lDQWdJQ0FnSUdOdmJuTjBJR1JwYzNCc1lYbEZiR1Z0Wlc1MElEMGdaRzlqZFcxbGJuUXVaMlYwUld4bGJXVnVkRUo1U1dRb0ozdGpiMjUwY205c1gybGtmUzEyWVd4MVpTY3BPd29nSUNBZ0lDQWdJQ0FnSUNCcFppQW9aR2x6Y0d4aGVVVnNaVzFsYm5RcElIdDdDaUFnSUNBZ0lDQWdJQ0FnSUNBZ0lDQmthWE53YkdGNVJXeGxiV1Z1ZEM1MFpYaDBRMjl1ZEdWdWRDQTlJR1p2Y20xaGRFNTFiV0psY2w5N1kyOXVkSEp2YkY5cFpIMG9kbUZzZFdVcE93b2dJQ0FnSUNBZ0lDQWdJQ0I5ZlFvZ0lDQWdJQ0FnSUNBZ0lDQUtJQ0FnSUNBZ0lDQWdJQ0FnWTI5dWMzUWdaR0YwWVNBOUlIdDdDaUFnSUNBZ0lDQWdJQ0FnSUNBZ0lDQmpiMjUwY205c2N6b2dlM3NLSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNBbmUyTnZiblJ5YjJ4ZmFXUjlYM1poYkhWbEp6b2dkbUZzZFdVS0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUgxOUNpQWdJQ0FnSUNBZ0lDQWdJSDE5T3dvZ0lDQWdJQ0FnSUNBZ0lDQjFjR1JoZEdWVFpYSjJaWElvWkdGMFlTazdDaUFnSUNBZ0lDQWdmWDBLSUNBZ0lDQWdJQ0FLSUNBZ0lDQWdJQ0F2THlCU1pXZHBjM1J5WVNCMWJpQnZZbk5sY25abGNpQndaWElnY1hWbGMzUnZJSE5zYVdSbGNpQnpjR1ZqYVdacFkyOEtJQ0FnSUNBZ0lDQmtiMk4xYldWdWRDNWhaR1JGZG1WdWRFeHBjM1JsYm1WeUtDZEVUMDFEYjI1MFpXNTBURzloWkdWa0p5d2dablZ1WTNScGIyNG9LU0I3ZXdvZ0lDQWdJQ0FnSUNBZ0lDQmpiMjV6ZENCemJHbGtaWElnUFNCa2IyTjFiV1Z1ZEM1blpYUkZiR1Z0Wlc1MFFubEpaQ2duZTJOdmJuUnliMnhmYVdSOUp5azdDaUFnSUNBZ0lDQWdJQ0FnSUdsbUlDaHpiR2xrWlhJcElIdDdDaUFnSUNBZ0lDQWdJQ0FnSUNBZ0lDQXZMeUJCWjJkcGIzSnVZU0JwYkNCa2FYTndiR0Y1SUdSbGJDQjJZV3h2Y21VZ2FXNXBlbWxoYkdVS0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUdOdmJuTjBJR1JwYzNCc1lYbEZiR1Z0Wlc1MElEMGdaRzlqZFcxbGJuUXVaMlYwUld4bGJXVnVkRUo1U1dRb0ozdGpiMjUwY205c1gybGtmUzEyWVd4MVpTY3BPd29nSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdhV1lnS0dScGMzQnNZWGxGYkdWdFpXNTBLU0I3ZXdvZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lHTnZibk4wSUhaaGJIVmxJRDBnY0dGeWMyVkdiRzloZENoemJHbGtaWEl1ZG1Gc2RXVXBPd29nSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUdScGMzQnNZWGxGYkdWdFpXNTBMblJsZUhSRGIyNTBaVzUwSUQwZ1ptOXliV0YwVG5WdFltVnlYM3RqYjI1MGNtOXNYMmxrZlNoMllXeDFaU2s3Q2lBZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0I5ZlFvZ0lDQWdJQ0FnSUNBZ0lDQjlmUW9nSUNBZ0lDQWdJSDE5S1RzS0NpQWdJQ0FnSUNBZ0x5OGdSMlZ6ZEdselkya2diQ2RwYm1sNmFXRnNhWHA2WVhwcGIyNWxJR1JsYVNCMllXeHZjbWtnYzJGc2RtRjBhUW9nSUNBZ0lDQWdJR1J2WTNWdFpXNTBMbUZrWkVWMlpXNTBUR2x6ZEdWdVpYSW9KMk52Ym5SeWIyeHpTVzVwZEdsaGJHbDZaV1FuTENCbWRXNWpkR2x2YmlncElIdDdDaUFnSUNBZ0lDQWdJQ0FnSUdOdmJuTjBJSE5zYVdSbGNpQTlJR1J2WTNWdFpXNTBMbWRsZEVWc1pXMWxiblJDZVVsa0tDZDdZMjl1ZEhKdmJGOXBaSDBuS1RzS0lDQWdJQ0FnSUNBZ0lDQWdhV1lnS0hOc2FXUmxjaWtnZTNzS0lDQWdJQ0FnSUNBZ0lDQWdJQ0FnSUdOdmJuTjBJSFpoYkhWbElEMGdjR0Z5YzJWR2JHOWhkQ2h6Ykdsa1pYSXVkbUZzZFdVcE93b2dJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ1kyOXVjM1FnWkdsemNHeGhlVVZzWlcxbGJuUWdQU0JrYjJOMWJXVnVkQzVuWlhSRmJHVnRaVzUwUW5sSlpDZ25lMk52Ym5SeWIyeGZhV1I5TFhaaGJIVmxKeWs3Q2lBZ0lDQWdJQ0FnSUNBZ0lDQWdJQ0JwWmlBb1pHbHpjR3hoZVVWc1pXMWxiblFwSUh0N0NpQWdJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lDQWdaR2x6Y0d4aGVVVnNaVzFsYm5RdWRHVjRkRU52Ym5SbGJuUWdQU0JtYjNKdFlYUk9kVzFpWlhKZmUyTnZiblJ5YjJ4ZmFXUjlLSFpoYkhWbEtUc0tJQ0FnSUNBZ0lDQWdJQ0FnSUNBZ0lIMTlDaUFnSUNBZ0lDQWdJQ0FnSUNBZ0lDQXZMeUJPYjI0Z1kyaHBZVzFoY21VZ2RYQmtZWFJsVTJ4cFpHVnlWbUZzZFdVZ2NYVnBJSEJsY2lCbGRtbDBZWEpsSUd4dmIzQUtJQ0FnSUNBZ0lDQWdJQ0FnZlgwS0lDQWdJQ0FnSUNCOWZTazdDaUFnSUNBOEwzTmpjbWx3ZEQ0S0lDQWdJQ0lpSWdvZ0lDQWdjbVYwZFhKdUlHaDBiV3dLQ2lNZ1QzVjBjSFYwT2lCSVZFMU1JR1JsYkd4dklITnNhV1JsY2dwemJHbGtaWEpmYUhSdGJDQTlJR055WldGMFpWOXpiR2xrWlhJb2JHRmlaV3dzSUcxcGJsOTJZV3dzSUcxaGVGOTJZV3dzSUdSbFptRjFiSFJmZG1Gc0xDQnpkR1Z3TENCamIyNTBjbTlzWDJsa0xDQmpkWE4wYjIxZlkzTnpLU0E9IiwKICAgICJpZCI6ICI4MWE1ODY5MS05MGJiLTQ4NjctYmExMy02MzBkODI4ZmRkYjMiLAogICAgImlucHV0cyI6IFsKICAgICAgewogICAgICAgICJuYW1lIjogImxhYmVsIiwKICAgICAgICAidHlwZSI6IHsKICAgICAgICAgICJuYW1lIjogIlN5c3RlbS5TdHJpbmciLAogICAgICAgICAgImFzc2VtYmx5IjogIlN5c3RlbS5Qcml2YXRlLkNvcmVMaWIiCiAgICAgICAgfSwKICAgICAgICAicHJldHR5IjogImxhYmVsIiwKICAgICAgICAiZGVzYyI6ICJDb252ZXJ0cyB0byBjb2xsZWN0aW9uIG9mIHRleHQgZnJhZ21lbnRzIiwKICAgICAgICAicHJldmlld3MiOiB0cnVlLAogICAgICAgICJvcHRpb25hbCI6IHRydWUKICAgICAgfSwKICAgICAgewogICAgICAgICJuYW1lIjogInN0ZXAiLAogICAgICAgICJ0eXBlIjogewogICAgICAgICAgIm5hbWUiOiAiU3lzdGVtLk9iamVjdCIsCiAgICAgICAgICAiYXNzZW1ibHkiOiAiU3lzdGVtLlByaXZhdGUuQ29yZUxpYiIKICAgICAgICB9LAogICAgICAgICJwcmV0dHkiOiAic3RlcCIsCiAgICAgICAgImRlc2MiOiAicmhpbm9zY3JpcHRzeW50YXggZ2VvbWV0cnkiLAogICAgICAgICJwcmV2aWV3cyI6IHRydWUsCiAgICAgICAgIm9wdGlvbmFsIjogdHJ1ZQogICAgICB9LAogICAgICB7CiAgICAgICAgIm5hbWUiOiAibWluX3ZhbCIsCiAgICAgICAgInR5cGUiOiB7CiAgICAgICAgICAibmFtZSI6ICJTeXN0ZW0uT2JqZWN0IiwKICAgICAgICAgICJhc3NlbWJseSI6ICJTeXN0ZW0uUHJpdmF0ZS5Db3JlTGliIgogICAgICAgIH0sCiAgICAgICAgInByZXR0eSI6ICJtaW5fdmFsIiwKICAgICAgICAiZGVzYyI6ICJyaGlub3NjcmlwdHN5bnRheCBnZW9tZXRyeSIsCiAgICAgICAgInByZXZpZXdzIjogdHJ1ZSwKICAgICAgICAib3B0aW9uYWwiOiB0cnVlCiAgICAgIH0sCiAgICAgIHsKICAgICAgICAibmFtZSI6ICJtYXhfdmFsIiwKICAgICAgICAidHlwZSI6IHsKICAgICAgICAgICJuYW1lIjogIlN5c3RlbS5PYmplY3QiLAogICAgICAgICAgImFzc2VtYmx5IjogIlN5c3RlbS5Qcml2YXRlLkNvcmVMaWIiCiAgICAgICAgfSwKICAgICAgICAicHJldHR5IjogIm1heF92YWwiLAogICAgICAgICJkZXNjIjogInJoaW5vc2NyaXB0c3ludGF4IGdlb21ldHJ5IiwKICAgICAgICAicHJldmlld3MiOiB0cnVlLAogICAgICAgICJvcHRpb25hbCI6IHRydWUKICAgICAgfSwKICAgICAgewogICAgICAgICJuYW1lIjogImRlZmF1bHRfdmFsIiwKICAgICAgICAidHlwZSI6IHsKICAgICAgICAgICJuYW1lIjogIlN5c3RlbS5PYmplY3QiLAogICAgICAgICAgImFzc2VtYmx5IjogIlN5c3RlbS5Qcml2YXRlLkNvcmVMaWIiCiAgICAgICAgfSwKICAgICAgICAicHJldHR5IjogImRlZmF1bHRfdmFsIiwKICAgICAgICAiZGVzYyI6ICJyaGlub3NjcmlwdHN5bnRheCBnZW9tZXRyeSIsCiAgICAgICAgInByZXZpZXdzIjogdHJ1ZSwKICAgICAgICAib3B0aW9uYWwiOiB0cnVlCiAgICAgIH0sCiAgICAgIHsKICAgICAgICAibmFtZSI6ICJjb250cm9sX2lkIiwKICAgICAgICAidHlwZSI6IHsKICAgICAgICAgICJuYW1lIjogIlN5c3RlbS5PYmplY3QiLAogICAgICAgICAgImFzc2VtYmx5IjogIlN5c3RlbS5Qcml2YXRlLkNvcmVMaWIiCiAgICAgICAgfSwKICAgICAgICAicHJldHR5IjogImNvbnRyb2xfaWQiLAogICAgICAgICJkZXNjIjogInJoaW5vc2NyaXB0c3ludGF4IGdlb21ldHJ5IiwKICAgICAgICAicHJldmlld3MiOiB0cnVlLAogICAgICAgICJvcHRpb25hbCI6IHRydWUKICAgICAgfSwKICAgICAgewogICAgICAgICJuYW1lIjogImN1c3RvbV9jc3MiLAogICAgICAgICJ0eXBlIjogewogICAgICAgICAgIm5hbWUiOiAiU3lzdGVtLk9iamVjdCIsCiAgICAgICAgICAiYXNzZW1ibHkiOiAiU3lzdGVtLlByaXZhdGUuQ29yZUxpYiIKICAgICAgICB9LAogICAgICAgICJwcmV0dHkiOiAiY3VzdG9tX2NzcyIsCiAgICAgICAgImRlc2MiOiAicmhpbm9zY3JpcHRzeW50YXggZ2VvbWV0cnkiLAogICAgICAgICJwcmV2aWV3cyI6IHRydWUsCiAgICAgICAgIm9wdGlvbmFsIjogdHJ1ZQogICAgICB9CiAgICBdLAogICAgIm91dHB1dHMiOiBbCiAgICAgIHsKICAgICAgICAibmFtZSI6ICJzbGlkZXJfaHRtbCIsCiAgICAgICAgInR5cGUiOiB7CiAgICAgICAgICAibmFtZSI6ICJTeXN0ZW0uT2JqZWN0IiwKICAgICAgICAgICJhc3NlbWJseSI6ICJTeXN0ZW0uUHJpdmF0ZS5Db3JlTGliIgogICAgICAgIH0sCiAgICAgICAgInN0cmljdCI6IGZhbHNlLAogICAgICAgICJwcmV0dHkiOiAic2xpZGVyX2h0bWwiLAogICAgICAgICJkZXNjIjogInJoaW5vc2NyaXB0c3ludGF4IGdlb21ldHJ5IiwKICAgICAgICAicHJldmlld3MiOiB0cnVlLAogICAgICAgICJvcHRpb25hbCI6IGZhbHNlCiAgICAgIH0KICAgIF0sCiAgICAibmlja25hbWUiOiAic2xpZGVyIGNvbnRyb2wiCiAgfQp9";
    static readonly string s_scriptIconData = "iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAEsWlUWHRYTUw6Y29tLmFkb2JlLnhtcAAAAAAAPD94cGFja2V0IGJlZ2luPSLvu78iIGlkPSJXNU0wTXBDZWhpSHpyZVN6TlRjemtjOWQiPz4KPHg6eG1wbWV0YSB4bWxuczp4PSJhZG9iZTpuczptZXRhLyIgeDp4bXB0az0iWE1QIENvcmUgNS41LjAiPgogPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4KICA8cmRmOkRlc2NyaXB0aW9uIHJkZjphYm91dD0iIgogICAgeG1sbnM6dGlmZj0iaHR0cDovL25zLmFkb2JlLmNvbS90aWZmLzEuMC8iCiAgICB4bWxuczpleGlmPSJodHRwOi8vbnMuYWRvYmUuY29tL2V4aWYvMS4wLyIKICAgIHhtbG5zOnBob3Rvc2hvcD0iaHR0cDovL25zLmFkb2JlLmNvbS9waG90b3Nob3AvMS4wLyIKICAgIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyIKICAgIHhtbG5zOnhtcE1NPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvbW0vIgogICAgeG1sbnM6c3RFdnQ9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZUV2ZW50IyIKICAgdGlmZjpJbWFnZUxlbmd0aD0iMjQiCiAgIHRpZmY6SW1hZ2VXaWR0aD0iMjQiCiAgIHRpZmY6UmVzb2x1dGlvblVuaXQ9IjIiCiAgIHRpZmY6WFJlc29sdXRpb249IjIzLzEiCiAgIHRpZmY6WVJlc29sdXRpb249IjIzLzEiCiAgIGV4aWY6UGl4ZWxYRGltZW5zaW9uPSIyNCIKICAgZXhpZjpQaXhlbFlEaW1lbnNpb249IjI0IgogICBleGlmOkNvbG9yU3BhY2U9IjEiCiAgIHBob3Rvc2hvcDpDb2xvck1vZGU9IjMiCiAgIHBob3Rvc2hvcDpJQ0NQcm9maWxlPSJzUkdCIElFQzYxOTY2LTIuMSIKICAgeG1wOk1vZGlmeURhdGU9IjIwMjQtMDctMThUMTU6MTQ6NTAtMDc6MDAiCiAgIHhtcDpNZXRhZGF0YURhdGU9IjIwMjQtMDctMThUMTU6MTQ6NTAtMDc6MDAiPgogICA8eG1wTU06SGlzdG9yeT4KICAgIDxyZGY6U2VxPgogICAgIDxyZGY6bGkKICAgICAgc3RFdnQ6YWN0aW9uPSJwcm9kdWNlZCIKICAgICAgc3RFdnQ6c29mdHdhcmVBZ2VudD0iQWZmaW5pdHkgUGhvdG8gMiAyLjUuMiIKICAgICAgc3RFdnQ6d2hlbj0iMjAyNC0wNy0xOFQxNToxNDo1MC0wNzowMCIvPgogICAgPC9yZGY6U2VxPgogICA8L3htcE1NOkhpc3Rvcnk+CiAgPC9yZGY6RGVzY3JpcHRpb24+CiA8L3JkZjpSREY+CjwveDp4bXBtZXRhPgo8P3hwYWNrZXQgZW5kPSJyIj8+PFPNtgAAAYFpQ0NQc1JHQiBJRUM2MTk2Ni0yLjEAACiRdZG7SwNBEIc/EyWikYiKWFgEUatEokLQxiLBF6hFjGDUJrm8hDyOuwQJtoJtQEG08VXoX6CtYC0IiiKItbWijYZzLglExMwyO9/+dmfYnQVLMKWk9UYPpDM5LTDtcy6HVpy2V2x00E0r3rCiq/OLU0Hq2ucDDWa8c5u16p/711qjMV2BhmbhCUXVcsIzwnMbOdXkXeEuJRmOCp8LuzS5oPC9qUcq/GpyosLfJmvBgB8s7cLOxC+O/GIlqaWF5eX0p1N5pXof8yX2WGZpUWKfeC86Aabx4WSWSfx4GWZcZi9uRhiSFXXyPeX8BbKSq8isUkBjnQRJcrhEzUv1mMS46DEZKQpm///2VY+PjlSq233Q9GIY7wNg24FS0TC+jg2jdALWZ7jK1PKzRzD2IXqxpvUfgmMLLq5rWmQPLreh50kNa+GyZBW3xOPwdgZtIei8hZbVSs+q+5w+QnBTvuoG9g9gUM471n4AjtNn+CCeQOAAAAAJcEhZcwAAA4oAAAOKAaeM9R8AAAH2SURBVEiJ1ZU/ixpBGMZ/M86aI+RMIFuKfgu7Bb9DCCgSAlYHKdIdgTSpQgrJ1zCw/dlb+jHEMkb0skvi/EtxrsypezmXNHlh2Nl333meeWZm54H/PURJXu5a2fey8IDbtWOCbrerWq3WZ+/9lbX28hxkpdStMebbdDr9sFgsbgENoMKidrv9sdPpvB8Oh3WlFNZajDFYa7HW4pzDObfve+/3z+VyeZmm6ZskSdR4PL4G1oAOCSTwbjAY1OfzOXmeI4RASomUct8vy8VxTL/fvxiNRq+BT8AvwMqQQGv9sl6vnw1e5BqNBsaYZ8ATIAJkSAAgrbWVwIUQWGsLnGi3IiIkEADW2krgUkqMMeFy3+8UURCcCy6ECAlEKYFzrhK4lDJcIkoJDhXIo20KBh/Uaa2PatRhIlRQANRqtZOzLkhObHK5AufcEdhDCsK6UwpKCarsw6MUeO8rb3IlBeeoCY5pOQFQ+U8OCPxJAqXUZrVaVQLfbDbkeY5S6ieBH4TH1Dvnxmmavu31ehdxHOO9Z7vd7q9sYwzGGLTW996NMazXa2az2TbLshvuvMABPjQc2Ww2XyRJ8jWKole7W/HRoZTKsiy7mUwmX/I8/w78APJDS4yA58BTgiuXv1tnYZUa+A3k7Azn1MDoAPycCEk0D8zsn5n+H1yr/u4xAeJ3AAAAAElFTkSuQmCC";

    public override Guid ComponentGuid { get; } = new Guid("81a58691-90bb-4867-ba13-630d828fddb3");

    public override GH_Exposure Exposure { get; } = GH_Exposure.primary;

    public override bool Obsolete { get; } = false;

    public ProjectComponent_81a58691() : base(s_scriptData, s_scriptIconData,
        name: "slider control",
        nickname: "slider control",
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