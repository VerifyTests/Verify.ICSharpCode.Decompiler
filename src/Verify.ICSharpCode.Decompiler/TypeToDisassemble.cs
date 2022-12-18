namespace VerifyTests.ICSharpCode.Decompiler;

public class TypeToDisassemble
{
    internal readonly TypeDefinitionHandle type;
    internal readonly PEFile file;

    static TypeToDisassemble() => VerifyICSharpCodeDecompiler.Enable();

    public TypeToDisassemble(PEFile file, TypeDefinitionHandle type)
    {
        this.type = type;
        this.file = file;
    }

    public TypeToDisassemble(PEFile file, string typeName)
    {
        this.file = file;
        type = file.FindType(typeName);
    }
}