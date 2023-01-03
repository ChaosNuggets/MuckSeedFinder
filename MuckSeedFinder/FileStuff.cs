using System;
using System.IO;
using UnityEngine;

namespace MuckSeedFinder
{
    internal class FileStuff
    {
        public static void LogSeed(double distance)
        {
            string path = Environment.GetFolderPath(
                Environment.SpecialFolder.DesktopDirectory) + @"\muck_seeds.csv"; // Get the path to the desktop

            if (!File.Exists(path))
            {
                // Create a file to write to
                WriteHeader(path);
            }

            WriteData(path, CreateWorld.seed, distance, FindPositions.hasFoundBow);

            Debug.Log($"Logged seed {CreateWorld.seed}");
        }

        private static void WriteHeader(string path)
        {
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine("Seed,Distance,Ancient Bow");
            }
        }

        private static void WriteData(string path, int seed, double distance, bool ancientBow)
        {
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine($"{seed},{distance}," + (ancientBow ? "yes" : "no"));
            }
        }
    }
}
