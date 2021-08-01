using System;
using Checkers.Client;

using GService = Checkers.Client.GameClient.GameService;
using Controller = Checkers.Client.GameClient.GameService.GameController;

class ConsoleClient
{
    static void Main(string[] args)
    {
        GameClient client = new("log", "pass");
        GService service = client.Service;
        Controller controller = service.Controller;
        controller.Request();
        while (true)
        {
            CrateAction(controller);
        }
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
