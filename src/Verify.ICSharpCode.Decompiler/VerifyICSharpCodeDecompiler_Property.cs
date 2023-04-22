namespace VerifyTests;

public static partial class VerifyICSharpCodeDecompiler
{
    static ConversionResult ConvertPropertyDefinitionHandle(PropertyToDisassemble property, IReadOnlyDictionary<string, object> context) =>
        Convert(context, disassembler =>
        {
            var propertyDefinition = property.PropertyDefinition;
            if (propertyDefinition.HasValue)
            {
                disassembler.DisassembleProperty(property.File, propertyDefinition.Value);
                return;
            }

            var propertyInfo = property.Property;
            if (propertyInfo == null)
                return;

            var partsToDisassemble = property.PartsToDisassemble;
            if (partsToDisassemble == PropertyParts.Definition)
            {
                disassembler.DisassembleProperty(property.File, (PropertyDefinitionHandle)propertyInfo.MetadataToken);
                return;
            }

            var getter = propertyInfo.Getter;
            var setter = propertyInfo.Setter;

            if (partsToDisassemble.HasFlag(PropertyParts.Getter) && getter != null)
            {
                disassembler.DisassembleMethod(property.File, (MethodDefinitionHandle)getter.MetadataToken);
            }
            if (partsToDisassemble.HasFlag(PropertyParts.Setter) && setter != null)
            {
                disassembler.DisassembleMethod(property.File, (MethodDefinitionHandle)setter.MetadataToken);
            }
        });

}