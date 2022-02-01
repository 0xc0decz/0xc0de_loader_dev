using HarmonyLib;
using System.Reflection;
using UnityEngine;
using _0xc0de_library;

namespace _0xc0de_MGT_Mod
{
    public class Main
    {
        static Main()
        {
            var harmony = new Harmony("com.0xc0de.MGT_Mod");
            
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        [HarmonyPatch]
        public static class Patch
        {
            [HarmonyPostfix]
            [HarmonyPatch(typeof(mainScript), "Update")]
            public static void Update()
            {
                if (Input.GetKey(KeyCode.F))
                {
                    Application.Quit();
                }
            }
        }
    }
}