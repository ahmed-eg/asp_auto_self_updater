using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace AspAutoSelfUpdater.Services
{
    class UpdaterService
    {

        static string UpdateFilePath => "update_files";

        public static bool CheckNewVersion() {
            var setting = SettingService.Setting;
            var curentVersion = FileVersionInfo.GetVersionInfo(setting.TargetAssemblyName).ProductVersion;
            Console.WriteLine($"Current Version is {curentVersion}");
            var serverVersion = GetServerVersion(setting.GetVersion);
            Console.WriteLine($"Server Version is {serverVersion}");            
            return curentVersion.CompareTo(serverVersion) != 0;
        }

        public static bool DownloadNewVersion()
        {
            var setting = SettingService.Setting;


            if (!GetUpdateFiles(setting.GetZipFile, UpdateFilePath))
            {
                Console.WriteLine($"Cannot download update files!!");
                return false;
            }

            return true;
        }


        public static bool StartUpdate()
        {
            try
            {
                
                if (!Directory.Exists(UpdateFilePath))
                {
                    Console.WriteLine($"Update folder is not exist!!");
                    return true;
                }

                if (!File.Exists("app_offline.htm"))
                {
                    File.WriteAllText("app_offline.htm", "<body><h1>Auto Updating....</h1></body>");
                }

                //int timeout = 10;
                //if (processID >= 0)
                //{
                //    while (Process.GetProcesses().Any(e => e.Id == processID))
                //    {
                //        if (timeout <= 0)
                //        {
                //            Console.WriteLine($"Timeout, try to kill the app process....");
                //            Process.GetProcessById(processID).Kill();
                //        }
                //        else
                //        {
                //            Console.WriteLine($"Waiting Application to exit....");
                //            timeout--;
                //        }
                //        System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
                //    }
                //}

                Console.WriteLine($"Start Copy Files");
                foreach (var file in Directory.GetFiles(UpdateFilePath))
                {
                    Console.WriteLine($"Copying {file}");
                    File.Copy(file, Path.GetFileName(file), true);
                }

                Directory.Delete(UpdateFilePath, true);

                if (File.Exists("app_offline.htm"))
                    File.Delete("app_offline.htm");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Will start updating again shortlly...");
                return false;
            }
        }


        private static string GetServerVersion(Models.UrlInfo url)
        {
            if (url.Type != "GET")
            {
                Console.WriteLine($"HTTP type not supported!  {url.Type}");
                return "";
            }
            var result = HttpService.HttpGet(url.Url);
            if (result == null || !result.IsSuccessStatusCode) return null;
            var responseString = result.Content.ReadAsStringAsync();
            responseString.Wait();
            return responseString.Result;

        }


        private static bool GetUpdateFiles(Models.UrlInfo url,string path)
        {
            if (url.Type != "GET")
            {
                Console.WriteLine($"HTTP type not supported!  {url.Type}");
                return false;
            }
            if (!HttpService.HttpGetDownloadFIle(url.Url, "file.zip")) return false;

            Console.WriteLine($"Zip file downloaded successfully!");

            if (Directory.Exists(path))
                Directory.Delete(path, true);

            ZipFile.ExtractToDirectory("file.zip", path);

            Console.WriteLine($"file unziped successfully!");

            File.Delete("file.zip");

            return true;
        }
    }
}
