using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuckSeedFinder
{
    internal class Reset
    {
        [HarmonyPatch(typeof(LoadingScreen), "FinishLoading")]
        [HarmonyPrefix]
        static void LeaveWorld()
        {
            GameManager.instance.LeaveGame();
            TestSeed.hasFoundSpear = false;
            CreateWorld.seed++;
        }
    }
}
