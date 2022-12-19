namespace VerifyTests;

public static partial class VerifyICSharpCodeDecompiler
{
    static ConversionResult ConvertMethodDefinitionHandle(MethodToDisassemble method, IReadOnlyDictionary<string, object> context) =>
        Convert(context, _ => _.DisassembleMethod(method.File, method.Method));
}