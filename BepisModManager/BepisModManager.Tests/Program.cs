using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepisModManager.BepInEx;
using BepisModManager.Installation;

namespace BepisModManager.Tests
{
    class Program
    {
        public static void Main(string[] args)
        {
            BleedingArtifacts bleeding = BleedingEdge.GetLatestArtifacts();

            BleedingArtifacts oldBleeding = BleedingEdge.GetArtifactsWithId("520");

            ReleaseArtifacts release = Release.GetLatestArtifacts();

            ReleaseArtifacts oldRelease = Release.GetArtifactsWithVersion("5.4.12");


            Console.WriteLine(
                $"Latest Bleeding Version: b{bleeding.Id}\nb520 Download URL: {BleedingEdge.GetDownloadURL(oldBleeding.Id, oldBleeding.Artifacts[0].File)}\nLatest Release Version: {release.Version}\nv5.4.12 Download URL: {oldRelease.artifacts[0].DownloadURL}"
                );

            var analyser = new DllAnalyser("G:/SteamLibrary/steamapps/common/Just Shapes & Beats/UnityPlayer.dll");
            Console.WriteLine(analyser.Headers);
            Console.WriteLine($"JSAB is {analyser.Platform.ToString()}");
        }
    }
}
