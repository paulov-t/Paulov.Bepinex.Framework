using System;
using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;

namespace Paulov.Bepinex.Framework.Patches
{
    public abstract class NullPaulovHarmonyPatch : IPaulovHarmonyPatch
    {
        public virtual IEnumerable<MethodBase> GetMethodsToPatch()
        {
            MethodBase method = GetMethodToPatch();
            if (method == null) yield break;
            yield return method;
        }

        public virtual MethodBase GetMethodToPatch() => null;

        #region GetXMethod

        public virtual HarmonyMethod GetFinalizerMethod() =>
            GetHarmonyMethodFromDerivedClass(HarmonyNameFromMethodName(nameof(GetFinalizerMethod)));

        public virtual HarmonyMethod GetILManipulatorMethod() =>
            GetHarmonyMethodFromDerivedClass(HarmonyNameFromMethodName(nameof(GetILManipulatorMethod)));

        public virtual HarmonyMethod GetPostfixMethod() =>
            GetHarmonyMethodFromDerivedClass(HarmonyNameFromMethodName(nameof(GetPostfixMethod)));

        public virtual HarmonyMethod GetPrefixMethod() =>
            GetHarmonyMethodFromDerivedClass(HarmonyNameFromMethodName(nameof(GetPrefixMethod)));

        public virtual HarmonyMethod GetTranspilerMethod() =>
            GetHarmonyMethodFromDerivedClass(HarmonyNameFromMethodName(nameof(GetTranspilerMethod)));

        #endregion

        protected HarmonyMethod GetHarmonyMethodFromDerivedClass(string methodName)
        {
            if (GetType() == typeof(NullPaulovHarmonyPatch))
                throw new InvalidOperationException("This method should only be called from a derived type.");
            MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            return method == null ? null : new HarmonyMethod(method);
        }

        private string HarmonyNameFromMethodName(string methodName)
        {
            const string prefixToTrim = "Get";
            if (!methodName.StartsWith(prefixToTrim, StringComparison.InvariantCultureIgnoreCase))
                throw new InvalidOperationException("Method name must start with 'Get'.");
            return methodName[prefixToTrim.Length..];
        }
    }
}