﻿.method private hidebysig 
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
