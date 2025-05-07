using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;

namespace Paulov.Bepinex.Framework;

public static class TranspilerHelper
{
    private static readonly IEnumerable<OpCode> FieldCodes = [OpCodes.Ldfld, OpCodes.Stfld];
    private static readonly IEnumerable<OpCode> MethodCodes = [OpCodes.Call, OpCodes.Callvirt];
    private static readonly IEnumerable<OpCode> BranchCodes =
        [OpCodes.Br, OpCodes.Brfalse, OpCodes.Brtrue, OpCodes.Brtrue_S, OpCodes.Brfalse_S, OpCodes.Br_S];

    public static CodeInstruction ParseCode(Code code)
    {
        CodeInstruction parsedInstruction = new CodeInstruction(code.OpCode)
        {
            labels = code.Label.HasValue ? [code.Label.Value] : []
        };

        object operand;

        if (!code.HasOperand) return parsedInstruction;

        //There is likely a better way to do this, but I can't think of one right now
        if (FieldCodes.Contains(code.OpCode))
        {
            operand = AccessTools.Field(code.CallerType, code.OperandTarget as string);
        }
        else if (code.Parameters?.Length > 0 || MethodCodes.Contains(code.OpCode))
        {
            operand = AccessTools.Method(code.CallerType, code.OperandTarget as string, code.Parameters);
        }
        else if (code.OpCode == OpCodes.Box)
        {
            operand = code.CallerType;
        }
        else if (BranchCodes.Contains(code.OpCode))
        {
            operand = code.OperandTarget;
        }
        else
        {
            //OpCode has operand but is of an unsupported type
            throw new ArgumentException($"Code with OpCode {code.OpCode} is not supported.");
        }

        parsedInstruction.operand = operand;
        return parsedInstruction;
    }
}

public class Code(
    OpCode opCode,
    object operandTarget = null,
    Type callerType = null,
    Type[] parameters = null,
    Label? label = null)
{
    public OpCode OpCode { get; } = opCode;
    public Type CallerType { get; } = callerType;
    public object OperandTarget { get; } = operandTarget;
    public Type[] Parameters { get; } = parameters;
    public Label? Label { get; } = label;
    public bool HasOperand => OperandTarget != null;
}
