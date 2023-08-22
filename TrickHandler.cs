using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BepInEx.Logging;
using HarmonyLib;
using Reptile;
using BRCML;
using UnityEngine;

namespace TrickGod 
{

    public class TrickHandler : MonoBehaviour
    {
        public static TrickHandler Instance;

        public bool enableTrickGod = false;
        public float boostValue = 100; 

        private Core core;
        private WorldHandler world;
        private bool coreHasBeenSetup;
        private bool delegateHasBeenSetup = false;

        
        public TrickHandler()
        {
            Instance = this;
        }
        
        public void ToggleTrickGod()
        {
            if (enableTrickGod == true)
            {
                enableTrickGod = false;
                Plugin.Log.LogInfo("TRICK GOD: OFF");
            }
            else
            {
                enableTrickGod = true;
                Plugin.Log.LogInfo("TRICK GOD: ON");
            }                 
        }

        public void AddBoost(float amt)
        {
            boostValue += amt;
            boostValue = Mathf.Clamp(boostValue, 0f, 1000f);
        }

        public void SubBoost(float amt)
        {
            boostValue -= amt;
            boostValue = Mathf.Clamp(boostValue, 0f, 1000f);
        }        

        public void Update()
        {
            //wait for core and world setup
            if (!coreHasBeenSetup)
            {
                core = Core.Instance;
                if (core != null)
                {
                    world = WorldHandler.instance;
                    coreHasBeenSetup = world != null;

                    if (!delegateHasBeenSetup)
                    {
                        StageManager.OnStageInitialized += () =>
                        {
                            Plugin.Log.LogInfo("Swapped to new stage!");
                            coreHasBeenSetup = false;
                        };
                        delegateHasBeenSetup = true;
                    }
                }
            }

            if (coreHasBeenSetup)
            {
                var player = world.GetCurrentPlayer();

                //if player combo'ing?
                if ((player.IsComboing() || player.IsGrinding()) && enableTrickGod == true)
                {
                    //give boost stack
                    player.AddBoostCharge(boostValue);

                    //give multiplier stack
                    player.AddScoreMultiplier();
                }
            }

        }        
    }    

}