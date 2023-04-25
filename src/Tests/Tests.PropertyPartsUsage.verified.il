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
