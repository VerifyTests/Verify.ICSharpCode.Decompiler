namespace VerifyTests;

public static partial class VerifyICSharpCodeDecompiler
{
    static void ConvertAssembly(PEFile module, AssemblyOptions options, ReflectionDisassembler disassembler, ITextOutput output)
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

    static ConversionResult ConvertAssembly(PEFile target, IReadOnlyDictionary<string, object> context) =>
        Convert(context, (disassembler, output) => ConvertAssembly(target, AssemblyOptions.IncludeModuleContents, disassembler, output));

    static ConversionResult ConvertAssembly(AssemblyToDisassemble target, IReadOnlyDictionary<string, object> context) =>
        Convert(context, (disassembler, output) => ConvertAssembly(target.File, target.Options, disassembler, output));
}