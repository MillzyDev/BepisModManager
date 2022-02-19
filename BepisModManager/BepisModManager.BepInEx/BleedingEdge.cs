using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Reflection;

namespace BepisModManager.BepInEx
{
    public static class BleedingEdge
    {
        public static BleedingArtifacts GetLatestArtifacts() 
            => GetArtifactsFromURL(References.BleedingEdgeLatestURL);

        public static BleedingArtifacts GetArtifactsWithId(string id) 
            => GetArtifactsFromURL(References.BleedingEdgeURL(id));

        public static string GetDownloadURL(string id, string file) 
            => $"{References.BleedingProjectURL}/{id}/{file}";

        static BleedingArtifacts GetArtifactsFromURL(string url)
        {
            using (var httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("User-Agent", $"BepisModManager/${Assembly.GetExecutingAssembly().GetName().Version.ToString()}");
                var response = httpClient.SendAsync(request).GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult();

                var artifacts = JsonConvert.DeserializeObject<BleedingArtifacts>(response);
                return artifacts;
            }
        }
    }
}