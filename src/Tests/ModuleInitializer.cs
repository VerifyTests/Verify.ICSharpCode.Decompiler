public static class ModuleInit
{
    #region enable

    [ModuleInitializer]
    public static void Init()
    {
        VerifyICSharpCodeDecompiler.Enable();

        #endregion

        VerifyDiffPlex.Initialize();
    }
}