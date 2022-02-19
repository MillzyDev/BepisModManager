using Newtonsoft.Json;
using System;
using System.Linq;

namespace BepisModManager.BepInEx
{
    public class ReleaseArtifacts
    {
        public class ReleaseArtifact
        {
            [JsonProperty("browser_download_url")]
            public string DownloadURL
            {
                get;
                set;
            }

            [JsonIgnore]
            ReleaseUnityBackend backend;

            [JsonIgnore]
            public ReleaseUnityBackend Backend { 
                get
                {
                    if (backend == null)
                    {
                        string[] url = DownloadURL.Split('/');
                        string file = url[url.Length - 1];
                        string[] components = file.Split('_');
                        string backendStr = String.Join("_", components[0], components[1]);
                        if (!Enum.TryParse(backendStr, out backend))
                        {
                            backend = ReleaseUnityBackend.BepInEx_x64;
                        }
                    }
                    return backend;
                }
            }
        }

        public ReleaseArtifact GetArtifact(ReleaseUnityBackend backend)
            => artifacts.First(x => x.Backend == backend);

        [JsonIgnore]
        string version;

        [JsonProperty("tag_name")]
        public string Version
        {
            get => version;
            set => version = value.Replace("v", "");
        }

        [JsonProperty("assets")]
        public ReleaseArtifact[] artifacts;
    }
}
