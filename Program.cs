using System;

namespace AspAutoSelfUpdater
{
    class Program
    {
        static void Main(string[] args)
        {

            if (args.Length == 0)
            {
                if (!Services.UpdaterService.CheckNewVersion())
                    return;

                if (!Services.UpdaterService.DownloadNewVersion())
                    return;
            }

            while (!Services.UpdaterService.StartUpdate())
                System.Threading.Thread.Sleep(1000);
        }
    }
}
