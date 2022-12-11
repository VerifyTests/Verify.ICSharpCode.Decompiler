using System.Text.RegularExpressions;
using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.Disassembler;
using VerifyTests.ICSharpCode.Decompiler;

namespace VerifyTests;

public static class VerifyICSharpCodeDecompiler
{
    static readonly Regex RvaScrubber = new(@"[ \t]+// Method begins at RVA 0x[0-9A-F]+\r?\n", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public static void Enable()
    {
        VerifierSettings.RegisterFileConverter<TypeToDisassemble>(ConvertTypeDefinitionHandle);
        VerifierSettings.RegisterFileConverter<MethodToDisassemble>(ConvertMethodDefinitionHandle);
        VerifierSettings.RegisterFileConverter<PropertyToDisassemble>(ConvertPropertyDefinitionHandle);
    }

    public static VerifySettings DontNormalizeIL(this VerifySettings settings)
    {
        settings.Context["VerifyICSharpCodeDecompiler.Normalize"] = false;
        return settings;
    }

    public static SettingsTask DontNormalizeIL(this SettingsTask settings)
    {
        settings.CurrentSettings.DontNormalizeIL();
        return settings;
    }

    static bool GetNormalizeIL(this IReadOnlyDictionary<string, object> context)
    {
        if (context.TryGetValue("VerifyICSharpCodeDecompiler.Normalize", out var value) &&
            value is bool result)
        {
            return result;
        }

        return true;
    }

    static ConversionResult ConvertTypeDefinitionHandle(TypeToDisassemble type, IReadOnlyDictionary<string, object> context) =>
        Convert(context, _ => _.DisassembleType(type.file, type.type));

    static ConversionResult ConvertPropertyDefinitionHandle(PropertyToDisassemble property, IReadOnlyDictionary<string, object> context) =>
        Convert(context, _ => _.DisassembleProperty(property.file, property.Property));

    static ConversionResult ConvertMethodDefinitionHandle(MethodToDisassemble method, IReadOnlyDictionary<string, object> context) =>
        Convert(context, _ => _.DisassembleMethod(method.file, method.method));

    static ConversionResult Convert(IReadOnlyDictionary<string, object> context, Action<ReflectionDisassemblerImport> action)
    {
        var output = new PlainTextOutput();
        var disassembler = new ReflectionDisassemblerImport(output, default);

        if (context.GetNormalizeIL())
        {
            disassembler.EntityProcessor = new SortByNameProcessor();
        }

        action(disassembler);

        var data = RvaScrubber.Replace(output.ToString(), string.Empty);

        return new(null, "txt", data);
    }
}