using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MuckSeedFinder
{
    internal class Reset
    {
        [HarmonyPatch(typeof(LoadingScreen), "FinishLoading")]
        [HarmonyPrefix]
        static void LeaveWorld()
        {
            if (TestSeed.hasFoundMeleeWeapon)
            {
                LogSeed();
            }
            ResetVariables();
            GameManager.instance.LeaveGame();
        }

        private static void LogSeed()
        {
            FileLog.Log($"{CreateWorld.seed}:");

            foreach (Item item in TestSeed.foundItems)
            {
                FileLog.Log($"{item.name} at {item.position}");
            }

            Debug.Log($"Logged seed {CreateWorld.seed}");
        }

        private static void ResetVariables()
        {
            TestSeed.hasFoundMeleeWeapon = false;
            CreateWorld.seed++;
            TestSeed.foundItems.Clear();
        }
    }
}
