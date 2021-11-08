using FrooxEngine;
using HarmonyLib;
using NeosModLoader;
using NetX;
using System.Collections.Generic;
using System.Reflection;

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

        [HarmonyPatch(typeof(LNL_Connection), "ConnectToRelay")]
        class NeosLNLRelayDisablerPatch2
        {
            static bool Prefix(LNL_Connection __instance)
            {
                MethodInfo setFailReason = AccessTools.DeclaredPropertySetter(typeof(LNL_Connection), "FailReason");
                MethodInfo setConnectionFailed = AccessTools.DeclaredPropertySetter(typeof(LNL_Connection), "ConnectionFailed");

                if (setFailReason != null && setConnectionFailed != null)
                {
                    // this.FailReason = "World.Error.FailedConnectToRelay";
                    setFailReason.Invoke(__instance, new object[] { "World.Error.FailedConnectToRelay" });

                    // this.ConnectionFailed(this);
                    setConnectionFailed.Invoke(__instance, new object[] { __instance });

                    // skip original method
                    Debug("Skipping LNL Relay");
                    return false;
                }
                else
                {
                    Error("Could not invoke FailReason or ConnectionFailed setters!");
                    return true;
                }
            }
        }

    }
}