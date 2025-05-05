# Paulov's Bepinex Framework

<div align="center">
    
<strong>A small library that can be used to quicky create BepInEx 5 plugins for Unity</strong>

  ![NuGet Version](https://img.shields.io/nuget/v/Paulov.Bepinex.Framework?style=for-the-badge)

</div>

## About The Project
This project is designed to allow developers to quickly create BepInEx 5 plugins for Unity games. It provides a simple way to create a plugin that can be easily loaded into the game.

## Installation
- Use the NuGet package manager to install the package

## Usage
- Ensure that Paulov.Bepinex.Framework.dll is in the game's BepinEx/plugins folder
- In your Plugin class, `Awake` method, add the following code to load the Framework.
```
    var assembly = Assembly.LoadFile(Path.Combine(ReflectionHelpers.GetBaseDirectory(), "BepInEx", "plugins", "Paulov.Bepinex.Framework.dll"));
    if (Assembly.UnsafeLoadFrom(assembly.Location) != null)
        Logger.LogInfo("Loaded Paulov.Bepinex.Framework.dll");
```
- Create a new class that inherits from `IPaulovHarmonyPatch` or `NullPaulovHarmonyPatch`
- Patch your desired method using the correct method (i.e. Prefix, Postfix, etc.)
- In your Plugin class, `Awake` method, Instantiate the ``HarmonyPatchManager`` class and call the `EnablePatches` method with the class you created
```
    var harmonyPatchManager = new Paulov.Bepinex.Framework.HarmonyPatchManager("Paulov's Main Harmony Manager");
    harmonyPatchManager.EnablePatches();
```


**To learn more about patching using Harmony, visit the [Harmony Wiki](https://harmony.pardeike.net/articles/patching.html)**

## License
Distributed under the Creative Commons Attribution-NonCommercial-NoDerivatives 4.0 International Public License. See [LICENSE](LICENSE.md) for more information.
