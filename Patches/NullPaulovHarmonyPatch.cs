using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Paulov.Bepinex.Framework.Patches
{
    public class NullPaulovHarmonyPatch : IPaulovHarmonyPatch
    {
        public virtual HarmonyMethod GetFinalizerMethod()
        {
            return null;
        }

        public virtual HarmonyMethod GetILManipulatorMethod()
        {
            return null;
        }

        public virtual MethodBase GetMethodToPatch()
        {
            return null;
        }

        public virtual HarmonyMethod GetPostfixMethod()
        {
            return null;
        }

        public virtual HarmonyMethod GetPrefixMethod()
        {
            return null;
        }

        public virtual HarmonyMethod GetTranspilerMethod()
        {
            return null;
        }
    }
}
