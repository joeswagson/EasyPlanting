using HarmonyLib;
using ScheduleOne.ItemFramework;
using ScheduleOne.ObjectScripts;
using ScheduleOne.ObjectScripts.Soil;
using ScheduleOne.PlayerTasks;
using ScheduleOne.PlayerTasks.Tasks;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EasyPlanting.Patches
{
    [HarmonyPatch(typeof(PourWaterTask))]
    public class PourWaterTaskPatches
    {
        //public static MethodBase TargetMethod()
        //{
        //    return AccessTools.Constructor(typeof(PourWaterTask));
        //}

        [HarmonyPatch(MethodType.Constructor, new[] { typeof(Pot), typeof(ItemInstance), typeof(Pourable) })]
        [HarmonyPostfix]
        public static void ctor(PourWaterTask __instance, Pot _pot, ItemInstance _itemInstance, Pourable _pourablePrefab)
        {
            __instance.SUCCESS_THRESHOLD *= ModMain.config.WaterTargetSizeMultiplier.Value;
            __instance.SUCCESS_TIME /= ModMain.config.WaterTargetHoverTimeDamper.Value;

            __instance.pourable.DragForceMultiplier *= ModMain.config.DragSpeedMultiplier.Value;
            __instance.pourable.TorqueMultiplier *= ModMain.config.DragTurnSpeedMultiplier.Value;
        }
    }
}
