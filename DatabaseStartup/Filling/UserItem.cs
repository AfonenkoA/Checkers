using DatabaseStartup.Filling.Entity;
using static WebService.Repository.MSSqlImplementation.Repository;
using static WebService.Repository.MSSqlImplementation.ItemRepository;
using static WebService.Repository.MSSqlImplementation.UserRepository;
using static DatabaseStartup.Filling.Common;


namespace DatabaseStartup.Filling;

internal static class UserItem
{
    private const string UserPictureSource = "UserAvatars.csv";

    private static string Pictures()
    {
        static string PictureId(string name) =>
            $"(SELECT {Id} FROM {PictureTable} WHERE {ItemName} = {name})";

        static string Set(string name) => $"SET @id = {PictureId(name)}";
        static string ExecUpdate(string log, string pass) => $"EXEC {UpdateUserPictureProc} {log}, {pass}, @id";
        static string Update(UserPictureArgs u) => $"{Set(u.PicName)}\n{ExecUpdate(u.Login, u.Password)}";

        return DeclareId +
               string.Join('\n', ReadLines(DataFile(UserPictureSource))
                   .Select(s => Update(new UserPictureArgs(s))));
    }

    public static readonly string Total = $@"
{Pictures()}";
}