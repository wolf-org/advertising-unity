using System;

namespace VirtueSky.Ads
{
    public static class Common
    {
        public static void CallActionAndClean(ref Action action)
        {
            if (action == null) return;
            var a = action;
            a();
            action = null;
        }
    }
}