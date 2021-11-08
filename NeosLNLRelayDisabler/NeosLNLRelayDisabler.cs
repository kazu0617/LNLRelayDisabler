using HarmonyLib;
using NeosModLoader;
using NetX;
using System.Net;

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

        [HarmonyPatch(typeof(LNL_Implementer), "RELAY_EP")]
        [HarmonyPatch(MethodType.Getter)]
        class NeosLNLRelayDisablerPatch2
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
