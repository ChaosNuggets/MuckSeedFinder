using HarmonyLib;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuckSeedFinder
{
    internal class TestSeed
    {
        public static bool hasFoundMeleeWeapon = false;
        public static List<Item> foundItems = new List<Item>();

        [HarmonyPatch(typeof(Chest), "InitChest")]
        [HarmonyPostfix]
        static void TestForWeapon(InventoryItem[] ___cells, Chest __instance)
        {
            foreach (InventoryItem item in ___cells)
            {
                if (item == null) continue;

                if (item.name == "Chiefs Spear")
                {
                    hasFoundMeleeWeapon = true;
                    foundItems.Add(new Item("chiefs spear", __instance.transform.position));
                }
                else if (item.name == "Ancient Bow")
                {
                    foundItems.Add(new Item("ancient bow", __instance.transform.position));
                }
                else if (item.name == "Night Blade")
                {
                    hasFoundMeleeWeapon = true;
                    foundItems.Add(new Item("night blade", __instance.transform.position));
                }
            }
        }

        [HarmonyPatch(typeof(Boat), "Start")]
        [HarmonyPrefix]
        static void FindBoatPosition(GameObject ___wheel)
        {
            foundItems.Add(new Item("boat", ___wheel.transform.position));
        }

        [HarmonyPatch(typeof(GuardianSpawner), "Start")]
        [HarmonyPostfix]
        static void FindGuardianPositions(List<GameObject> ___structures)
        {
            foreach (GameObject structure in ___structures)
            {
                foundItems.Add(new Item("guardian", structure.transform.position));
            }
        }

        [HarmonyPatch(typeof(GameManager), "SendPlayersIntoGame")]
        [HarmonyPrefix]
        static void FindSpawnPosition(List<Vector3> spawnPositions)
        {
            foundItems.Add(new Item("spawn", spawnPositions[0]));
        }
    }
}
