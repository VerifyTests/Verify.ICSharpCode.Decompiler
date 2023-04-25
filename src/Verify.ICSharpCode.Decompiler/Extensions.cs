using ICSharpCode.Decompiler.TypeSystem;

namespace VerifyTests.ICSharpCode.Decompiler;

public static class Extensions
{
    public static ITypeDefinition FindTypeDefinition(this PEFile file, string typeName)
    {
        var typeSystem = new DecompilerTypeSystem(file, new UniversalAssemblyResolver(null, false, null));

        return typeSystem.Modules
            .SelectMany(m => m.TypeDefinitions)
            .SingleOrDefault(t => t.ReflectionName == typeName || t.FullName == typeName)
               ?? throw new($"Could not find `{typeName}` in `{file.FileName}`");
    }

    public static TypeDefinitionHandle FindType(this PEFile file, string typeName) =>
        (TypeDefinitionHandle)FindTypeDefinition(file, typeName).MetadataToken;

    public static PropertyDefinitionHandle FindProperty(this PEFile file, string typeName, string propertyName) =>
        (PropertyDefinitionHandle)FindPropertyInfo(file, typeName, propertyName).MetadataToken;

    public static IProperty FindPropertyInfo(this PEFile file, string typeName, string propertyName)
    {
        var typeDefinition = file.FindTypeDefinition(typeName);
        return typeDefinition.Properties.SingleOrDefault(p => p.Name == propertyName)
                       ?? throw new($"Could not find `{typeName}.{propertyName}` in `{file.FileName}`");
    }

    public static MethodDefinitionHandle FindMethod(this PEFile file, string typeName, string methodName, Func<IMethod, bool>? predicate = null)
    {
        var type = file.FindTypeDefinition(typeName);

        var method = type.Methods
            .SingleOrDefault(m => m.GetName() == methodName && predicate?.Invoke(m) != false)
                     ?? throw new($"Could not find `{typeName}.{methodName}` in `{file.FileName}`");

        return (MethodDefinitionHandle)method.MetadataToken;
    }

    static string GetName(this IMethod method)
    {
        var name = method.Name;
        var genericParameterCount = method.TypeParameters.Count;

        return genericParameterCount > 0 ? $"{name}`{genericParameterCount}" : name;
    }
}