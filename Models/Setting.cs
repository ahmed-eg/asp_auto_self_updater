using System;
using System.Collections.Generic;
using System.Text;

namespace AspAutoSelfUpdater.Models
{
    class Setting
    {
        public UrlInfo GetVersion { get; set; }
        public UrlInfo GetZipFile { get; set; }

        public string TargetAssemblyName { get; set; }
        public int CheckIntervalSeconds { get; set; } = 3600;
        public bool CheckIntervalEnable { get; set; } = false;
        public bool SuccessfullyLoaded { get; set; } = false;
    }

}
