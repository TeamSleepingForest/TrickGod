﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using Reptile;
using BRCML;


//This mod allows you to toggle what's called trick god mode. In this mode:
// - Manuals/Slides don't have any time decay
// - Tricks can be modified to give increased points [check config]
// - Multipliers can be modified to increase in different increments [check config]

//TODO:
// Find method that is called when a trick is completed and add a listener in this plugin
// Find method that is called when a multiplier is increased and add a listener in this plugin
// Find method that adds time decay to slides and find a way to disable it

namespace TrickGod
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInDependency(BRCML.PluginInfos.PLUGIN_ID, BepInDependency.DependencyFlags.HardDependency)]
    [BepInProcess("Bomb Rush Cyberfunk.exe")]
    public class Plugin : BaseUnityPlugin
    {
        // private static bool trickGod = true;
        // private static float multiplierIncrement = 5; // how much the mutlipler increases when a new trick is done (corner lean, vert, etc.)
        // private static float trickIncrement = 1000; //how many points are awarded per trick
        internal static Plugin Instance { get; private set; } = null;
        internal static ManualLogSource Log { get; private set; } = null;

        private void Awake()
        {
            // Plugin startup logic
            Instance = this;
            Log = Logger;
            Logger.LogInfo("Trick God is loaded!!!");
            var harmony = new Harmony ("io.teamsleepingforest.trickgod");
            harmony.PatchAll();
        }
    }
}