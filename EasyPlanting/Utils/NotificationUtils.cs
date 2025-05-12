using MelonLoader;
using UnityEngine;

#if IL2CPP
using Il2CppScheduleOne.DevUtilities;
using Il2CppScheduleOne.UI;
#elif MONO
using ScheduleOne;
using ScheduleOne.DevUtilities;
using ScheduleOne.Growing;
using ScheduleOne.ItemFramework;
using ScheduleOne.ObjectScripts;
using ScheduleOne.ObjectScripts.Soil;
using ScheduleOne.PlayerTasks;
using ScheduleOne.PlayerTasks.Tasks;
using ScheduleOne.UI;
#endif

namespace EasyPlanting.Utils
{
    public class NotificationUtils
    {
        public static Sprite WarnIcon = Embedded.LoadSprite("IconWarn.png") ?? FallbackSprite();
        public static Sprite GoodIcon = Embedded.LoadSprite("IconGood.png") ?? FallbackSprite();
        public static Sprite BadIcon = Embedded.LoadSprite("IconBad.png") ?? FallbackSprite();

        #region FallbackSprite
        internal static Sprite _fallbackSprite = FallbackSprite();
        private static void fillTexture(Texture2D tex, Color c)
        {
            for (int x = 0; x < tex.width; x++)
            {
                for (int y = 0; y < tex.height; y++)
                {
                    tex.SetPixel(x, y, c);
                }
            }
        }
        public static Sprite FallbackSprite()
        {
            if (_fallbackSprite == null)
            {
                Texture2D tex = new Texture2D(2, 2);
                fillTexture(tex, new Color(255, 0, 255));
                _fallbackSprite = Sprite.Create(tex, new Rect(0, 0, 2, 2), Vector2.one / 2);
            }
            return _fallbackSprite;
        }
        #endregion

        public enum NotificationType
        {
            Good = 0,
            Warn = 1,
            Bad = 2
        }
        public static void SendNotification(string text, string header = "Notification", NotificationType urgency = NotificationType.Good, int duration=5, bool playSound=true)
        {
            Sprite icon = null!;
            switch (urgency)
            {
                case NotificationType.Good:
                    icon = GoodIcon;
                    break;
                case NotificationType.Warn:
                    icon = WarnIcon;
                    break;
                case NotificationType.Bad:
                    icon = BadIcon;
                    break;
            }

            if (Singleton<NotificationsManager>.InstanceExists)
            {
                Melon<ModMain>.Logger.Msg(icon == null ? icon : "null");
                Singleton<NotificationsManager>.Instance.SendNotification(header, text, icon, duration, playSound);
            }
            else
            {
                string uninitializedMessage = $"[NOTIFICATION (ingame manager not loaded)] \"{header}\" - \"{text}\"";
                switch (urgency) // double switch with same params is crazy
                {
                    case NotificationType.Good:
                        Melon<ModMain>.Logger.Msg(uninitializedMessage);
                        icon = _fallbackSprite;
                        break;
                    case NotificationType.Warn:
                        Melon<ModMain>.Logger.Warning(uninitializedMessage);
                        icon = _fallbackSprite;
                        break;
                    case NotificationType.Bad:
                        Melon<ModMain>.Logger.Error(uninitializedMessage);
                        break;
                }
            }
        }
    }
}
