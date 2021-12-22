using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.Metadata;
using VerifyTests;
using VerifyNUnit;
using NUnit.Framework;
using VerifyTests.ICSharpCode.Decompiler;

[TestFixture]
public class Tests
{
    static string assemblyPath;

    #region TypeDefinitionUsage
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
    #endregion

    #region TypeNameUsage
    [Test]
    public Task TypeNameUsage()
    {
        using var file = new PEFile(assemblyPath);
        return Verify(new TypeToDisassemble(file, "Target"));
    }
    #endregion

    #region MethodNameUsage
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
    #endregion

    #region PropertyNameUsage
    [Test]
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

    [Test]
    public async Task MethodNameMisMatch()
    {
        var exception = Assert.ThrowsAsync<Exception>(
            () =>
            {
                using var file = new PEFile(assemblyPath);
                return Verify(new MethodToDisassemble(file, "Target", "Missing"));
            });
        await Verify(exception);
    }

    [Test]
    public async Task PropertyNameMisMatch()
    {
        var exception = Assert.ThrowsAsync<Exception>(
            () =>
            {
                using var file = new PEFile(assemblyPath);
                return Verify(new PropertyToDisassemble(file, "Target", "Missing"));
            });
        await Verify(exception);
    }

    [Test]
    public async Task TypeNameMisMatch()
    {
        var exception = Assert.ThrowsAsync<Exception>(
            () =>
            {
                using var file = new PEFile(assemblyPath);
                return Verify(new TypeToDisassemble(file, "Missing"));
            });
        await Verify(exception);
    }

    static Tests()
    {
        #region Enable
        VerifyICSharpCodeDecompiler.Enable();
        #endregion
        assemblyPath = Assembly.GetExecutingAssembly().Location;

        VerifierSettings.AddScrubber(builder =>
        {
            using var sr = new StringReader(builder.ToString());
            builder.Clear();
            string? line;
            var index = 0;
            while ((line = sr.ReadLine()) != null)
            {
                index++;
                if (index > 45)
                {
                    break;
                }

                builder.AppendLine(line);
            }
            builder.AppendLine("...");
        });
    }
}