.class private auto ansi '<Module>'
{
}

.class public auto ansi beforefieldinit AssemblyToProcess.Class
	extends [System.Runtime]System.Object
{
	.field private int32 '<Property>k__BackingField'
	.custom instance void [System.Runtime]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [System.Runtime]System.Diagnostics.DebuggerBrowsableState) = (
	)
	.custom instance void [System.Runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
	)
	.field private float64 'field'

	.method public hidebysig specialname rtspecialname 
		instance void .ctor () cil managed 
	{
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: call instance void [System.Runtime]System.Object::.ctor()
		IL_0006: nop
		IL_0007: ret
	}

	.method public hidebysig specialname 
		instance int32 get_Property () cil managed 
	{
		.custom instance void [System.Runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
		)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: ldfld int32 AssemblyToProcess.Class::'<Property>k__BackingField'
		IL_0006: ret
	}

	.method public hidebysig 
		instance void Method () cil managed 
	{
		.maxstack 3
		.locals init (
			[0] string
		)

		IL_0000: nop
		IL_0001: ldstr "SomeText"
		IL_0006: ldarg.0
		IL_0007: ldflda float64 AssemblyToProcess.Class::'field'
		IL_000c: call class [System.Runtime]System.Globalization.CultureInfo [System.Runtime]System.Globalization.CultureInfo::get_CurrentCulture()
		IL_0011: call instance string [System.Runtime]System.Double::ToString(class [System.Runtime]System.IFormatProvider)
		IL_0016: call string [System.Runtime]System.String::Concat(string, string)
		IL_001b: stloc.0
		IL_001c: ldloc.0
		IL_001d: call void [System.Diagnostics.TraceSource]System.Diagnostics.Trace::WriteLine(string)
		IL_0022: nop
		IL_0023: ret
	}

	.method public hidebysig specialname 
		instance void set_Property (
			int32 'value'
		) cil managed 
	{
		.custom instance void [System.Runtime]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
		)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: ldarg.1
		IL_0002: stfld int32 AssemblyToProcess.Class::'<Property>k__BackingField'
		IL_0007: ret
	}

	.property instance int32 Property()
	{
		.get instance int32 AssemblyToProcess.Class::get_Property()
		.set instance void AssemblyToProcess.Class::set_Property(int32)
	}

}

