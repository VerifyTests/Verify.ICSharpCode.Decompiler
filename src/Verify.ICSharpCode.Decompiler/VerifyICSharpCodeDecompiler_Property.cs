namespace VerifyTests;

public static partial class VerifyICSharpCodeDecompiler
{
    static ConversionResult ConvertPropertyDefinitionHandle(PropertyToDisassemble property, IReadOnlyDictionary<string, object> context) =>
        Convert(context, _ => _.DisassembleProperty(property.File, property.Property));

}