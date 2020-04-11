using System;
using System.Collections.Generic;
using System.Text;

namespace AspAutoSelfUpdater.Models
{
    class UrlInfo
    {
        public string Url { get; set; }
        public string Type { get; set; } = "GET";
        public object Payload { get; set; }
    }
}
