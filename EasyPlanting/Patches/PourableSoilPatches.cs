using HarmonyLib;

#if IL2CPP
using Il2CppScheduleOne.ObjectScripts.Soil;
#elif MONO
using ScheduleOne;
using ScheduleOne.DevUtilities;
using ScheduleOne.Growing;
using ScheduleOne.ObjectScripts.Soil;
using ScheduleOne.PlayerTasks;
#endif

namespace EasyPlanting.Patches
{
    [HarmonyPatch(typeof(PourableSoil))]
    public class PourableSoilPatches
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(PourableSoil.PourAmount))]
        public static void Awake(PourableSoil __instance, ref float amount)
        {
            amount *= ModMain.config.SoilPourSpeedMultiplier.Value;
        }
    }
}
