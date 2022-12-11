// Copyright (c) 2022 Tom Englert
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this
// software and associated documentation files (the "Software"), to deal in the Software
// without restriction, including without limitation the rights to use, copy, modify, merge,
// publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons
// to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or
// substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
// PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
// FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.

#nullable enable
// ReSharper disable All

using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;

using ICSharpCode.Decompiler.IL;
using ICSharpCode.Decompiler.Metadata;

namespace ICSharpCode.Decompiler.Disassembler
{
    public class SortByNameProcessor : IEntityProcessor
    {
        public IReadOnlyCollection<InterfaceImplementationHandle> Process(PEFile module,
            IReadOnlyCollection<InterfaceImplementationHandle> items) =>
            items.OrderBy(item => GetSortKey(item, module)).ToArray();

        public IReadOnlyCollection<TypeDefinitionHandle> Process(PEFile module,
            IReadOnlyCollection<TypeDefinitionHandle> items) =>
            items.OrderBy(item => GetSortKey(item, module)).ToArray();

        public IReadOnlyCollection<MethodDefinitionHandle> Process(PEFile module,
            IReadOnlyCollection<MethodDefinitionHandle> items) =>
            items.OrderBy(item => GetSortKey(item, module)).ToArray();

        public IReadOnlyCollection<PropertyDefinitionHandle> Process(PEFile module,
            IReadOnlyCollection<PropertyDefinitionHandle> items) =>
            items.OrderBy(item => GetSortKey(item, module)).ToArray();

        public IReadOnlyCollection<EventDefinitionHandle> Process(PEFile module,
            IReadOnlyCollection<EventDefinitionHandle> items) =>
            items.OrderBy(item => GetSortKey(item, module)).ToArray();

        public IReadOnlyCollection<FieldDefinitionHandle> Process(PEFile module,
            IReadOnlyCollection<FieldDefinitionHandle> items) =>
            items.OrderBy(item => GetSortKey(item, module)).ToArray();

        public IReadOnlyCollection<CustomAttributeHandle> Process(PEFile module,
            IReadOnlyCollection<CustomAttributeHandle> items) =>
            items.OrderBy(item => GetSortKey(item, module)).ToArray();

        private static string GetSortKey(TypeDefinitionHandle handle, PEFile module) =>
            handle.GetFullTypeName(module.Metadata).ToILNameString();

        private static string GetSortKey(MethodDefinitionHandle handle, PEFile module)
        {
            PlainTextOutput output = new PlainTextOutput();
            MethodDefinition definition = module.Metadata.GetMethodDefinition(handle);

            // Start with the methods name, skip return type
            output.Write(module.Metadata.GetString(definition.Name));
            DisassemblerSignatureTypeProvider signatureProvider = new DisassemblerSignatureTypeProvider(module, output);
            MethodSignature<Action<ILNameSyntax>> signature =
                definition.DecodeSignature(signatureProvider, new GenericContext(handle, module));

            if (signature.GenericParameterCount > 0)
            {
                output.Write($"`{signature.GenericParameterCount}");
            }

            ImportedMethods.WriteParameterList(output, signature);

            return output.ToString();
        }

        private static string GetSortKey(InterfaceImplementationHandle handle, PEFile module) =>
            module.Metadata.GetInterfaceImplementation(handle)
                .Interface
                .GetFullTypeName(module.Metadata)
                .ToILNameString();

        private static string GetSortKey(FieldDefinitionHandle handle, PEFile module) =>
            module.Metadata.GetString(module.Metadata.GetFieldDefinition(handle).Name);

        private static string GetSortKey(PropertyDefinitionHandle handle, PEFile module) =>
            module.Metadata.GetString(module.Metadata.GetPropertyDefinition(handle).Name);

        private static string GetSortKey(EventDefinitionHandle handle, PEFile module) =>
            module.Metadata.GetString(module.Metadata.GetEventDefinition(handle).Name);

        private static string GetSortKey(CustomAttributeHandle handle, PEFile module) =>
            module.Metadata.GetCustomAttribute(handle)
                .Constructor
                .GetDeclaringTypeImport(module.Metadata)
                .GetFullTypeName(module.Metadata)
                .ToILNameString();
    }

    static class ImportedMethods
    {
        public static EntityHandle GetDeclaringTypeImport(this EntityHandle entity, MetadataReader metadata)
        {
            switch (entity.Kind)
            {
                case HandleKind.MethodDefinition:
                    var md = metadata.GetMethodDefinition((MethodDefinitionHandle) entity);
                    return md.GetDeclaringType();
                case HandleKind.MemberReference:
                    var mr = metadata.GetMemberReference((MemberReferenceHandle) entity);
                    return mr.Parent;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        internal static void WriteParameterList(ITextOutput output, MethodSignature<Action<ILNameSyntax>> methodSignature)
        {
            output.Write("(");
            for (int i = 0; i < methodSignature.ParameterTypes.Length; ++i)
            {
                if (i > 0)
                    output.Write(", ");
                if (i == methodSignature.RequiredParameterCount)
                    output.Write("..., ");
                methodSignature.ParameterTypes[i](ILNameSyntax.SignatureNoNamedTypeParameters);
            }

            output.Write(")");
        }
    }
}