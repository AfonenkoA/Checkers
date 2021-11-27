using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Checkers.Api.Interface;
using Checkers.Api.WebImplementation;
using Checkers.Client;
using Checkers.Data.Entity;
using Checkers.Data.Old;
using static System.Console;
using static Checkers.Client.GameClient;
using static Checkers.Client.GameClient.GameService;
using User = Checkers.Data.Old.User;

namespace ConsoleClient;

internal static class ConsoleClient
{
    private static async Task Main()
    {
        IAsyncUserApi api = new UserWebApi();
        WriteLine(await api.CreateUser(new UserCreationData()));
        Read();
    }
    //ForegroundColor = ConsoleColor.Red;
    //WriteLine("◯");
    //ForegroundColor = ConsoleColor.Green;
    //WriteLine("⬛");
    //ForegroundColor = ConsoleColor.White;
    //Read();
    

    private static void TestDatabase()
    {
        using GameDatabase db = new GameDatabase.Factory("").Get();
        db.Users.Include(u => u.Achievements);
        foreach (User user in db.Users)
            WriteLine(string.Join<Achievement>(" ", user.Achievements.ToArray()));
    }

    private static void TestGameServer(GameClient client)
    {
        GameService service = client.Service;
        service.Connect();
        GameController controller = service.Controller;
        if (controller != null)
        {
            controller.Request();
            controller.Move(new Position(1, 2), new Position(2, 3));
            controller.Surrender();
        }

        service.Disconnect();
    }


    private static async void TestApi(GameClient client)
    {
        try
        {
            WriteLine(await client.GetGameAsync(3));
            WriteLine(await client.AuthorizeAsync());
            WriteLine(await client.GetAchievementsAsync());
            WriteLine(await client.GetFriendsAsync());
            WriteLine(await client.GetGamesAsync());
            WriteLine(await client.GetItemsAsync());
        }
        catch (Exception ex)
        { WriteLine(ex); }
    }

    private static void CrateAction(GameController controller)
    {
        WriteLine("1: Move\n2: Emote\n3: Surrender");
        switch (int.Parse(ReadLine() ?? string.Empty))
        {
            case 1:
                controller.Move(new Position(1, 1), new Position(2, 2));
                break;
            case 2:
                controller.Emote(1);
                break;
            case 3:
                controller.Surrender();
                break;
            default: throw new Exception();
        }
    }

}