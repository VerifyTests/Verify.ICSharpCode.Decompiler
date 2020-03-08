using System.IO;
using System.Text;
using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.Disassembler;
using Verify.ICSharpCode.Decompiler;

namespace Verify
{
    public static class VerifyICSharpCodeDecompiler
    {
        public static void Enable()
        {
            SharedVerifySettings.RegisterFileConverter<TypeToDisassemble>("txt", ConvertTypeDefinitionHandle);
            SharedVerifySettings.RegisterFileConverter<MethodToDisassemble>("txt", ConvertMethodDefinitionHandle);
            SharedVerifySettings.RegisterFileConverter<PropertyToDisassemble>("txt", ConvertPropertyDefinitionHandle);
        }

        static ConversionResult ConvertTypeDefinitionHandle(TypeToDisassemble type, VerifySettings _)
        {
            var output = new PlainTextOutput();
            var disassembler = new ReflectionDisassembler(output, default);
            disassembler.DisassembleType(type.file, type.type);
            return ConversionResult(output);
        }

        static ConversionResult ConvertPropertyDefinitionHandle(PropertyToDisassemble property, VerifySettings _)
        {
            var output = new PlainTextOutput();
            var disassembler = new ReflectionDisassembler(output, default);
            disassembler.DisassembleProperty(property.file, property.Property);
            return ConversionResult(output);
        }

        static ConversionResult ConvertMethodDefinitionHandle(MethodToDisassemble method, VerifySettings _)
        {
            var output = new PlainTextOutput();
            var disassembler = new ReflectionDisassembler(output, default);
            disassembler.DisassembleMethod(method.file, method.method);
            return ConversionResult(output);
        }

        static ConversionResult ConversionResult(PlainTextOutput output)
        {
            return new ConversionResult(null, new Stream[] {StringToMemoryStream(output.ToString())});
        }

        static MemoryStream StringToMemoryStream(string text)
        {
            var bytes = Encoding.UTF8.GetBytes(text.Replace("\r\n", "\n"));
            return new MemoryStream(bytes);
        }
    }
}