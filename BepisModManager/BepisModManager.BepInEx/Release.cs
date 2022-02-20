using BepisModManager.Http;
using Newtonsoft.Json;

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
            var response = HttpHelper.GetStringFromURLAsync(url).GetAwaiter().GetResult();

            var artifacts = JsonConvert.DeserializeObject<ReleaseArtifacts>(response);
            return artifacts;
        }
    }
}