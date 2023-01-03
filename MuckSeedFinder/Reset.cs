using HarmonyLib;
using UnityEngine;

namespace MuckSeedFinder
{
    internal class Reset
    {
        public static bool hasReset = false;

        public static void ResetWorld()
        {
            if (hasReset) return;

            hasReset = true;

            if (FindPositions.chiefsSpears.Count > 0)
            {
                double distance = CalculateDistance.CalculateShortestDistance(
                    FindPositions.spawn,
                    FindPositions.chiefsSpears,
                    FindPositions.guardians,
                    FindPositions.boat
                );

                FileStuff.LogSeed(distance);
            }
            GameManager.instance.LeaveGame();
            Debug.Log("Reset world");
        }

        public static void ResetVariables()
        {
            FindPositions.hasFoundBow = false;
            FindPositions.chiefsSpears.Clear();
            FindPositions.guardians.Clear();
            CreateWorld.seed++;
            hasReset = false;
            Debug.Log("Reset variables");
        }
    }
}
