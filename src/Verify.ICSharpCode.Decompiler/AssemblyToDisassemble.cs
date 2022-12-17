using ICSharpCode.Decompiler.Metadata;

namespace VerifyTests;

[Flags]
public enum AssemblyOptions
{
    None = 0,
    IncludeAssemblyReferences = 1,
    IncludeAssemblyHeader = 2,
    IncludeModuleHeader = 4,
    IncludeModuleContents = 8,
    Full = 15
}

public class AssemblyToDisassemble
{
    internal readonly PEFile file;
    internal readonly AssemblyOptions options;

    static AssemblyToDisassemble() => VerifyICSharpCodeDecompiler.Enable();

    public AssemblyToDisassemble(PEFile file, AssemblyOptions options = AssemblyOptions.IncludeModuleContents)
    {
        this.file = file;
        this.options = options;
    }
}
