using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarvestDotNet
{
  internal static class ExtensionMethods
  {
    internal static string ToFormat(this string formatString, params object[] objs)
    {
      if (formatString == null)
        return null;
      return string.Format(formatString, objs);
    }

    internal static Task<TResult> CreateTaskFromResult<TResult>(this TResult result)
    {
      var taskSource = new TaskCompletionSource<TResult>();
      taskSource.SetResult(result);
      return taskSource.Task;
    }
  }
}
