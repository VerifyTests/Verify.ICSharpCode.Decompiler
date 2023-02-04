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

    public static void UseIlExtension(this VerifySettings settings) =>
        settings.Context["VerifyICSharpCodeDecompiler.Extension"] = "il";

    public static SettingsTask UseIlExtension(this SettingsTask settings)
    {
        settings.CurrentSettings.UseIlExtension();
        return settings;
    }

    static string GetIlExtension(this IReadOnlyDictionary<string, object> context)
    {
        if (context.TryGetValue("VerifyICSharpCodeDecompiler.Extension", out var value)
            && value is string result)
        {
            return result;
        }

        return "txt";
    }
}
