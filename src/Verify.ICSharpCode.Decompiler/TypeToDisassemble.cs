using System.Reflection.Metadata;
using ICSharpCode.Decompiler.Metadata;

namespace VerifyTests.ICSharpCode.Decompiler;

public class TypeToDisassemble
{
    internal TypeDefinitionHandle type;
    internal PEFile file;

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