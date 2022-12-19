namespace VerifyTests;

public static partial class VerifyICSharpCodeDecompiler
{
    static ConversionResult ConvertTypeDefinitionHandle(TypeToDisassemble type, IReadOnlyDictionary<string, object> context) =>
        Convert(context, _ => _.DisassembleType(type.file, type.type));
}