﻿.class public auto ansi beforefieldinit Target
	extends [System.Runtime]System.Object
	implements [System.ObjectModel]System.ComponentModel.INotifyPropertyChanged
{
	.custom instance void [System.Runtime]System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
		01 00 00 00 00
	)
	.custom instance void [System.Runtime]System.Runtime.CompilerServices.NullableContextAttribute::.ctor(uint8) = (
		01 00 02 00 00
	)
	.interfaceimpl type [System.ObjectModel]System.ComponentModel.INotifyPropertyChanged
		.custom instance void [System.Runtime]System.Runtime.CompilerServices.NullableAttribute::.ctor(uint8) = (
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
