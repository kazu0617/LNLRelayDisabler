using FrooxEngine;
using HarmonyLib;
using NeosModLoader;
using System.Collections.Generic;

namespace NeosLNLRelayDisabler
{
    public class NeosLNLRelayDisabler : NeosMod
    {
        public override string Name => "NeosLNLRelayDisabler";
        public override string Author => "kazu0617";
        public override string Version => "1.0.0";
        public override string Link => "https://github.com/kazu0617/NeosLNLRelayDisabler/";
        public override void OnEngineInit()
        {
            Harmony harmony = new Harmony("net.kazu0617.NeosLNLRelayDisabler");

            harmony.PatchAll();
            Msg("Hooks installed successfully!");
        }

//        [HarmonyPatch(typeof(NetX.LNL_Manager), "GetSupportedSchemes")]
//        class NeosLNLRelayDisablerPatch
//        {
//            static bool Prefix(ref List<string> schemes)
//            {
//                schemes.Add("lnl");
//                return false; //try to skip original one
//            }
//        }

        [HarmonyPatch(typeof(NetX.LNL_Connection), "ConnectToRelay")]
        class NeosLNLRelayDisablerPatch2
        {
            static bool Prefix()
            {
                return false; //try to skip original one
            }
        }

    }
}