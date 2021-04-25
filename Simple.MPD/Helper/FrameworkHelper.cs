using System.Threading.Tasks;

namespace Simple.MPD.Helper
{
    public static class FrameworkHelper
    {
#if NET45
        static Task s_completedTask = null;
#endif

        public static Task GetCompletedTask()
        {
#if NET45
            var completedTask = s_completedTask;
            if (completedTask == null)
                s_completedTask = completedTask = new Task(() => { return; }); // benign initialization ----
            return completedTask;
#else
            return Task.CompletedTask;
#endif
        }
    }
}
