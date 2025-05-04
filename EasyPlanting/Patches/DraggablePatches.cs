using HarmonyLib;
using ScheduleOne.PlayerTasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPlanting.Patches
{
    [HarmonyPatch(typeof(Draggable))]
    public class DraggablePatches
    {
        [HarmonyPostfix]
        [HarmonyPatch("Awake")]
        public static void Awake(Draggable __instance)
        {
            __instance.DragForceMultiplier *= ModMain.config.DragSpeedMultiplier.Value;
            __instance.TorqueMultiplier *= ModMain.config.DragTurnSpeedMultiplier.Value;
        }
    }
}
