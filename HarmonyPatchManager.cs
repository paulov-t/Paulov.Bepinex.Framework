using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using UnityEngine.Video;

namespace Paulov.Bepinex.Framework;

public class HarmonyPatchManager
{
    private readonly ManualLogSource logger;
    private readonly IPatchProvider _patchProvider;
    private List<Harmony> harmonyList = [];

    public HarmonyPatchManager(string managerName, IPatchProvider patchProvider = null)
    {
        logger = Logger.CreateLogSource(managerName ?? GetType().Name);
        _patchProvider = patchProvider;
    }

    public void EnablePatches()
    {
        foreach (IPaulovHarmonyPatch patch in _patchProvider.GetPatches())
        {
            try
            {
                Type patchType = patch.GetType();
                Harmony harmony = new Harmony(patchType.Name);
                if (Activator.CreateInstance(patchType) is not IPaulovHarmonyPatch obj || obj.GetMethodToPatch() == null)
                    continue;

                _ = harmony.Patch(
                    obj.GetMethodToPatch(),
                    obj.GetPrefixMethod(),
                    obj.GetPostfixMethod(),
                    obj.GetTranspilerMethod(),
                    obj.GetFinalizerMethod(), 
                    obj.GetILManipulatorMethod()
                );
                harmonyList.Add(harmony);
            }
            catch (Exception e)
            {
                logger.LogError(e);
            }
        }
        
        logger.LogDebug($"{nameof(HarmonyPatchManager)}: {harmonyList.Count} harmony patches applied");
    }

    public void DisablePatches()
    {
        foreach (Harmony harmony in harmonyList)
        {
            try
            {
                harmony.UnpatchSelf();
            }
            catch (Exception e)
            {
                logger.LogError($"Failed to unpatch {harmony.Id}.\n{e}");
            }
        }
        harmonyList.Clear();
    }

    

}
