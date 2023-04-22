using ICSharpCode.Decompiler.TypeSystem;

namespace VerifyTests;

[Flags]
public enum PropertyParts
{
    /// <summary>
    /// Disassembles only the property definition
    /// </summary>
    Definition = 0,
    /// <summary>
    /// Disassembles the property getter
    /// </summary>
    Getter = 1,
    /// <summary>
    /// Disassembles the property setter
    /// </summary>
    Setter = 2,
    /// <summary>
    /// Disassembles both the property getter and setter
    /// </summary>
    GetterAndSetter = Getter | Setter,
}

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