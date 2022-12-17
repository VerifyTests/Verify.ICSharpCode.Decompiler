using System.Reflection.Metadata;
using ICSharpCode.Decompiler.Metadata;
using ICSharpCode.Decompiler.TypeSystem;
using VerifyTests.ICSharpCode.Decompiler;

namespace VerifyTests;

public class MethodToDisassemble
{
    internal readonly MethodDefinitionHandle method;
    internal readonly PEFile file;

    static MethodToDisassemble() => VerifyICSharpCodeDecompiler.Enable();

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