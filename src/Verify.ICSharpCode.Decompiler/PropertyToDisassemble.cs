using System.Reflection.Metadata;
using ICSharpCode.Decompiler.Metadata;
using Verify.ICSharpCode.Decompiler;

namespace Verify
{
    public class PropertyToDisassemble
    {
        internal PropertyDefinitionHandle Property;
        internal PEFile file;

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
}