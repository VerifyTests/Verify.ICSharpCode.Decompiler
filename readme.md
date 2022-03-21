# <img src="/src/icon.png" height="30px"> Verify.ICSharpCode.Decompiler

[![Build status](https://ci.appveyor.com/api/projects/status/8kndmciqywvg350w?svg=true)](https://ci.appveyor.com/project/SimonCropp/verify-icsharpcode-decompiler)
[![NuGet Status](https://img.shields.io/nuget/v/Verify.ICSharpCode.Decompiler.svg)](https://www.nuget.org/packages/Verify.ICSharpCode.Decompiler/)

Extends [Verify](https://github.com/VerifyTests/Verify) to allow verification of assemblies via [ICSharpCode.Decompiler](https://github.com/icsharpcode/ILSpy/wiki/Getting-Started-With-ICSharpCode.Decompiler).



## NuGet package

https://nuget.org/packages/Verify.ICSharpCode.Decompiler/


## Usage

Enable once at assembly load time:

<!-- snippet: Enable -->
<a id='snippet-enable'></a>
```cs
VerifyICSharpCodeDecompiler.Enable();
```
<sup><a href='/src/Tests/Tests.cs#L98-L100' title='Snippet source file'>snippet source</a> | <a href='#snippet-enable' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

Then given the following type:

<!-- snippet: Target.cs -->
<a id='snippet-Target.cs'></a>
```cs
using System.ComponentModel;

public class Target :
    INotifyPropertyChanged
{
    void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new(propertyName));

    public event PropertyChangedEventHandler? PropertyChanged;

    string? property;

    public string? Property
    {
        get => property;
        set
        {
            property = value;
            OnPropertyChanged();
        }
    }
}
```
<sup><a href='/src/Tests/Target.cs#L1-L22' title='Snippet source file'>snippet source</a> | <a href='#snippet-Target.cs' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Verify Type

<!-- snippet: TypeDefinitionUsage -->
<a id='snippet-typedefinitionusage'></a>
```cs
[Test]
public Task TypeDefinitionUsage()
{
    using var file = new PEFile(assemblyPath);
    var type = file.Metadata.TypeDefinitions
        .Single(x =>
        {
            var fullName = x.GetFullTypeName(file.Metadata);
            return fullName.Name == "Target";
        });
    return Verify(new TypeToDisassemble(file, type));
}
```
<sup><a href='/src/Tests/Tests.cs#L10-L23' title='Snippet source file'>snippet source</a> | <a href='#snippet-typedefinitionusage' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

Result:

<!-- snippet: Tests.TypeDefinitionUsage.verified.txt -->
<a id='snippet-Tests.TypeDefinitionUsage.verified.txt'></a>
```txt
.class public auto ansi beforefieldinit Target
	extends [System.Runtime]System.Object
	implements .custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
		01 00 00 00 00
	)
	[System.ObjectModel]System.ComponentModel.INotifyPropertyChanged
{
	.custom instance void System.Runtime.CompilerServices.NullableContextAttribute::.ctor(uint8) = (
		01 00 02 00 00
	)
	.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
		01 00 00 00 00
	)
	// Fields
	.field private class [System.ObjectModel]System.ComponentModel.PropertyChangedEventHandler PropertyChanged
	.custom instance void [System.Runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
		01 00 00 00
	)
	.custom instance void [System.Runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [System.Runtime]System.Diagnostics.DebuggerBrowsableState) = (
		01 00 00 00 00 00 00 00
	)
	.field private string 'property'

	// Methods
	.method private hidebysig 
		instance void OnPropertyChanged (
			[opt] string propertyName
		) cil managed 
	{
		.param [1] = nullref
			.custom instance void [System.Runtime]System.Runtime.CompilerServices.CallerMemberNameAttribute::.ctor() = (
				01 00 00 00
			)
		// Method begins at RVA 0x2092
		// Header size: 1
		// Code size: 26 (0x1a)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: ldfld class [System.ObjectModel]System.ComponentModel.PropertyChangedEventHandler Target::PropertyChanged
		IL_0006: dup
		IL_0007: brtrue.s IL_000c

		IL_0009: pop
		IL_000a: br.s IL_0019
...
```
<sup><a href='/src/Tests/Tests.TypeDefinitionUsage.verified.txt#L1-L46' title='Snippet source file'>snippet source</a> | <a href='#snippet-Tests.TypeDefinitionUsage.verified.txt' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

A string for the type name can also be used:

<!-- snippet: TypeNameUsage -->
<a id='snippet-typenameusage'></a>
```cs
[Test]
public Task TypeNameUsage()
{
    using var file = new PEFile(assemblyPath);
    return Verify(new TypeToDisassemble(file, "Target"));
}
```
<sup><a href='/src/Tests/Tests.cs#L25-L32' title='Snippet source file'>snippet source</a> | <a href='#snippet-typenameusage' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Verify Method

<!-- snippet: MethodNameUsage -->
<a id='snippet-methodnameusage'></a>
```cs
[Test]
public Task MethodNameUsage()
{
    using var file = new PEFile(assemblyPath);
    return Verify(
        new MethodToDisassemble(
            file,
            "Target",
            "OnPropertyChanged"));
}
```
<sup><a href='/src/Tests/Tests.cs#L34-L45' title='Snippet source file'>snippet source</a> | <a href='#snippet-methodnameusage' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

Result:

<!-- snippet: Tests.MethodNameUsage.verified.txt -->
<a id='snippet-Tests.MethodNameUsage.verified.txt'></a>
```txt
.method private hidebysig 
	instance void OnPropertyChanged (
		[opt] string propertyName
	) cil managed 
{
	.param [1] = nullref
		.custom instance void [System.Runtime]System.Runtime.CompilerServices.CallerMemberNameAttribute::.ctor() = (
			01 00 00 00
		)
	// Method begins at RVA 0x2092
	// Header size: 1
	// Code size: 26 (0x1a)
	.maxstack 8

	IL_0000: ldarg.0
	IL_0001: ldfld class [System.ObjectModel]System.ComponentModel.PropertyChangedEventHandler Target::PropertyChanged
	IL_0006: dup
	IL_0007: brtrue.s IL_000c

	IL_0009: pop
	IL_000a: br.s IL_0019

	IL_000c: ldarg.0
	IL_000d: ldarg.1
	IL_000e: newobj instance void [System.ObjectModel]System.ComponentModel.PropertyChangedEventArgs::.ctor(string)
	IL_0013: callvirt instance void [System.ObjectModel]System.ComponentModel.PropertyChangedEventHandler::Invoke(object, class [System.ObjectModel]System.ComponentModel.PropertyChangedEventArgs)
	IL_0018: nop

	IL_0019: ret
} // end of method Target::OnPropertyChanged
...
```
<sup><a href='/src/Tests/Tests.MethodNameUsage.verified.txt#L1-L31' title='Snippet source file'>snippet source</a> | <a href='#snippet-Tests.MethodNameUsage.verified.txt' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->



## Icon

[Gem](https://thenounproject.com/term/shatter/1084820/) designed by [Bakunetsu Kaito](https://thenounproject.com/sevenknights_friendship/) from [The Noun Project](https://thenounproject.com).
