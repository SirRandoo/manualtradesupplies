using System.Reflection;
using HarmonyLib;
using JetBrains.Annotations;
using Verse;

namespace SirRandoo.ManualTradeSupplies
{
    [UsedImplicitly]
    [StaticConstructorOnStartup]
    public static class Patcher
    {
        static Patcher()
        {
            new Harmony("com.sirrandoo.manualtradesupplies").PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}
