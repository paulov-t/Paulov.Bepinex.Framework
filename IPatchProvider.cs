using System.Collections.Generic;

namespace Paulov.Bepinex.Framework;

public interface IPatchProvider
{
    public IEnumerable<IPaulovHarmonyPatch> GetPatches();
}
