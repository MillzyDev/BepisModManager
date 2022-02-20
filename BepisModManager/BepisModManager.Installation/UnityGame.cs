using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using BepisModManager.BepInEx;
using BepisModManager.Http;
using BepisModManager.Installation.Exceptions;

namespace BepisModManager.Installation
{
    public class UnityGame
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
            string[] splitPath = gamePath.Split('/');
            name = splitPath[splitPath.Length - 1];

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
            string downloadURL = "";
            string fileName = "";

            if (bleedingEdge || backend == UnityBackend.IL2CPP)
            {
                var latestArtifacts = BleedingEdge.GetLatestArtifacts();
                BleedingArtifacts.BleedingArtifact artifact;

                if (platform == Platform.x64 && backend == UnityBackend.IL2CPP)
                {
                    artifact = latestArtifacts.GetArtifact(BleedingUnityBackend.BepInEx_UnityIL2CPP_x64);
                }
                else if (platform == Platform.x86 && backend == UnityBackend.IL2CPP)
                {
                    artifact = latestArtifacts.GetArtifact(BleedingUnityBackend.BepInEx_UnityIL2CPP_x86);
                }
                else if (platform == Platform.x64 && backend == UnityBackend.Mono)
                {
                    artifact = latestArtifacts.GetArtifact(BleedingUnityBackend.BepInEx_UnityMono_x64);
                }
                else if (platform == Platform.x86 && backend == UnityBackend.Mono)
                {
                    artifact = latestArtifacts.GetArtifact(BleedingUnityBackend.BepInEx_UnityMono_x86);
                }
                else
                {
                    artifact = latestArtifacts.GetArtifact(BleedingUnityBackend.BepInEx_UnityMono_unix);
                }

                fileName = artifact.File;
                downloadURL = BleedingEdge.GetDownloadURL(latestArtifacts.Id, fileName);

            } 
            else
            {
                var latestArtifacts = Release.GetLatestArtifacts();
                ReleaseArtifacts.ReleaseArtifact artifact;

                if (platform == Platform.x64)
                    artifact = latestArtifacts.GetArtifact(ReleaseUnityBackend.BepInEx_x64);
                else if (platform == Platform.x86)
                    artifact = latestArtifacts.GetArtifact(ReleaseUnityBackend.BepInEx_x86);
                else
                    artifact = latestArtifacts.GetArtifact(ReleaseUnityBackend.BepInEx_unix);

                downloadURL = artifact.DownloadURL;
                fileName = artifact.FileName;
            }

            // create temp stuff if it doesnt exist
            Directory.CreateDirectory(TempStorage.TempPath);

            // download the zip
            string tempPath = Path.Combine(TempStorage.TempPath, fileName);
            HttpHelper.DownloadFileAsync(downloadURL, Path.Combine(TempStorage.TempPath, fileName)).GetAwaiter().GetResult();

            // extract zip
            var zipArchive = ZipFile.OpenRead(tempPath);
            zipArchive.ExtractToDirectory(path, true);
            
            isModded = true;
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

        public bool IsModded
        {
            get => isModded;
        }
    }
}