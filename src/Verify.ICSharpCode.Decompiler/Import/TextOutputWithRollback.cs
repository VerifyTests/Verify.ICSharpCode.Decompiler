// ReSharper disable All
#nullable disable

using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using ICSharpCode.Decompiler.Disassembler;
using ICSharpCode.Decompiler.Metadata;
using ICSharpCode.Decompiler.TypeSystem;
using ICSharpCode.Decompiler;

namespace ICSharpCode.Decompiler.Disassembler;

internal class TextOutputWithRollback : ITextOutput
{
    List<Action<ITextOutput>> actions;
    ITextOutput target;

    public TextOutputWithRollback(ITextOutput target)
    {
        this.target = target;
        this.actions = new List<Action<ITextOutput>>();
    }

    string ITextOutput.IndentationString
    {
        get
        {
            return target.IndentationString;
        }
        set
        {
            target.IndentationString = value;
        }
    }

    public void Commit()
    {
        foreach (var action in actions)
        {
            action(target);
        }
    }

    public void Indent()
    {
        actions.Add(target => target.Indent());
    }

    public void MarkFoldEnd()
    {
        actions.Add(target => target.MarkFoldEnd());
    }

    public void MarkFoldStart(string collapsedText = "...", bool defaultCollapsed = false)
    {
        actions.Add(target => target.MarkFoldStart(collapsedText, defaultCollapsed));
    }

    public void Unindent()
    {
        actions.Add(target => target.Unindent());
    }

    public void Write(char ch)
    {
        actions.Add(target => target.Write(ch));
    }

    public void Write(string text)
    {
        actions.Add(target => target.Write(text));
    }

    public void WriteLine()
    {
        actions.Add(target => target.WriteLine());
    }

    public void WriteLocalReference(string text, object reference, bool isDefinition = false)
    {
        actions.Add(target => target.WriteLocalReference(text, reference, isDefinition));
    }

    public void WriteReference(OpCodeInfo opCode, bool omitSuffix = false)
    {
        actions.Add(target => target.WriteReference(opCode));
    }

    public void WriteReference(PEFile module, Handle handle, string text, string protocol = "decompile", bool isDefinition = false)
    {
        actions.Add(target => target.WriteReference(module, handle, text, protocol, isDefinition));
    }

    public void WriteReference(IType type, string text, bool isDefinition = false)
    {
        actions.Add(target => target.WriteReference(type, text, isDefinition));
    }

    public void WriteReference(IMember member, string text, bool isDefinition = false)
    {
        actions.Add(target => target.WriteReference(member, text, isDefinition));
    }
}

