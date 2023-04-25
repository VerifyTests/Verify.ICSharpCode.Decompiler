using ICSharpCode.Decompiler.TypeSystem;

namespace VerifyTests;

public class PropertyToDisassemble
{
    internal PropertyDefinitionHandle? PropertyDefinition;
    internal readonly IProperty? Property;
    internal readonly PEFile File;
    internal readonly PropertyParts PartsToDisassemble;

    public PropertyToDisassemble(PEFile file, PropertyDefinitionHandle property)
    {
        PropertyDefinition = property;
        File = file;
    }

    public PropertyToDisassemble(PEFile file, IProperty property, PropertyParts partsToDisassemble = PropertyParts.GetterAndSetter)
    {
        Property = property;
        PartsToDisassemble = partsToDisassemble;
        File = file;
    }

    public PropertyToDisassemble(PEFile file, string typeName, string propertyName, PropertyParts partsToDisassemble = default)
        : this(file, file.FindPropertyInfo(typeName, propertyName), partsToDisassemble)
    {
    }
}