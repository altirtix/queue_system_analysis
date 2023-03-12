using System;
using System.Windows;

namespace QS.Controllers.UI.NavBar
{
    public static class Tools
    {
        public static void Restart()
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        public static void Exit()
        {
            Environment.Exit(0);
        }
    }
}
