using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Linq;
using System.Reflection;

namespace Paulov.Bepinex.Framework;

public class HarmonyPatchManager
{
    private readonly ManualLogSource _logger;
    private readonly IPatchProvider _patchProvider;
    private readonly Harmony _harmony = new("Paulov.Bepinex.Framework");

    public HarmonyPatchManager(string managerName, IPatchProvider patchProvider = null)
    {
        _logger = Logger.CreateLogSource(managerName ?? GetType().Name);
        _patchProvider = patchProvider ?? new ReflectionPatchProvider();
    }

    public int EnableAll()
    {
        if (_patchProvider != null) return _patchProvider.GetPatches().Sum(ApplyPatches);
        
        _logger.LogError($"{nameof(HarmonyPatchManager)}: No patch provider set. Patches will not be applied.");
        return 0;

    }

    public int DisableAll()
    {
        int patchesRemoved = 0;
        foreach (MethodBase patch in _harmony.GetPatchedMethods())
        {
            try
            {
                _harmony.Unpatch(patch, HarmonyPatchType.All);
                patchesRemoved++;
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to unpatch {patch.Name}: {e}");
            }
        }
        
        return patchesRemoved;
    }

    private int ApplyPatches(IPaulovHarmonyPatch patch)
    {
        int patchesApplied = 0;

        foreach (MethodBase method in patch.GetMethodsToPatch())
        {
            _ = _harmony.Patch(method, patch.GetPrefixMethod(), patch.GetPostfixMethod(),
                patch.GetTranspilerMethod(), patch.GetFinalizerMethod(), patch.GetILManipulatorMethod());
            patchesApplied++;
        }
        
        return patchesApplied;
    }
}
