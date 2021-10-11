using System.Reflection.Metadata;
using ICSharpCode.Decompiler.Metadata;
using VerifyTests.ICSharpCode.Decompiler;

namespace VerifyTests;

public class MethodToDisassemble
{
    internal MethodDefinitionHandle method;
    internal PEFile file;

    public MethodToDisassemble(PEFile file, MethodDefinitionHandle method)
    {
        this.method = method;
        this.file = file;
    }

    public MethodToDisassemble(PEFile file, string typeName, string methodName)
    {
        method = file.FindMethod(typeName,methodName);
        this.file = file;
    }
}