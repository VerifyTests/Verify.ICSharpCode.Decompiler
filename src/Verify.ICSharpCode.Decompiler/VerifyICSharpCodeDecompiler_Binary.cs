namespace VerifyTests;

public static partial class VerifyICSharpCodeDecompiler
{
    static readonly Regex BinaryDataExpression = new(@"^[ \t]+[0-9A-F]{2}( [0-9A-F]{2})*", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public static void ScrubBinaryData(this VerifySettings settings) =>
        settings.ScrubLines(line => BinaryDataExpression.IsMatch(line));

    public static SettingsTask ScrubBinaryData(this SettingsTask settings)
    {
        settings.CurrentSettings.ScrubBinaryData();
        return settings;
    }
}