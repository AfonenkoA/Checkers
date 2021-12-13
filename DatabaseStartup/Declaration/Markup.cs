namespace DatabaseStartup.Declaration;

internal static class Markup
{
    public static string SqlString(object o) => $"N'{o.ToString()?.Replace("\'", @"''")}'";
}