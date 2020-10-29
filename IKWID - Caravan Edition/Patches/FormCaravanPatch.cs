using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using JetBrains.Annotations;
using RimWorld;
using Verse;

namespace SirRandoo.ManualTradeSupplies.Patches
{
    [HarmonyPatch]
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public static class FormCaravanPatch
    {
        private static readonly FieldInfo CloseOnCancel = AccessTools.Field(typeof(Window), "closeOnCancel");
        private static readonly FieldInfo AutoSelectFoodAndMedicine = AccessTools.Field(typeof(Dialog_FormCaravan), "autoSelectFoodAndMedicine");
    
        public static IEnumerable<MethodBase> TargetMethods()
        {
            yield return AccessTools.Constructor(
                typeof(Dialog_FormCaravan),
                new[] {typeof(Map), typeof(bool), typeof(Action), typeof(bool)}
            );
        }

        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var marker = false;
            foreach (CodeInstruction instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Stfld && instruction.OperandIs(CloseOnCancel))
                {
                    marker = true;
                    yield return instruction;
                    continue;
                }

                if (instruction.opcode == OpCodes.Stfld && instruction.OperandIs(AutoSelectFoodAndMedicine))
                {
                    marker = false;
                    continue;
                }

                if (!marker)
                {
                    yield return instruction;
                }
            }
        }
    }
}
