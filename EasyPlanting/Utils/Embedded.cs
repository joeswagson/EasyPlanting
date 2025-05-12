using MelonLoader;
using System.Reflection;
using UnityEngine;

namespace EasyPlanting.Utils
{
    public class Embedded
    {
        public static string ResourcePath = "EasyPlanting.Resources";
        public static string ResolveResource(string name)
        {
            return $"{ResourcePath}.{name}";
        }
        
        public static byte[]? LoadDataFromResource(Assembly target, string identifier)
        {
            Melon<ModMain>.Logger.Msg($"Loading resource \"{identifier}\"");
            if (!target.GetManifestResourceNames().Contains(identifier))
            {
                Melon<ModMain>.Logger.Error($"Resource \"{identifier}\" Not Found.");
                return null;
            }

            using (Stream stream = target.GetManifestResourceStream(identifier))
            {
                if (stream == null)
                {
                    Melon<ModMain>.Logger.Error($"Resource \"{identifier}\" Unavailable.");
                    return null;
                }

                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                Melon<ModMain>.Logger.Msg($"Read {stream.Length} bytes from \"{identifier}\".");
                return buffer;
            }
        }

        public static Sprite? LoadSprite(string resource)
        {
            byte[]? pngData = LoadDataFromResource(Melon<ModMain>.Instance.MelonAssembly.Assembly, ResolveResource(resource));

            if(pngData == null)
            {
                Melon<ModMain>.Logger.Error($"Failed to load Sprite \"{resource}\".");
                return null;
            }

            Texture2D texture = new Texture2D(2, 2);
            texture.name = Path.GetFileNameWithoutExtension(resource);
            texture.LoadImage(pngData, false);

            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one / 2);

            //UnityEngine.Object.Destroy(texture);

            return sprite;
        }
    }
}
