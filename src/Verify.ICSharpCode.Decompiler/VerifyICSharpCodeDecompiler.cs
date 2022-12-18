using System.Text.RegularExpressions;
using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.Disassembler;
using ICSharpCode.Decompiler.Metadata;
using VerifyTests.ICSharpCode.Decompiler;

namespace VerifyTests;

public static class VerifyICSharpCodeDecompiler
{
    static readonly Regex RvaScrubber = new(@"[ \t]+// Method begins at RVA 0x[0-9A-F]+\r?\n", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    static readonly Regex BinaryDataExpression = new(@"^[ \t]+[0-9A-F]{2}( [0-9A-F]{2})*", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    static VerifyICSharpCodeDecompiler()
    {
        VerifierSettings.RegisterFileConverter<TypeToDisassemble>(ConvertTypeDefinitionHandle);
        VerifierSettings.RegisterFileConverter<MethodToDisassemble>(ConvertMethodDefinitionHandle);
        VerifierSettings.RegisterFileConverter<PropertyToDisassemble>(ConvertPropertyDefinitionHandle);
        VerifierSettings.RegisterFileConverter<AssemblyToDisassemble>(ConvertAssembly);
    }

    public static void Enable()
    {
    }

    public static VerifySettings ScrubBinaryData(this VerifySettings settings)
    {
        settings.ScrubLines(line => BinaryDataExpression.IsMatch(line));
        return settings;
    }

    public static SettingsTask ScrubBinaryData(this SettingsTask settings)
    {
        settings.CurrentSettings.ScrubBinaryData();
        return settings;
    }

    public static VerifySettings ScrubComments(this VerifySettings settings)
    {
        settings.ScrubLines(line =>
        {
            var commentStart = line.IndexOf("//", StringComparison.Ordinal);

            return commentStart == 0 || (commentStart > 0 && line.Take(commentStart).All(char.IsWhiteSpace));
        }, ScrubberLocation.Last);

        settings.ScrubLinesWithReplace(line =>
        {
            var commentStart = line.IndexOf("//", StringComparison.Ordinal);
            return commentStart < 0 ? line : line.Substring(0, commentStart).TrimEnd();
        }, ScrubberLocation.Last);

        return settings;
    }

    public static SettingsTask ScrubComments(this SettingsTask settings)
    {
        settings.CurrentSettings.ScrubComments();
        return settings;
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

    static void ConvertAssembly(PEFile module, AssemblyOptions options, ReflectionDisassemblerImport disassembler, ITextOutput output)
    {
        if ((options & AssemblyOptions.IncludeAssemblyReferences) != 0)
        {
            disassembler.WriteAssemblyReferences(module.Metadata);
            output.WriteLine();
        }

        if ((options & AssemblyOptions.IncludeAssemblyHeader) != 0)
        {
            disassembler.WriteAssemblyHeader(module);
            output.WriteLine();
        }

        if ((options & AssemblyOptions.IncludeModuleHeader) != 0)
        {
            disassembler.WriteModuleHeader(module);
            output.WriteLine();
        }

        if ((options & AssemblyOptions.IncludeModuleContents) != 0)
        {
            disassembler.WriteModuleContents(module);
        }
    }

    static ConversionResult ConvertTypeDefinitionHandle(TypeToDisassemble type, IReadOnlyDictionary<string, object> context) =>
        Convert(context, _ => _.DisassembleType(type.file, type.type));

    static ConversionResult ConvertPropertyDefinitionHandle(PropertyToDisassemble property, IReadOnlyDictionary<string, object> context) =>
        Convert(context, _ => _.DisassembleProperty(property.file, property.Property));

    static ConversionResult ConvertMethodDefinitionHandle(MethodToDisassemble method, IReadOnlyDictionary<string, object> context) =>
        Convert(context, _ => _.DisassembleMethod(method.file, method.method));

    static ConversionResult ConvertAssembly(AssemblyToDisassemble target, IReadOnlyDictionary<string, object> context) =>
        Convert(context, (disassembler, output) => ConvertAssembly(target.file, target.options, disassembler, output));

    static ConversionResult Convert(IReadOnlyDictionary<string, object> context, Action<ReflectionDisassemblerImport> action) =>
        Convert(context, (disassembler, _) => action(disassembler));

    static ConversionResult Convert(IReadOnlyDictionary<string, object> context, Action<ReflectionDisassemblerImport, ITextOutput> action)
    {
        var output = new PlainTextOutput();
        var disassembler = new ReflectionDisassemblerImport(output, default);

        if (context.GetNormalizeIL())
        {
            disassembler.EntityProcessor = new SortByNameProcessor();
        }

        action(disassembler, output);

        var data = RvaScrubber.Replace(output.ToString(), string.Empty);
 
        return new(null, "txt", data);
    }
}