public static class ModuleInit
{
    #region enable

    [ModuleInitializer]
    public static void Init() =>
        VerifyICSharpCodeDecompiler.Initialize();

    #endregion

    [ModuleInitializer]
    public static void AnotherModuleInitializer() =>
        VerifyICSharpCodeDecompiler.Initialize();

    [ModuleInitializer]
    public static void InitOther() =>
        VerifierSettings.InitializePlugins();
}