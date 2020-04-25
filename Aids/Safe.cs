using System;
using System.Collections.Generic;
using System.Text;

namespace WebApp.Aids
{
    public static class Safe
    {
        private static readonly object key = new object();
        public static T Run<T>(Func<T> function, T valueOnExeption,
            bool useLock = false)
        {
            return useLock
                ? lockedRun(function, valueOnExeption)
                : run(function, valueOnExeption);
        }
        public static void Run(Action action, bool useLock = false)
        {
            if (useLock) lockedRun(action);
            else run(action);
        }

#pragma warning disable IDE1006 // Naming Styles
        private static T run<T>(Func<T> function, T valueOnExeption)
#pragma warning restore IDE1006 // Naming Styles
        {
            try
            {
                return function();
            }
            catch (Exception e)
            {
                Log.Exception(e);
                return valueOnExeption;
            }
        }

#pragma warning disable IDE1006 // Naming Styles
        private static T lockedRun<T>(Func<T> function, T valueOnExeption)
#pragma warning restore IDE1006 // Naming Styles
        {
            lock (key) { return run(function, valueOnExeption); }
        }
#pragma warning disable IDE1006 // Naming Styles
        private static void run(Action action)
#pragma warning restore IDE1006 // Naming Styles
        {
            try { action(); } catch (Exception e) { Log.Exception(e); }
        }
#pragma warning disable IDE1006 // Naming Styles
        private static void lockedRun(Action action)
#pragma warning restore IDE1006 // Naming Styles
        {
            lock (key) { run(action); }
        }
    }
}
