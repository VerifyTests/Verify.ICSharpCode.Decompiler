# <img src="/src/icon.png" height="30px"> Verify.ICSharpCode.Decompiler

[![Build status](https://ci.appveyor.com/api/projects/status/8kndmciqywvg350w?svg=true)](https://ci.appveyor.com/project/SimonCropp/verify-icsharpcode-decompiler)
[![NuGet Status](https://img.shields.io/nuget/v/Verify.ICSharpCode.Decompiler.svg)](https://www.nuget.org/packages/Verify.ICSharpCode.Decompiler/)

Extends [Verify](https://github.com/VerifyTests/Verify) to allow verification of assemblies via [ICSharpCode.Decompiler](https://github.com/icsharpcode/ILSpy/wiki/Getting-Started-With-ICSharpCode.Decompiler).

Support is available via a [Tidelift Subscription](https://tidelift.com/subscription/pkg/nuget-verify.icsharpcode.decompiler?utm_source=nuget-verify.icsharpcode.decompiler&utm_medium=referral&utm_campaign=enterprise).

toc


## NuGet package

https://nuget.org/packages/Verify.ICSharpCode.Decompiler/


## Usage

Enable once at assembly load time:

snippet: Enable

Then given the following type:

snippet: Target.cs


### Verify Type

snippet: TypeDefinitionUsage

Result:

snippet: Tests.TypeDefinitionUsage.verified.txt

A string for the type name can also be used:

snippet: TypeNameUsage


### Verify Method

snippet: MethodNameUsage

Result:

snippet: Tests.MethodNameUsage.verified.txt


## Security contact information

To report a security vulnerability, use the [Tidelift security contact](https://tidelift.com/security). Tidelift will coordinate the fix and disclosure.


## Icon

[Gem](https://thenounproject.com/term/shatter/1084820/) designed by [Bakunetsu Kaito](https://thenounproject.com/sevenknights_friendship/) from [The Noun Project](https://thenounproject.com/creativepriyanka).