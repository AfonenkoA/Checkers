using Common.Entity;
using DatabaseStartup.Filling.Entity;
using static WebService.Repository.MSSqlImplementation.UserRepository;
using static DatabaseStartup.Declaration.Markup;
using static DatabaseStartup.Filling.Common;

namespace DatabaseStartup.Filling;

internal static class User
{
    private const string UserSource = "Users.csv";

    private static readonly string Type = $@"
GO
INSERT INTO {UserTypeTable}({UserTypeName}) VALUES 
({SqlString((UserType)1)}),
({SqlString((UserType)2)}),
({SqlString((UserType)3)}),
({SqlString((UserType)4)}),
({SqlString((UserType)5)})";

    private static string LoadUsers() =>
        string.Join('\n', ReadLines(DataFile(UserSource))
            .Select(s => new UserArgs(s))
            .Select(i => Exec(CreateUserProc, i)));

    public static readonly string Total = $@"
{Type}
{LoadUsers()}";
}