using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Paulov.Bepinex.Framework.Patches
{
    public abstract class NullPaulovHarmonyPatch : IPaulovHarmonyPatch
    {
        public abstract IEnumerable<MethodBase> GetMethodsToPatch();
        
        #region GetXMethod

        public virtual HarmonyMethod GetFinalizerMethod() => null;

        public virtual HarmonyMethod GetILManipulatorMethod() => null;

        public virtual HarmonyMethod GetPostfixMethod() => null;

        public virtual HarmonyMethod GetPrefixMethod() => null;

        public virtual HarmonyMethod GetTranspilerMethod() => null;

        #endregion
    }
}
