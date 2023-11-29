[TestFixture]
public class Tests
{
    static readonly string assemblyPath = Assembly.GetExecutingAssembly().Location;
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
    public void GenericLookup()
    {
        using var file = new PEFile(assemblyPath);
        var type1 = file.FindType("GenericTarget`1");
        True(type1 != default);
        var type2 = file.FindType("GenericTarget`2");
        True(type2 != default);
        var method1 = file.FindMethod("GenericTarget`1", "GenericMethod1`1");
        True(method1 != default);
        var method2 = file.FindMethod("GenericTarget`2", "GenericMethod2`1");
        True(method2 != default);
    }

    [Test]
    public void NestedTypeLookup()
    {
        using var file = new PEFile(assemblyPath);
        var type1 = file.FindType("OuterType");
        True(type1 != default);
        var type2 = file.FindType("OuterType.NestedType");
        True(type2 != default);
        var type3 = file.FindType("OuterType.NestedType.NestedNestedType");
        True(type3 != default);
        var type4 = file.FindType("OuterType+NestedType+NestedNestedType");
        True(type4 != default);
    }

    [Test]
    public void NamespaceLookup()
    {
        using var file = new PEFile(assemblyPath);
        var type1 = file.FindType("MyNamespace.TypeInNamespace.NestedType");

        True(type1 != default);
    }

    [Test]
    public void MethodOverloadLookup()
    {
        using var file = new PEFile(assemblyPath);
        var type = file.FindType("GenericTarget`1");
        True(type != default);

        Assert.Throws<InvalidOperationException>(() => file.FindMethod("GenericTarget`1", "Overload"));

        var method = file.FindMethod("GenericTarget`1", "Overload", _ => _.Parameters.Count == 0);
        True(method != default);

        Assert.Throws<InvalidOperationException>(() => file.FindMethod("GenericTarget`1", "Overload", _ => _.Parameters.Count == 2));

        method = file.FindMethod("GenericTarget`1", "Overload", _ => _.Parameters is [_, { Type.ReflectionName: "System.Double" }]);
        True(method != default);
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