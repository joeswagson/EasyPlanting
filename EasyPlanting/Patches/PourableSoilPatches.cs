using HarmonyLib;
using ScheduleOne.ObjectScripts.Soil;
using System;
using System.Collections.Generic;
using System.Text;

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
