using HarmonyLib;
using System;
using System.IO;
using UnityEngine;

namespace MuckSeedFinder
{
    internal class FileStuff
    {
        public static void LogSeed()
        {
            string path = Environment.GetFolderPath(
                Environment.SpecialFolder.DesktopDirectory) + @"muck_seeds.csv"; // Get the path to the desktop

            if (!File.Exists(path))
            {
                // Create a file to write to
                WriteHeader(path);
            }

            WriteData(path);

            Debug.Log($"Logged seed {CreateWorld.seed}");
        }

        private static void WriteHeader(string path)
        {
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine("Seed,Distance,Ancient Bow");
            }
        }

        private static void WriteData(string path)
        {
            using (StreamWriter sw = File.AppendText(path))
            {
                // TODO: add the logging code here
            }

            FileLog.Log($"{CreateWorld.seed}:");

            foreach (Item item in FindPositions.foundItems)
            {
                FileLog.Log($"{item.name} at {item.position}");
            }
        }
    }
}
