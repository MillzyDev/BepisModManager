using System;
using System.Collections.Generic;
using System.IO;

namespace BepisModManager.Installation
{
    public class GameRegistry
    {
        private List<UnityGame> games = new List<UnityGame>();

        public List<UnityGame> Games
        {
            get => games;
        }

        private static GameRegistry instance;

        public static GameRegistry Instance
        {
            get
            {
                if (instance == null)
                    instance = new GameRegistry();
                return instance;
            }
        }

        public void AddGameLibrary(string path)
        {
            string[] gameDirs = Directory.GetDirectories(path);

            try
            {
                foreach (string dir in gameDirs)
                {
                    AddGame(dir);
                }
            }
            catch(Exception) 
            {
                // game couldnt be added for whatever reason, most likely cuz its not a unity game
            }
        }

        public void AddGame(string path)
            => games.Add(new UnityGame(path));
        
    }
}