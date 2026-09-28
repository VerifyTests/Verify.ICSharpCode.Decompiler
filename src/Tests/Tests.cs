namespace Tests;

public class Tests
{
    static readonly string assemblyPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
    static readonly string assembly2Path = typeof(AssemblyToProcess.Class).Assembly.Location;

    #region TypeDefinitionUsage

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

    #endregion

    #region TypeNameUsage

    [Test]
    public async Task TypeNameUsage()
    {
        using var file = new PEFile(assemblyPath);
        await Verify(new TypeToDisassemble(file, "Target"));
    }

    #endregion

    #region MethodNameUsage

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

    #endregion

    #region PropertyNameUsage

    [Test]
    public async Task PropertyNameUsage()
    {
        using var file = new PEFile(assemblyPath);
        await Verify(
            new PropertyToDisassemble(
                file,
                "Target",
                "Property"));
    }

    #endregion

    #region PropertyPartsUsage

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

    #endregion

    [Test]
    public async Task AssemblyUsage()
    {
        using var file = new PEFile(assembly2Path);
        await Verify(
            new AssemblyToDisassemble(file));
    }
    [Test]
    public async Task PEFile()
    {
        using var file = new PEFile(assembly2Path);
        await Verify(file);
    }

    [Test]
    public async Task AssemblyUsageWithScrubbers()
    {
        using var file = new PEFile(assembly2Path);
        await Verify(new AssemblyToDisassemble(file))
            .ScrubComments()
            .ScrubBinaryData();
    }

    [Test]
    public Task MethodNameMisMatch() =>
        ThrowsTask(async () =>
        {
            using var file = new PEFile(assemblyPath);
            await Verify(new MethodToDisassemble(file, "Target", "Missing"));
        });

    [Test]
    public Task PropertyNameMisMatch() =>
        ThrowsTask(async () =>
        {
            using var file = new PEFile(assemblyPath);
            await Verify(new PropertyToDisassemble(file, "Target", "Missing"));
        });

    [Test]
    public Task TypeNameMisMatch() =>
        ThrowsTask(async () =>
        {
            using var file = new PEFile(assemblyPath);
            await Verify(new TypeToDisassemble(file, "Missing"));
        });

    [Test]
    public async Task GenericLookup()
    {
        using var file = new PEFile(assemblyPath);
        var type1 = file.FindType("GenericTarget`1");
        await Assert.That(type1 != default).IsTrue();
        var type2 = file.FindType("GenericTarget`2");
        await Assert.That(type2 != default).IsTrue();
        var method1 = file.FindMethod("GenericTarget`1", "GenericMethod1`1");
        await Assert.That(method1 != default).IsTrue();
        var method2 = file.FindMethod("GenericTarget`2", "GenericMethod2`1");
        await Assert.That(method2 != default).IsTrue();
    }

    [Test]
    public async Task NestedTypeLookup()
    {
        using var file = new PEFile(assemblyPath);
        var type1 = file.FindType("OuterType");
        await Assert.That(type1 != default).IsTrue();
        var type2 = file.FindType("OuterType.NestedType");
        await Assert.That(type2 != default).IsTrue();
        var type3 = file.FindType("OuterType.NestedType.NestedNestedType");
        await Assert.That(type3 != default).IsTrue();
        var type4 = file.FindType("OuterType+NestedType+NestedNestedType");
        await Assert.That(type4 != default).IsTrue();
    }

    [Test]
    public async Task NamespaceLookup()
    {
        using var file = new PEFile(assemblyPath);
        var type1 = file.FindType("MyNamespace.TypeInNamespace.NestedType");

        await Assert.That(type1 != default).IsTrue();
    }

    [Test]
    public async Task MethodOverloadLookup()
    {
        using var file = new PEFile(assemblyPath);
        var type = file.FindType("GenericTarget`1");
        await Assert.That(type != default).IsTrue();

        await Assert.That(() => file.FindMethod("GenericTarget`1", "Overload")).ThrowsExactly<InvalidOperationException>();

        var method = file.FindMethod("GenericTarget`1", "Overload", _ => _.Parameters.Count == 0);
        await Assert.That(method != default).IsTrue();

        await Assert.That(() => file.FindMethod("GenericTarget`1", "Overload", _ => _.Parameters.Count == 2)).ThrowsExactly<InvalidOperationException>();

        method = file.FindMethod("GenericTarget`1", "Overload", _ => _.Parameters is [_, { Type.ReflectionName: "System.Double" }]);
        await Assert.That(method != default).IsTrue();
    }

    #region BackwardCompatibility

    [Test]
    public async Task BackwardCompatibility()
    {
        using var file = new PEFile(assemblyPath);
        await Verify(new TypeToDisassemble(file, "Target"))
            .DontNormalizeIl();
    }

    #endregion
}