using Newtonsoft.Json;
using System.Net.Http;
using System.Reflection;

namespace BepisModManager.BepInEx
{
    public static class Release
    {
        public static ReleaseArtifacts GetLatestArtifacts()
            => GetArtifactsFromURL(References.ReleaseLatestURL);

        public static ReleaseArtifacts GetArtifactsWithVersion(string version)
            => GetArtifactsFromURL(References.ReleaseURL(version));

        static ReleaseArtifacts GetArtifactsFromURL(string url)
        {
            using (var httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("User-Agent", $"BepisModManager/${Assembly.GetExecutingAssembly().GetName().Version.ToString()}");
                var response = httpClient.SendAsync(request).GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult();

                var artifacts = JsonConvert.DeserializeObject<ReleaseArtifacts>(response);
                return artifacts;
            }
        }
    }
}