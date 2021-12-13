using Api.Interface;
using Api.WebImplementation;
using Common.Entity;

namespace Site.Data.Models
{
    public class UserModel
    {
        private static readonly IAsyncUserApi api = new UserWebApi();

        async Task<IEnumerable<BasicUserData>> List()
        {
            var (_, Users) = await api.TryGetUsersByNick("");
            return Users;
        }
    }
}
