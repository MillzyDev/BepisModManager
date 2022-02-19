using System.Diagnostics;
using System.IO;
using BepisModManager.BepInEx;
using BepisModManager.Installation.Exceptions;

namespace BepisModManager.Installation
{
    class UnityGame
    {
        string name;
        string path;
        Platform platform;
        UnityBackend backend;
        string unityVersion;
        bool isModded;

        public UnityGame(string gamePath)
        {
            string unityPlayer = Path.Combine(gamePath, @"UnityPlayer.dll");

            // check if dir is actually a unity game
            if (!File.Exists(unityPlayer))
                throw new NonUnityGameException("Game provided is not a UnityEngine game");

            // set the game name
            name = Path.GetDirectoryName(gamePath);

            // get the game unity version
            var playerInfo = FileVersionInfo.GetVersionInfo(unityPlayer);
            unityVersion = playerInfo.FileVersion;

            // check if game is x64 or x86
            var dllAnalysis = new DllAnalyser(unityPlayer);
            platform = dllAnalysis.Platform;

            // check if the game uses speedy IL2CPP or slow Mono
            if (File.Exists(Path.Combine(gamePath, @"GameAssembly.dll")))
                backend = UnityBackend.IL2CPP;
            else
                backend = UnityBackend.Mono;

            if (Directory.Exists(Path.Combine(gamePath, @"BepInEx")))
                isModded = true;

            path = gamePath;
        }


        public void InstallBepInEx(bool bleedingEdge = false)
        {
            string downloadURL;

            if (bleedingEdge || backend == UnityBackend.IL2CPP)
            {
                var latestArtifacts = BleedingEdge.GetLatestArtifacts();

                if (platform == Platform.x64 && backend == UnityBackend.IL2CPP)
                    downloadURL = BleedingEdge.GetDownloadURL(latestArtifacts.Id, latestArtifacts.GetArtifact(BleedingUnityBackend.BepInEx_UnityIL2CPP_x64).File);
                else if (platform == Platform.x86 && backend == UnityBackend.IL2CPP)
                    downloadURL = BleedingEdge.GetDownloadURL(latestArtifacts.Id, latestArtifacts.GetArtifact(BleedingUnityBackend.BepInEx_UnityIL2CPP_x86).File);
                else if (platform == Platform.x64 && backend == UnityBackend.Mono)
                    downloadURL = BleedingEdge.GetDownloadURL(latestArtifacts.Id, latestArtifacts.GetArtifact(BleedingUnityBackend.BepInEx_UnityMono_x64).File);
                else if (platform == Platform.x86 && backend == UnityBackend.Mono)
                    downloadURL = BleedingEdge.GetDownloadURL(latestArtifacts.Id, latestArtifacts.GetArtifact(BleedingUnityBackend.BepInEx_UnityMono_x86).File);
                else
                    downloadURL = BleedingEdge.GetDownloadURL(latestArtifacts.Id, latestArtifacts.GetArtifact(BleedingUnityBackend.BepInEx_UnityMono_unix).File);
            } 
            else
            {
                var latestArtifacts = Release.GetLatestArtifacts();

                if (platform == Platform.x64)
                    downloadURL = latestArtifacts.GetArtifact(ReleaseUnityBackend.BepInEx_x64).DownloadURL;
                else if (platform == Platform.x86)
                    downloadURL = latestArtifacts.GetArtifact(ReleaseUnityBackend.BepInEx_x86).DownloadURL;
                else
                    downloadURL = latestArtifacts.GetArtifact(ReleaseUnityBackend.BepInEx_unix).DownloadURL;
            }

            // TODO: Download file and install to game
        }

        public string Name
        {
            get => name;
        }

        public string UnityVersion
        {
            get => unityVersion;
        }

        public Platform Platform
        {
            get => platform;
        }

        public UnityBackend Backend
        {
            get => backend;
        }
    }
}