namespace VerifyTests;

public static partial class VerifyICSharpCodeDecompiler
{
    public static void ScrubComments(this VerifySettings settings)
    {
        settings.ScrubLines(
            line =>
            {
                var commentStart = line.IndexOf("//", StringComparison.Ordinal);

                return commentStart == 0 ||
                       (commentStart > 0 && line.Take(commentStart).All(char.IsWhiteSpace));
            },
            ScrubberLocation.Last);

        settings.ScrubLinesWithReplace(
            line =>
            {
                var commentStart = line.IndexOf("//", StringComparison.Ordinal);
                if (commentStart < 0)
                {
                    return line;
                }

                return line.Substring(0, commentStart).TrimEnd();
            },
            ScrubberLocation.Last);
    }

    public static SettingsTask ScrubComments(this SettingsTask settings)
    {
        settings.CurrentSettings.ScrubComments();
        return settings;
    }
}