using System;
using Checkers.Client;

using GService = Checkers.Client.GameClient.GameService;
using Controller = Checkers.Client.GameClient.GameService.GameController;
using System.Threading.Tasks;

class ConsoleClient
{
    static void Main(string[] args)
    {
        GameClient client = new("log", "pass");
        //GService service = client.Service;
        //Controller controller = service.Controller;

        Task.Run(async () =>
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
            });
        Console.Read();
        //controller.Request();
        //while (true)
        //{
        //    CrateAction(controller);
        //}
    }
    private static void CrateAction(Controller controller)
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
