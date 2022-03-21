using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.Disassembler;
using VerifyTests.ICSharpCode.Decompiler;

namespace VerifyTests;

public static class VerifyICSharpCodeDecompiler
{
    public static void Enable()
    {
        VerifierSettings.RegisterFileConverter<TypeToDisassemble>(ConvertTypeDefinitionHandle);
        VerifierSettings.RegisterFileConverter<MethodToDisassemble>(ConvertMethodDefinitionHandle);
        VerifierSettings.RegisterFileConverter<PropertyToDisassemble>(ConvertPropertyDefinitionHandle);
    }

    static ConversionResult ConvertTypeDefinitionHandle(TypeToDisassemble type, IReadOnlyDictionary<string, object> _)
    {
        PlainTextOutput output = new();
        ReflectionDisassembler disassembler = new(output, default);
        disassembler.DisassembleType(type.file, type.type);
        return ConversionResult(output);
    }

    static ConversionResult ConvertPropertyDefinitionHandle(PropertyToDisassemble property, IReadOnlyDictionary<string, object> _)
    {
        PlainTextOutput output = new();
        ReflectionDisassembler disassembler = new(output, default);
        disassembler.DisassembleProperty(property.file, property.Property);
        return ConversionResult(output);
    }

    static ConversionResult ConvertMethodDefinitionHandle(MethodToDisassemble method, IReadOnlyDictionary<string, object> _)
    {
        PlainTextOutput output = new();
        ReflectionDisassembler disassembler = new(output, default);
        disassembler.DisassembleMethod(method.file, method.method);
        return ConversionResult(output);
    }

    static ConversionResult ConversionResult(PlainTextOutput output) =>
        new(null,"txt", output.ToString());
}