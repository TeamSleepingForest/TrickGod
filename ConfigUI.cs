using UnityEngine;


namespace TrickGod 
{

    public class ConfigUI : MonoBehaviour
    {
        public bool open = true;
        private Rect winRect = new(20, 20, 275, 180);


        public void OnGUI()
        {
            if (open)
            {
                winRect = GUI.Window(0, winRect, WinProc, $"{PluginInfo.PLUGIN_NAME} ({PluginInfo.PLUGIN_VERSION})");
            }
        }

        private void WinProc(int id)
        {
            var ox = 15f;
            var oy = 30f;
            var mx = winRect.width - 30;
            {
                var modeToggle = TrickHandler.Instance.enableTrickGod;
                var boostVal = TrickHandler.Instance.boostValue;
                GUI.Label(new(ox, oy, mx, 20), $"Trick God Mode: {(modeToggle ? "<color=green>On</color>" : "<color=red>Off</color>")}");
                oy += 20 + 5;

                GUI.Label(new(ox, oy, mx, 20), $"Boost Increment: {boostVal:F}");
                oy += 20 + 5;
            }

            oy += 10;  

            {
                if (GUI.Button(new(ox, oy, (mx / 2) - 5, 30), $"Add Boost (+5)"))
                {
                    TrickHandler.Instance.AddBoost(5f);
                }    

                if (GUI.Button(new(ox + 5 + (mx / 2), oy, (mx / 2) - 5, 30), "Sub Boost (-5)"))
                {
                    TrickHandler.Instance.SubBoost(5f);
                }   
            }

            oy += 10; 

            {
                oy += 20 + 5;
                if (GUI.Button(new(ox, oy, mx, 30), "Toggle Trick God (backslash)"))
                {
                    TrickHandler.Instance.ToggleTrickGod();
                }   
            }
            GUI.DragWindow();
        }   


        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Quote)) open = !open;
            if (Input.GetKeyDown(KeyCode.Backslash)) TrickHandler.Instance.ToggleTrickGod();
            if (Input.GetKeyDown(KeyCode.Equals) || Input.GetKeyDown(KeyCode.KeypadPlus)) TrickHandler.Instance.AddBoost(5);
            if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus)) TrickHandler.Instance.SubBoost(5);
        }            

    }

}