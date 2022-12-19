namespace VerifyTests;

public static partial class VerifyICSharpCodeDecompiler
{
    public static void DontNormalizeIl(this VerifySettings settings) =>
        settings.Context["VerifyICSharpCodeDecompiler.Normalize"] = false;

    public static SettingsTask DontNormalizeIl(this SettingsTask settings)
    {
        settings.CurrentSettings.DontNormalizeIl();
        return settings;
    }

    static bool GetNormalizeIl(this IReadOnlyDictionary<string, object> context)
    {
        if (context.TryGetValue("VerifyICSharpCodeDecompiler.Normalize", out var value) &&
            value is bool result)
        {
            return result;
        }

        return true;
    }
}