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
        public bool SuccessfullyLoaded { get; set; } = false;
    }

}
