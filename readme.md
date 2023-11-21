# <img src="/src/icon.png" height="30px"> Verify.ICSharpCode.Decompiler

[![Discussions](https://img.shields.io/badge/Verify-Discussions-yellow?svg=true&label=)](https://github.com/orgs/VerifyTests/discussions)
[![Build status](https://ci.appveyor.com/api/projects/status/8kndmciqywvg350w?svg=true)](https://ci.appveyor.com/project/SimonCropp/verify-icsharpcode-decompiler)
[![NuGet Status](https://img.shields.io/nuget/v/Verify.ICSharpCode.Decompiler.svg)](https://www.nuget.org/packages/Verify.ICSharpCode.Decompiler/)

Extends [Verify](https://github.com/VerifyTests/Verify) to allow verification of assemblies via [ICSharpCode.Decompiler](https://github.com/icsharpcode/ILSpy/wiki/Getting-Started-With-ICSharpCode.Decompiler).


**See [Milestones](../../milestones?state=closed) for release notes.**


## NuGet package

https://nuget.org/packages/Verify.ICSharpCode.Decompiler/


## Usage

<!-- snippet: enable -->
<a id='snippet-enable'></a>
```cs
[ModuleInitializer]
public static void Init() =>
    VerifyICSharpCodeDecompiler.Initialize();
```
<sup><a href='/src/Tests/ModuleInitializer.cs#L3-L9' title='Snippet source file'>snippet source</a> | <a href='#snippet-enable' title='Start of snippet'>anchor</a></sup>
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
public async Task TypeDefinitionUsage()
{
    using var file = new PEFile(assemblyPath);
    var type = file.Metadata.TypeDefinitions
        .Single(
            _ =>
            {
                var fullName = _.GetFullTypeName(file.Metadata);
                return fullName.Name == "Target";
            });
    await Verify(new TypeToDisassemble(file, type));
}
```
<sup><a href='/src/Tests/Tests.cs#L7-L23' title='Snippet source file'>snippet source</a> | <a href='#snippet-typedefinitionusage' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

Result:

<!-- snippet: Tests.TypeDefinitionUsage.verified.il -->
<a id='snippet-Tests.TypeDefinitionUsage.verified.il'></a>
```il
.class public auto ansi beforefieldinit Target
	extends [System.Runtime]System.Object
	implements [System.ObjectModel]System.ComponentModel.INotifyPropertyChanged
{
	.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
		01 00 00 00 00
	)
	.custom instance void System.Runtime.CompilerServices.NullableContextAttribute::.ctor(uint8) = (
		01 00 02 00 00
	)
	.interfaceimpl type [System.ObjectModel]System.ComponentModel.INotifyPropertyChanged
	.custom instance void System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
		01 00 00 00 00
	)

	// Fields
	.field private string 'property'
	.field private class [System.ObjectModel]System.ComponentModel.PropertyChangedEventHandler PropertyChanged
	.custom instance void [System.Runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [System.Runtime]System.Diagnostics.DebuggerBrowsableState) = (
		01 00 00 00 00 00 00 00
	)
	.custom instance void [System.Runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
		01 00 00 00
	)

	// Methods
	.method public hidebysig specialname rtspecialname 
		instance void .ctor () cil managed 
	{
		// Header size: 1
		// Code size: 8 (0x8)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: call instance void [System.Runtime]System.Object::.ctor()
		IL_0006: nop
		IL_0007: ret
	} // end of method Target::.ctor

	.method public final hidebysig specialname newslot virtual 
		instance void add_PropertyChanged (
			class [System.ObjectModel]System.ComponentModel.PropertyChangedEventHandler 'value'
		) cil managed 
	{
		.custom instance void [System.Runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Header size: 12
		// Code size: 41 (0x29)
		.maxstack 3
		.locals init (
			[0] class [System.ObjectModel]System.ComponentModel.PropertyChangedEventHandler,
			[1] class [System.ObjectModel]System.ComponentModel.PropertyChangedEventHandler,
			[2] class [System.ObjectModel]System.ComponentModel.PropertyChangedEventHandler
		)

		IL_0000: ldarg.0
		IL_0001: ldfld class [System.ObjectModel]System.ComponentModel.PropertyChangedEventHandler Target::PropertyChanged
		IL_0006: stloc.0
		// loop start (head: IL_0007)
			IL_0007: ldloc.0
			IL_0008: stloc.1
			IL_0009: ldloc.1
			IL_000a: ldarg.1
			IL_000b: call class [System.Runtime]System.Delegate [System.Runtime]System.Delegate::Combine(class [System.Runtime]System.Delegate, class [System.Runtime]System.Delegate)
			IL_0010: castclass [System.ObjectModel]System.ComponentModel.PropertyChangedEventHandler
			IL_0015: stloc.2
			IL_0016: ldarg.0
			IL_0017: ldflda class [System.ObjectModel]System.ComponentModel.PropertyChangedEventHandler Target::PropertyChanged
			IL_001c: ldloc.2
			IL_001d: ldloc.1
			IL_001e: call !!0 [System.Threading]System.Threading.Interlocked::CompareExchange<class [System.ObjectModel]System.ComponentModel.PropertyChangedEventHandler>(!!0&, !!0, !!0)
			IL_0023: stloc.0
			IL_0024: ldloc.0
			IL_0025: ldloc.1
			IL_0026: bne.un.s IL_0007
		// end loop
		IL_0028: ret
	} // end of method Target::add_PropertyChanged

	.method public hidebysig specialname 
		instance string get_Property () cil managed 
	{
		// Header size: 1
		// Code size: 7 (0x7)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: ldfld string Target::'property'
		IL_0006: ret
	} // end of method Target::get_Property

	.method private hidebysig 
		instance void OnPropertyChanged (
			[opt] string propertyName
		) cil managed 
	{
		.param [1] = nullref
			.custom instance void [System.Runtime]System.Runtime.CompilerServices.CallerMemberNameAttribute::.ctor() = (
				01 00 00 00
			)
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

	.method public final hidebysig specialname newslot virtual 
		instance void remove_PropertyChanged (
			class [System.ObjectModel]System.ComponentModel.PropertyChangedEventHandler 'value'
		) cil managed 
	{
		.custom instance void [System.Runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Header size: 12
		// Code size: 41 (0x29)
		.maxstack 3
		.locals init (
			[0] class [System.ObjectModel]System.ComponentModel.PropertyChangedEventHandler,
			[1] class [System.ObjectModel]System.ComponentModel.PropertyChangedEventHandler,
			[2] class [System.ObjectModel]System.ComponentModel.PropertyChangedEventHandler
		)

		IL_0000: ldarg.0
		IL_0001: ldfld class [System.ObjectModel]System.ComponentModel.PropertyChangedEventHandler Target::PropertyChanged
		IL_0006: stloc.0
		// loop start (head: IL_0007)
			IL_0007: ldloc.0
			IL_0008: stloc.1
			IL_0009: ldloc.1
			IL_000a: ldarg.1
			IL_000b: call class [System.Runtime]System.Delegate [System.Runtime]System.Delegate::Remove(class [System.Runtime]System.Delegate, class [System.Runtime]System.Delegate)
			IL_0010: castclass [System.ObjectModel]System.ComponentModel.PropertyChangedEventHandler
			IL_0015: stloc.2
			IL_0016: ldarg.0
			IL_0017: ldflda class [System.ObjectModel]System.ComponentModel.PropertyChangedEventHandler Target::PropertyChanged
			IL_001c: ldloc.2
			IL_001d: ldloc.1
			IL_001e: call !!0 [System.Threading]System.Threading.Interlocked::CompareExchange<class [System.ObjectModel]System.ComponentModel.PropertyChangedEventHandler>(!!0&, !!0, !!0)
			IL_0023: stloc.0
			IL_0024: ldloc.0
			IL_0025: ldloc.1
			IL_0026: bne.un.s IL_0007
		// end loop
		IL_0028: ret
	} // end of method Target::remove_PropertyChanged

	.method public hidebysig specialname 
		instance void set_Property (
			string 'value'
		) cil managed 
	{
		// Header size: 1
		// Code size: 21 (0x15)
		.maxstack 8

		IL_0000: nop
		IL_0001: ldarg.0
		IL_0002: ldarg.1
		IL_0003: stfld string Target::'property'
		IL_0008: ldarg.0
		IL_0009: ldstr "Property"
		IL_000e: call instance void Target::OnPropertyChanged(string)
		IL_0013: nop
		IL_0014: ret
	} // end of method Target::set_Property

	// Events
	.event [System.ObjectModel]System.ComponentModel.PropertyChangedEventHandler PropertyChanged
	{
		.addon instance void Target::add_PropertyChanged(class [System.ObjectModel]System.ComponentModel.PropertyChangedEventHandler)
		.removeon instance void Target::remove_PropertyChanged(class [System.ObjectModel]System.ComponentModel.PropertyChangedEventHandler)
	}


	// Properties
	.property instance string Property()
	{
		.get instance string Target::get_Property()
		.set instance void Target::set_Property(string)
	}

} // end of class Target
```
<sup><a href='/src/Tests/Tests.TypeDefinitionUsage.verified.il#L1-L199' title='Snippet source file'>snippet source</a> | <a href='#snippet-Tests.TypeDefinitionUsage.verified.il' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

A string for the type name can also be used:

<!-- snippet: TypeNameUsage -->
<a id='snippet-typenameusage'></a>
```cs
[Test]
public async Task TypeNameUsage()
{
    using var file = new PEFile(assemblyPath);
    await Verify(new TypeToDisassemble(file, "Target"));
}
```
<sup><a href='/src/Tests/Tests.cs#L25-L34' title='Snippet source file'>snippet source</a> | <a href='#snippet-typenameusage' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

### Verify Method

<!-- snippet: MethodNameUsage -->
<a id='snippet-methodnameusage'></a>
```cs
[Test]
public async Task MethodNameUsage()
{
    using var file = new PEFile(assemblyPath);
    await Verify(
        new MethodToDisassemble(
            file,
            "Target",
            "OnPropertyChanged"));
}
```
<sup><a href='/src/Tests/Tests.cs#L36-L49' title='Snippet source file'>snippet source</a> | <a href='#snippet-methodnameusage' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

Result:

<!-- snippet: Tests.MethodNameUsage.verified.il -->
<a id='snippet-Tests.MethodNameUsage.verified.il'></a>
```il
.method private hidebysig 
	instance void OnPropertyChanged (
		[opt] string propertyName
	) cil managed 
{
	.param [1] = nullref
		.custom instance void [System.Runtime]System.Runtime.CompilerServices.CallerMemberNameAttribute::.ctor() = (
			01 00 00 00
		)
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
```
<sup><a href='/src/Tests/Tests.MethodNameUsage.verified.il#L1-L29' title='Snippet source file'>snippet source</a> | <a href='#snippet-Tests.MethodNameUsage.verified.il' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

### Verify Property
<!-- snippet: PropertyPartsUsage -->
<a id='snippet-propertypartsusage'></a>
```cs
[Test]
public async Task PropertyPartsUsage()
{
    using var file = new PEFile(assemblyPath);
    await Verify(
        new PropertyToDisassemble(
            file,
            "Target",
            "Property",
            PropertyParts.GetterAndSetter));
}
```
<sup><a href='/src/Tests/Tests.cs#L66-L80' title='Snippet source file'>snippet source</a> | <a href='#snippet-propertypartsusage' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

Result:

<!-- snippet: Tests.PropertyPartsUsage.verified.il -->
<a id='snippet-Tests.PropertyPartsUsage.verified.il'></a>
```il
.method public hidebysig specialname 
	instance string get_Property () cil managed 
{
	// Header size: 1
	// Code size: 7 (0x7)
	.maxstack 8

	IL_0000: ldarg.0
	IL_0001: ldfld string Target::'property'
	IL_0006: ret
} // end of method Target::get_Property
.method public hidebysig specialname 
	instance void set_Property (
		string 'value'
	) cil managed 
{
	// Header size: 1
	// Code size: 21 (0x15)
	.maxstack 8

	IL_0000: nop
	IL_0001: ldarg.0
	IL_0002: ldarg.1
	IL_0003: stfld string Target::'property'
	IL_0008: ldarg.0
	IL_0009: ldstr "Property"
	IL_000e: call instance void Target::OnPropertyChanged(string)
	IL_0013: nop
	IL_0014: ret
} // end of method Target::set_Property
```
<sup><a href='/src/Tests/Tests.PropertyPartsUsage.verified.il#L1-L30' title='Snippet source file'>snippet source</a> | <a href='#snippet-Tests.PropertyPartsUsage.verified.il' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Settings

Starting with version 3.2 the generated IL is normalized by default, to avoid failed tests only because the binary layout has changed:

- types and members are sorted by name
- RVA adress comments are stripped 

To turn of the sorting, use the `DontNormalizeIL` setting. It will then decompile IL reflecting the original layout:

<!-- snippet: BackwardCompatibility -->
<a id='snippet-backwardcompatibility'></a>
```cs
[Test]
public async Task BackwardCompatibility()
{
    using var file = new PEFile(assemblyPath);
    await Verify(new TypeToDisassemble(file, "Target"))
        .DontNormalizeIl();
}
```
<sup><a href='/src/Tests/Tests.cs#L184-L194' title='Snippet source file'>snippet source</a> | <a href='#snippet-backwardcompatibility' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

## Icon

[Gem](https://thenounproject.com/term/shatter/1084820/) designed by [Bakunetsu Kaito](https://thenounproject.com/sevenknights_friendship/) from [The Noun Project](https://thenounproject.com).
