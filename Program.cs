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

            Console.WriteLine("Start Updating...");
            //string updateFolder = "update_files";
            //int appProcessId = -1;

            //if (args.Length > 0) updateFolder = args[0];
            //if (args.Length > 1) appProcessId = int.Parse(args[1]);

            while (!Services.UpdaterService.StartUpdate())
                System.Threading.Thread.Sleep(1000);
        }
    }
}
