using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace MuckSeedFinder
{
    [BepInPlugin("me.chaosnuggets.muckseedfinder", "MuckSeedFinder", "1.0.0.0")]
    public class MainClass : BaseUnityPlugin
    {
        public static MainClass instance;
        public Harmony harmony;
        public ManualLogSource log;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }

            log = Logger;
            harmony = new Harmony("me.chaosnuggets.muckseedfinder");

            harmony.PatchAll(typeof(CreateWorld));
            harmony.PatchAll(typeof(TestSeed));
            harmony.PatchAll(typeof(Reset));

            log.LogInfo("Muck Seed Finder: Mod loaded");
        }

        private void OnDestroy()
        {
            harmony.UnpatchSelf();
        }
    }
}