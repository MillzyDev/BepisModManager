namespace BepisModManager.BepInEx
{
    static class References
    {
        public static string BleedingBaseURL
        {
            get => "https://builds.bepinex.dev/api";
        }

        public static string BleedingProjectURL
        {
            get => "https://builds.bepinex.dev/projects/bepinex_be";
        }

        public static string BleedingEdgeLatestURL
        {
            get => $"{BleedingBaseURL}/projects/bepinex_be/artifacts/latest";
        }

        public static string ReleaseBaseURL
        {
            get => "https://api.github.com/repos/BepInEx/BepInEx/releases";
        }

        public static string ReleaseLatestURL
        {
            get => $"{ReleaseBaseURL}/latest";
        }

        public static string BleedingEdgeURL(string id)
        {
            return $"{BleedingBaseURL}/projects/bepinex_be/artifacts/{id}";
        }

        public static string ReleaseURL(string version)
        {
            return $"{ReleaseBaseURL}/tags/v{version}";
        }
    }
}