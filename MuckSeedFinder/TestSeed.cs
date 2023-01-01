using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace MuckSeedFinder
{
    internal class TestSeed
    {
        public static bool hasFoundSpear = false;

        [HarmonyPatch(typeof(Chest), "InitChest")]
        [HarmonyPostfix]
        static void TestForSpear(List<InventoryItem> items)
        {
            if (hasFoundSpear) return;

            foreach (InventoryItem item in items)
            {
                if (item.name == "Chiefs Spear")
                {
                    FileLog.Log(CreateWorld.seed.ToString());
                    hasFoundSpear = true;
                    break;
                }
            }
        }
    }
}
