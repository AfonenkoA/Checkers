﻿using static System.IO.Directory;
using static System.IO.File;
using static DatabaseStartup.Declaration.Markup;
using static WebService.Repository.MSSqlImplementation.RepositoryBase;

namespace DatabaseStartup.Filling;

internal static class Common
{
    public const string DeclareId = $"GO\nDECLARE {IdVar} INT\n";

    public static string ResourceFile(string filename) => SqlString($@"{Path}\Filling\Img\{filename}");

    public static IEnumerable<string> ReadLines(string filename) => ReadAllLines(filename);

    private static readonly string Path = GetCurrentDirectory();

    public static readonly Exception LineSplitException = new ArgumentNullException { Source = "Can't split line by ';'" };

    public static string DataFile(string filename) => $@"{Path}\Filling\Data\{filename}";

    public static string Exec(string command, object args) =>
        $"EXEC {command} {args}";
}