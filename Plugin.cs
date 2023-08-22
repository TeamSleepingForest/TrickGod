using System;
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
// Add UI Element

namespace TrickGod
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInProcess("Bomb Rush Cyberfunk.exe")]
    public class Plugin : BaseUnityPlugin
    {
        internal static Plugin Instance { get; private set; } = null;
        internal static ManualLogSource Log { get; private set; } = null;

        private GameObject _mod;
        private TrickHandler _tricks;

        private void Awake()
        {
            // Plugin startup logic
            Instance = this;
            Log = Logger;
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
            var harmony = new Harmony (PluginInfo.PLUGIN_GUID);
            harmony.PatchAll();

            _tricks = new();

            _mod = new();
            _mod.AddComponent<ConfigUI>();
            _mod.AddComponent<TrickHandler>();
            GameObject.DontDestroyOnLoad(_mod);            
        }

        void Start()
        {
            Logger.LogInfo("init trick god");
        }
        
    }
}
