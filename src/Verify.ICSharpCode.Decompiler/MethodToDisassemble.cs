using System.Reflection.Metadata;
using ICSharpCode.Decompiler.Metadata;
using ICSharpCode.Decompiler.TypeSystem;
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

    public MethodToDisassemble(PEFile file, string typeName, string methodName, Func<IMethod, bool>? predicate = null)
    {
        this.method = file.FindMethod(typeName, methodName, predicate);
        this.file = file;
    }
}