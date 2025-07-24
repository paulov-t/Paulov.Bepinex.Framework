# Paulov's Bepinex Framework

A small library that can be used to quicky create BepInEx 5 plugins for Unity

[![Nuget][nuget-shield]][nuget-url]
[![Contributors][contributors-shield]][contributors-url]

## About The Project
This project is designed to allow developers to quickly create BepInEx 5 plugins for Unity games. It provides a simple way to create a plugin that can be easily loaded into the game.

## Installation
- Use the NuGet package manager to install the [NuGet Package](https://www.nuget.org/packages/Paulov.Bepinex.Framework) into your project `dotnet add package Paulov.Bepinex.Framework`

## Usage
- Ensure that Paulov.Bepinex.Framework.dll is in the game's BepinEx/plugins folder
- Create a new class that inherits from `IPaulovHarmonyPatch` or `NullPaulovHarmonyPatch`
- Patch your desired method using the correct method (i.e. Prefix, Postfix, etc.)
- Create a `PatchProvider` class that inherits `IPatchProvider` like so:
```
    public class LocalPatchProvider : IPatchProvider
    {
        public IEnumerable<IPaulovHarmonyPatch> GetPatches()
        {
            IOrderedEnumerable<Type> assemblyTypes = GetType().Assembly.GetTypes().OrderBy(x => x.Name);
            foreach (Type type in assemblyTypes)
            {
                if(type.GetInterface(nameof(IPaulovHarmonyPatch)) is null) continue;
                yield return (IPaulovHarmonyPatch)Activator.CreateInstance(type);
            }
        }
    }
```
- In your Plugin class, `Awake` method, Instantiate the ``HarmonyPatchManager`` class and call the `EnablePatches` method with the class you created
```
    var harmonyPatchManager = new Paulov.Bepinex.Framework.HarmonyPatchManager("Paulov's Main Harmony Manager");
    harmonyPatchManager.EnablePatches();
```

## Learn about Harmony Patching

To learn more about patching using Harmony, visit the [Harmony Wiki](https://harmony.pardeike.net/articles/patching.html)

## License

Distributed under the Creative Commons Attribution-NonCommercial-NoDerivatives 4.0 International Public License. See [LICENSE](LICENSE.md) for more information.


<!-- MARKDOWN LINKS & IMAGES -->
[nuget-shield]: https://img.shields.io/nuget/v/Paulov.Bepinex.Framework?style=for-the-badge

[nuget-url]: https://www.nuget.org/packages/Paulov.Bepinex.Framework

[contributors-shield]: https://img.shields.io/github/contributors/paulov-t/Paulov.Bepinex.Framework.svg?style=for-the-badge

[contributors-url]: https://github.com/paulov-t/Paulov.Bepinex.Framework/graphs/contributors

