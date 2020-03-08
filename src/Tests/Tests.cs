using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.Metadata;
using Verify;
using Verify.ICSharpCode.Decompiler;
using VerifyXunit;
using Xunit;
using Xunit.Abstractions;

public class Tests :
    VerifyBase
{
    string assemblyPath;

    #region TypeDefinitionUsage
    [Fact]
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
    #endregion

    #region TypeNameUsage
    [Fact]
    public Task TypeNameUsage()
    {
        using var file = new PEFile(assemblyPath);
        return Verify(new TypeToDisassemble(file, "Target"));
    }
    #endregion

    #region MethodNameUsage
    [Fact]
    public Task MethodNameUsage()
    {
        using var file = new PEFile(assemblyPath);
        return Verify(
            new MethodToDisassemble(
                file,
                "Target",
                "OnPropertyChanged"));
    }
    #endregion

    #region PropertyNameUsage
    [Fact]
    public Task PropertyNameUsage()
    {
        using var file = new PEFile(assemblyPath);
        return Verify(
            new PropertyToDisassemble(
                file,
                "Target",
                "Property"));
    }
    #endregion

    [Fact]
    public async Task MethodNameMisMatch()
    {
        var exception = await Assert.ThrowsAsync<Exception>(
            () =>
            {
                using var file = new PEFile(assemblyPath);
                return Verify(new MethodToDisassemble(file, "Target", "Missing"));
            });
        await Verify(exception);
    }

    [Fact]
    public async Task PropertyNameMisMatch()
    {
        var exception = await Assert.ThrowsAsync<Exception>(
            () =>
            {
                using var file = new PEFile(assemblyPath);
                return Verify(new PropertyToDisassemble(file, "Target", "Missing"));
            });
        await Verify(exception);
    }

    [Fact]
    public async Task TypeNameMisMatch()
    {
        var exception = await Assert.ThrowsAsync<Exception>(
            () =>
            {
                using var file = new PEFile(assemblyPath);
                return Verify(new TypeToDisassemble(file, "Missing"));
            });
        await Verify(exception);
    }

    public Tests(ITestOutputHelper output) :
        base(output)
    {
        assemblyPath = Assembly.GetExecutingAssembly().Location;
    }

    static Tests()
    {
        SharedVerifySettings.AddScrubber(x =>
        {
            var builder = new StringBuilder();
            using var sr = new StringReader(x);
            string? line;
            var index = 0;
            while ((line = sr.ReadLine()) != null)
            {
                index++;
                if (index > 35)
                {
                    break;
                }

                builder.AppendLine(line);
            }
            builder.AppendLine("...");
            return builder.ToString();
        });
    }
}