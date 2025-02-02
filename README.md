<div align="center">
<h3 align="center">Paulov's Bepinex Framework</h3>

  <p align="center">
	A small library that can be used to quicky create BepInEx 5 plugins for Unity
	<br />
  </p>

</div>

## About The Project
This project is designed to allow developers to quickly create BepInEx 5 plugins for Unity games. It provides a simple way to create a plugin that can be easily loaded into the game.

## Installation
- Use the NuGet package manager to install the package

## Usage
- Create a new class that inherits from `IPaulovHarmonyPatch`
- Patch your desired method using the correct method (i.e. Prefix, Postfix, etc.)
- Instantiate the ``HarmonyPatchManager`` class and call the `EnablePatches` method with the class you created

**To learn more about patching using Harmony, visit the [Harmony Wiki](https://harmony.pardeike.net/articles/patching.html)**

## License
Distributed under the Creative Commons Attribution-NonCommercial-NoDerivatives 4.0 International Public License. See [LICENSE](LICENSE.md) for more information.