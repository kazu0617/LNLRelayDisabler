using HarmonyLib;
using NeosModLoader;
using NetX;
using System.Net;

namespace LNLRelayDisabler
{
    public class LNLRelayDisabler : NeosMod
    {
        public override string Name => "LNLRelayDisabler";
        public override string Author => "kazu0617";
        public override string Version => "1.0.1";
        public override string Link => "https://github.com/kazu0617/LNLRelayDisabler/";
        public override void OnEngineInit()
        {
            Harmony harmony = new Harmony("net.kazu0617.LNLRelayDisabler");
            harmony.PatchAll();

            Debug("Hooks installed successfully!");
        }

        [HarmonyPatch(typeof(LNL_Implementer), "RELAY_EP")]
        [HarmonyPatch(MethodType.Getter)]
        class Patch
        {
            static bool Prefix(ref IPEndPoint __result)
            {
                Debug("Nulled RELAY_EP");
                __result = null;
                return true; // skip the original method
            }
        }
    }
}
