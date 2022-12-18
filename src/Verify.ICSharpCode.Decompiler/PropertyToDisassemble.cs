namespace VerifyTests;

public class PropertyToDisassemble
{
    internal readonly PropertyDefinitionHandle Property;
    internal readonly PEFile file;

    public PropertyToDisassemble(PEFile file, PropertyDefinitionHandle property)
    {
        Property = property;
        this.file = file;
    }

    public PropertyToDisassemble(PEFile file, string typeName, string propertyName)
    {
        Property = file.FindProperty(typeName,propertyName);
        this.file = file;
    }
}