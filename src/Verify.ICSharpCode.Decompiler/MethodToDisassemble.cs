using ICSharpCode.Decompiler.TypeSystem;

namespace VerifyTests;

public class MethodToDisassemble
{
    internal readonly MethodDefinitionHandle Method;
    internal readonly PEFile File;

    public MethodToDisassemble(PEFile file, MethodDefinitionHandle method)
    {
        Method = method;
        File = file;
    }

    public MethodToDisassemble(PEFile file, string typeName, string methodName, Func<IMethod, bool>? predicate = null)
    {
        Method = file.FindMethod(typeName, methodName, predicate);
        File = file;
    }
}