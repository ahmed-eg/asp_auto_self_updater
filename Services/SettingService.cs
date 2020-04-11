using AspAutoSelfUpdater.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AspAutoSelfUpdater.Services
{
    class SettingService
    {
        public Setting _Setting { get; set; }

        private static SettingService settingService=null;
        public static Setting Setting
        {
            get
            {
                if (settingService == null)
                    settingService = new SettingService();

                return settingService._Setting;
            }
        }

        public SettingService()
        {
            try
            {
                if (File.Exists("AspAutoSelfUpdater.Setting.json"))
                {
                    var settingData = File.ReadAllText("AspAutoSelfUpdater.Setting.json");
                    _Setting = JsonConvert.DeserializeObject<Setting>(settingData);
                    _Setting.SuccessfullyLoaded = true;
                    return;
                }
                else {
                    Console.WriteLine($"Setting File not exist!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errir Reading Setting file: {ex}");            
            }

            _Setting = new Setting {
                SuccessfullyLoaded = false
            };
        }
    }
}
