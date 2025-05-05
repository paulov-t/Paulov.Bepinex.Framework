using BepInEx;
using BepInEx.Logging;

namespace Paulov.Bepinex.Framework;

[BepInPlugin("Paulov.Bepinex.Framework", "Paulov.Bepinex.Framework", "2025.5.2")]
public class Plugin : BaseUnityPlugin
{
    internal static new ManualLogSource Logger;

    private void Awake()
    {
        Logger = base.Logger;
    }
}
