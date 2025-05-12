using EasyPlanting;
using EasyPlanting.Product;
using MelonLoader;
using UnityEngine;

#if IL2CPP
using Il2CppScheduleOne;
using Il2CppScheduleOne.DevUtilities;
using Il2CppScheduleOne.Growing;
using Il2CppScheduleOne.PlayerTasks;
#elif MONO
using ScheduleOne;
using ScheduleOne.DevUtilities;
using ScheduleOne.Growing;
using ScheduleOne.PlayerTasks;
#endif


[assembly: MelonInfo(typeof(ModMain), BuildInfo.ProductName, BuildInfo.ProductVersion, BuildInfo.ProductAuthor, BuildInfo.ProductLink)]
namespace EasyPlanting
{
    public class ModMain : MelonMod
    {
#pragma warning disable CS8618
        public static Config config;
#pragma warning restore CS8618

        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg("Loading config...");
            config = new Config();
            LoggerInstance.Msg("Loaded!");
        }

        public override void OnDeinitializeMelon()
        {
            config.Category.SaveToFile();
        }

        // been FOREVER since ive touched OnUpdate. damn.
        public override void OnUpdate()
        {
            if (!GameInput.GetButton(GameInput.ButtonCode.PrimaryClick) || !Singleton<TaskManager>.InstanceExists || Singleton<TaskManager>.Instance.currentTask == null)
                return;

            if (config.DragOverSoilChunks.Value && Singleton<TaskManager>.Instance.currentTask is SowSeedTask task && task.seedReachedDestination)
            {
                Clickable? clickable = task.GetClickable(out RaycastHit hit);
                if (clickable != null && clickable is SoilChunk chunk)
                {
                    chunk.StartClick(hit);
                    chunk.EndClick();
                }
            }
        }
    }
}
