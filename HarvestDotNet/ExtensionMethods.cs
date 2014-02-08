using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
    public static async Task<T> ReadAsAsync<T>(this HttpContent httpContent)
    {
        var jsonString = await httpContent.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(jsonString);
    }
  }
}
