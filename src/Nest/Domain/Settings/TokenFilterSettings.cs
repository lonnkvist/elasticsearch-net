﻿using Newtonsoft.Json;

namespace Nest
{
    public abstract class TokenFilterSettings
    {
        protected TokenFilterSettings(string type)
        {
            this.Type = type;
        }

        [JsonProperty("type")]
        public string Type { get; protected set; }
    }
}