namespace VerifyTests;

public class PropertyToDisassemble
{
    internal readonly PropertyDefinitionHandle Property;
    internal readonly PEFile File;

    public PropertyToDisassemble(PEFile file, PropertyDefinitionHandle property)
    {
        Property = property;
        File = file;
    }

    public PropertyToDisassemble(PEFile file, string typeName, string propertyName)
    {
        Property = file.FindProperty(typeName,propertyName);
        File = file;
    }
}