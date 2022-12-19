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