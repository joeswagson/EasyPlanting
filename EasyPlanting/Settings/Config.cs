using EasyPlanting.Utils;
using MelonLoader;
using System.Reflection;

namespace EasyPlanting
{
    public class Config
    {
        public MelonPreferences_Category Category;



        public MelonPreferences_Entry<bool> DragOverSoilChunks;

        [ReloadRequired]
        public MelonPreferences_Entry<float> DragSpeedMultiplier;

        [ReloadRequired]
        public MelonPreferences_Entry<float> DragTurnSpeedMultiplier;

        public MelonPreferences_Entry<float> SoilPourSpeedMultiplier;

        [ReloadRequired]
        public MelonPreferences_Entry<float> WaterTargetSizeMultiplier;
        [ReloadRequired]
        public MelonPreferences_Entry<float> WaterTargetHoverTimeDamper;

        public MelonPreferences_Entry<float> BudSizeMultiplier;

        public Config()
        {
            Category = MelonPreferences.CreateCategory("EasyPlanting");

            DragOverSoilChunks = Category.CreateEntry("DragOverSoilChunks", true);
            DragSpeedMultiplier = Category.CreateEntry("DragSpeedMultiplier", 1.5f);
            DragTurnSpeedMultiplier = Category.CreateEntry("DragTurnSpeedMultiplier", 1.5f);
            SoilPourSpeedMultiplier = Category.CreateEntry("SoilPourSpeedMultiplier", 4f);
            WaterTargetSizeMultiplier = Category.CreateEntry("WaterTargetSizeMultiplier", 1.5f);
            WaterTargetHoverTimeDamper = Category.CreateEntry("WaterTargetHoverTimeDamper", 1.5f);
            BudSizeMultiplier = Category.CreateEntry("BudSizeMultiplier", 2f);

            foreach(FieldInfo field in GetType().GetFields())
                if (field.FieldType.Name.StartsWith("MelonPreferences_Entry") && field.CustomAttributes.Where(x => x.AttributeType == typeof(ReloadRequiredAttribute)).Count() > 0)
                    ((MelonPreferences_Entry)field.GetValue(this)).OnEntryValueChangedUntyped.Subscribe((oldValue, newValue) => {
                        NotificationUtils.SendNotification("Reload save to apply.", "EasyPlanting", NotificationUtils.NotificationType.Warn);
                    });
        }
    }
}
