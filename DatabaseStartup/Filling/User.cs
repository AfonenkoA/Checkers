using Common.Entity;
using static WebService.Repository.MSSqlImplementation.UserRepository;
using static DatabaseStartup.Declaration.Markup;

namespace DatabaseStartup.Filling;

internal class User
{
    internal static readonly string Type = $@"
GO
INSERT INTO {UserTypeTable}({UserTypeName}) VALUES 
({SqlString((UserType)1)}),
({SqlString((UserType)2)}),
({SqlString((UserType)3)}),
({SqlString((UserType)4)}),
({SqlString((UserType)5)})";


}