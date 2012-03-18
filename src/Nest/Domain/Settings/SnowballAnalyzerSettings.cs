﻿using Newtonsoft.Json;

namespace Nest
{
    public class SnowballAnalyzerSettings : AnalyzerSettings
    {
        public SnowballAnalyzerSettings()
        {
            this.Type = "snowball";
        }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("stopwords")]
        public string StopWords { get; set; }
    }
}