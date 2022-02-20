using BepisModManager.Http;
using Newtonsoft.Json;

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
            var response = HttpHelper.GetStringFromURLAsync(url).GetAwaiter().GetResult();

            var artifacts = JsonConvert.DeserializeObject<BleedingArtifacts>(response);
            return artifacts;
        }
    }
}