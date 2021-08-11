using Checkers.Client;
using Checkers.Data;
using System;
using System.Data.Entity;
using System.Linq;
using static Checkers.Client.GameClient;
using static Checkers.Client.GameClient.GameService;

class ConsoleClient
{
    static void Main(string[] args)
    {
        using (GameDatabase db = new())
        {
            db.Users.Include(u => u.Achievements);
            foreach (User user in db.Users)
                Console.WriteLine(string.Join<Achievement>(" ", user.Achievements.ToArray()));
        }
        Console.Read();

    }

    private static void TestGameServer(GameClient client)
    {
        GameService service = client.Service;
        service.Connect();
        GameController controller = service.Controller;
        controller.Request();
        controller.Move(new Position(1, 2), new Position(2, 3));
        controller.Surrender();
        service.Disconnect();
    }


    private static async void TestAPI(GameClient client)
    {
        try
        {
            Console.WriteLine(await client.GetGameAsync(3));
            Console.WriteLine(await client.AuthorizeAsync());
            Console.WriteLine(await client.GetAchievementsAsync());
            Console.WriteLine(await client.GetFriendsAsync());
            Console.WriteLine(await client.GetGamesAsync());
            Console.WriteLine(await client.GetItemsAsync());
        }
        catch (Exception ex)
        { Console.WriteLine(ex); }
    }

    private static void CrateAction(GameController controller)
    {
        Console.WriteLine("1: Move\n2: Emote\n3: Surrender");
        switch (int.Parse(Console.ReadLine()))
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
