using HarmonyLib;

#if IL2CPP
using Il2CppScheduleOne.PlayerTasks;
#elif MONO
using ScheduleOne.PlayerTasks;
#endif

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
