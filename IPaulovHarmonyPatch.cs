using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;

namespace Paulov.Bepinex.Framework
{
    public interface IPaulovHarmonyPatch
    {
        IEnumerable<MethodBase> GetMethodsToPatch();
        HarmonyMethod GetPrefixMethod();
        HarmonyMethod GetPostfixMethod();
        HarmonyMethod GetTranspilerMethod();
        HarmonyMethod GetFinalizerMethod();
        HarmonyMethod GetILManipulatorMethod();
    }
}
