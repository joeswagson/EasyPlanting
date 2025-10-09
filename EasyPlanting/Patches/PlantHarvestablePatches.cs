using HarmonyLib;
using MelonLoader;
using UnityEngine;

#if IL2CPP
using Il2CppScheduleOne.Growing;
#elif MONO
using ScheduleOne.Growing;
#endif

namespace EasyPlanting.Patches
{
    [HarmonyPatch(typeof(PlantHarvestable))]
    public class PlantHarvestablePatches
    {
        [HarmonyPostfix]
        [HarmonyPatch(MethodType.Constructor)]
        public static void ctor(PlantHarvestable __instance)
        {
            CapsuleCollider capsule = __instance.gameObject.GetComponentInChildren<CapsuleCollider>(true);
            SphereCollider sphere = __instance.gameObject.GetComponentInChildren<SphereCollider>(true);
            Melon<ModMain>.Logger.Msg(capsule);
            capsule.height *= ModMain.config.BudSizeMultiplier.Value * 2;
            capsule.radius *= ModMain.config.BudSizeMultiplier.Value * 2;
        }
    }
}
