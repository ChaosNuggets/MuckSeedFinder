using HarmonyLib;
using UnityEngine;

namespace MuckSeedFinder
{
    internal class Reset
    {
        [HarmonyPatch(typeof(LoadingScreen), "FinishLoading")]
        [HarmonyPrefix]
        static void ResetWorld()
        {
            if (FindPositions.chiefsSpears.Count > 0)
            {
                FileStuff.LogSeed();
            }
            ResetVariables();
            GameManager.instance.LeaveGame();
        }

        private static void ResetVariables()
        {
            FindPositions.hasFoundBow = false;
            FindPositions.chiefsSpears.Clear();
            FindPositions.guardians.Clear();
            CreateWorld.seed++;
        }
    }
}
