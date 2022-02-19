using Newtonsoft.Json;
using System.Linq;

namespace BepisModManager.BepInEx
{
    public class BleedingArtifacts
    {
        public class BleedingArtifact
        {
            [JsonProperty("description")]
            public string Description
            {
                get;
                set;
            }

            [JsonProperty("file")]
            public string File
            {
                get;
                set;
            }
        }

        public BleedingArtifact GetArtifact(BleedingUnityBackend backend)
        {
            string stringValue = backend.ToString();
            return Artifacts.First(x => x.File.StartsWith(stringValue));
        }
               
        [JsonProperty("artifacts")]
        public BleedingArtifact[] Artifacts
        {
            get;
            set;
        }

        [JsonProperty("changelog")]
        public string Changelog
        {
            get;
            set;
        }

        [JsonProperty("date")]
        public string Date
        {
            get;
            set;
        }

        [JsonProperty("hash")]
        public string Hash
        {
            get;
            set;
        }

        [JsonProperty("id")]
        public string Id
        {
            get;
            set;
        }

        [JsonProperty("short_hash")]
        public string ShortHash
        {
            get;
            set;
        }
    }
}