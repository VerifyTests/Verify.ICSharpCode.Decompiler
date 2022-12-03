using System.Reflection.Metadata;
using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.Metadata;

namespace VerifyTests.ICSharpCode.Decompiler;

public static class Extensions
{
    public static TypeDefinitionHandle FindType(this PEFile file, string typeName)
    {
        var type = file.Metadata.TypeDefinitions
            .SingleOrDefault(handle =>
            {
                var fullName = handle.GetFullTypeName(file.Metadata);
                return fullName.ToILNameString() == typeName;
            });
        if (type == default)
        {
            throw new($"Could not find `{typeName}` in `{file.FileName}`");
        }

        return type;
    }

    public static PropertyDefinitionHandle FindProperty(this PEFile file, string typeName, string propertyName)
    {
        var type = file.FindType(typeName);
        var metadata = file.Metadata;
        var typeDefinition = metadata.GetTypeDefinition(type);

        foreach (var handle in typeDefinition.GetProperties())
        {
            var definition = metadata.GetPropertyDefinition(handle);
            var s = metadata.GetString(definition.Name);
            if (s == propertyName)
            {
                return handle;
            }
        }

        throw new($"Could not find `{typeName}.{propertyName}` in `{file.FileName}`");
    }

    public static MethodDefinitionHandle FindMethod(this PEFile file, string typeName, string methodName)
    {
        var type = file.FindType(typeName);
        var metadata = file.Metadata;
        var typeDefinition = metadata.GetTypeDefinition(type);

        foreach (var handle in typeDefinition.GetMethods())
        {
            var definition = metadata.GetMethodDefinition(handle);
            var name = metadata.GetString(definition.Name);
            var genericParameterCount = definition.GetGenericParameters().Count;
            if (genericParameterCount > 0)
            {
                name += $"`{genericParameterCount}";
            }

            if (name == methodName)
            {
                return handle;
            }
        }

        throw new($"Could not find `{typeName}.{methodName}` in `{file.FileName}`");
    }
}