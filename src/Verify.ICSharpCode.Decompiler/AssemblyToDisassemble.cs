namespace VerifyTests;

public class AssemblyToDisassemble
{
    internal readonly PEFile File;
    internal readonly AssemblyOptions Options;

    public AssemblyToDisassemble(PEFile file, AssemblyOptions options = AssemblyOptions.IncludeModuleContents)
    {
        File = file;
        Options = options;
    }
}
