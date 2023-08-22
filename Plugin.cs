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
// Find method that is called when a trick is completed and add a listener in this plugin
// Find method that is called when a multiplier is increased and add a listener in this plugin
// Find method that adds time decay to slides and find a way to disable it

namespace TrickGod
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInProcess("Bomb Rush Cyberfunk.exe")]
    public class Plugin : BaseUnityPlugin
    {

        internal volatile bool enableTrickGod;
        internal static ConfigEntry<KeyCode> toggleKey;
        internal static ConfigEntry<double> multiplierIncrement;// how much the mutlipler increases when a new trick is done (corner lean, vert, etc.)
        internal static ConfigEntry<double> trickIncrement;//how many points are awarded per trick

        internal static Plugin Instance { get; private set; } = null;
        internal static ManualLogSource Log { get; private set; } = null;

        private Core core;
        private WorldHandler world;
        private bool coreHasBeenSetup;

        private void Awake()
        {
            // Plugin startup logic
            Instance = this;
            Log = Logger;
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
            var harmony = new Harmony (PluginInfo.PLUGIN_GUID);
            harmony.PatchAll();

            enableTrickGod = false;
            multiplierIncrement = this.Config.Bind<double>("Sets", "MultiplierIncrement", 5.0);
            trickIncrement = this.Config.Bind<double>("Sets", "TrickIncrement", 1000.0);   
            toggleKey = this.Config.Bind<KeyCode>("KeyBinds", "ReloadLibrary", KeyCode.F1);
        }

        void Start()
        {
            Logger.LogInfo("init trick god");
        }

        void Update()
        {
            //wait for core and world setup
            if (!coreHasBeenSetup)
            {
                core = Core.Instance;
                if (core != null)
                {
                    world = WorldHandler.instance;
                    coreHasBeenSetup = world != null;
                }
            }

            if (coreHasBeenSetup)
            {
                var player = world.GetCurrentPlayer();

                //if player combo'ing?
                if ((player.IsComboing() || player.IsGrinding()) && enableTrickGod == true)
                {
                    Logger.LogInfo("BOOST TRICK SCORE - Start!");

                    //give boost stack
                    player.AddBoostCharge(500);

                    //give multiplier stack
                    player.AddScoreMultiplier();

                    Logger.LogInfo("BOOST TRICK SCORE - End!");
                }

                //toggle trick god
                if (Input.GetKeyDown(toggleKey.Value))
                {
                    if (enableTrickGod == true)
                    {
                        enableTrickGod = false;
                        Logger.LogInfo("TRICK GOD: OFF");
                    }
                    else
                    {
                        enableTrickGod = true;
                        Logger.LogInfo("TRICK GOD: ON");
                    }
                }
            }


        }
        
    }
}
