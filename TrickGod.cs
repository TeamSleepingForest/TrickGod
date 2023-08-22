// using System;
// using System.Runtime.Serialization;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using BepInEx.Logging;
// using HarmonyLib;
// using Reptile;
// using BRCML;

// namespace TrickGod 
// {

//     class PlayerPatch
//     {
//         ///Force Patch of new slide Decay System
//         [HarmonyPatch(typeof(Player))]
//         [HarmonyPrefix]
//         public static bool Prefix(Player __instance) 
//         {
//             Plugin.Log.LogInfo("PrefixPatch called");
//             return false; 
//         }
//     }

// }