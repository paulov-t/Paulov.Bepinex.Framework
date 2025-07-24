using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using HarmonyLib;

namespace Paulov.Bepinex.Framework;

public class ReflectionPatchProvider : IPatchProvider
{
    public IEnumerable<IPaulovHarmonyPatch> GetPatches()
    {
        StackTrace stackTrace = new StackTrace();
        Assembly thisAssembly = GetType().Assembly;
        Assembly callingAssembly = null;
        for (int i = 0; i < stackTrace.FrameCount; i++)
        {
            MethodBase stackFrameMethod = stackTrace.GetFrame(i).GetMethod();
            Assembly stackFrameAssembly = stackFrameMethod?.DeclaringType?.Assembly;
            if (stackFrameAssembly == thisAssembly) continue;
            callingAssembly = stackFrameAssembly;
            break;
        }
        
        if(callingAssembly == null) throw new InvalidOperationException("Could not find calling assembly.");
        return callingAssembly.GetTypes()
            .OrderBy(x => x.Name)
            .Where(x => x.GetInterface(nameof(IPaulovHarmonyPatch)) != null)
            .Select(x => Activator.CreateInstance(x) as IPaulovHarmonyPatch);
    }
}