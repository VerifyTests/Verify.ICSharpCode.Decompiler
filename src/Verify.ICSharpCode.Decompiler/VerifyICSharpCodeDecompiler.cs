using System.Collections.Generic;
using System.IO;
using System.Text;
using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.Disassembler;
using VerifyTests.ICSharpCode.Decompiler;

namespace VerifyTests
{
    public static class VerifyICSharpCodeDecompiler
    {
        public static void Enable()
        {
            VerifierSettings.RegisterFileConverter<TypeToDisassemble>(ConvertTypeDefinitionHandle);
            VerifierSettings.RegisterFileConverter<MethodToDisassemble>(ConvertMethodDefinitionHandle);
            VerifierSettings.RegisterFileConverter<PropertyToDisassemble>(ConvertPropertyDefinitionHandle);
        }

        static ConversionResult ConvertTypeDefinitionHandle(TypeToDisassemble type, IReadOnlyDictionary<string, object> _)
        {
            PlainTextOutput output = new();
            ReflectionDisassembler disassembler = new(output, default);
            disassembler.DisassembleType(type.file, type.type);
            return ConversionResult(output);
        }

        static ConversionResult ConvertPropertyDefinitionHandle(PropertyToDisassemble property, IReadOnlyDictionary<string, object> _)
        {
            PlainTextOutput output = new();
            ReflectionDisassembler disassembler = new(output, default);
            disassembler.DisassembleProperty(property.file, property.Property);
            return ConversionResult(output);
        }

        static ConversionResult ConvertMethodDefinitionHandle(MethodToDisassemble method, IReadOnlyDictionary<string, object> _)
        {
            PlainTextOutput output = new();
            ReflectionDisassembler disassembler = new(output, default);
            disassembler.DisassembleMethod(method.file, method.method);
            return ConversionResult(output);
        }

        static ConversionResult ConversionResult(PlainTextOutput output)
        {
            return new(null,
                new[]
                {
                    new ConversionStream("txt", StringToMemoryStream(output.ToString()))
                });
        }

        static MemoryStream StringToMemoryStream(string text)
        {
            var bytes = Encoding.UTF8.GetBytes(text.Replace("\r\n", "\n"));
            return new(bytes);
        }
    }
}