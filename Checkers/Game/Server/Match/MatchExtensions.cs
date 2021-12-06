using Checkers.Game.Model;

namespace Checkers.Game.Server.Match;

internal static class MatchExtensions
{
    public static void AddListener(this Player p, IGameController controller)
    {
        p.OnMove += controller.Move;
        p.OnEmote += controller.Emote;
        p.OnSurrender += controller.Surrender;
    }

    public static void RemoveListener(this Player p, IGameController controller)
    {
        p.OnMove -= controller.Move;
        p.OnEmote -= controller.Emote;
        p.OnSurrender -= controller.Surrender;
    }

    public static void AddListener(this GameModel model, Player p)
    {
        model.OnMove += p.SendEvent;
        model.OnEmote += p.SendEvent;
        model.OnGameStart += p.SendEvent;
        model.OnGameEnd += p.SendEvent;
    }

    public static void RemoveListener(this GameModel model, Player p)
    {
        model.OnMove -= p.SendEvent;
        model.OnEmote -= p.SendEvent;
        model.OnGameStart -= p.SendEvent;
        model.OnGameEnd -= p.SendEvent;
    }

    public static void AddListener(this GameModel model, Match m)
    {
        model.OnMove += m.Log;
        model.OnEmote += m.Log;
        model.OnGameStart += m.Log;
        model.OnGameEnd += m.Log;
    }

    public static void RemoveListener(this GameModel model, Match m)
    {
        model.OnMove -= m.Log;
        model.OnEmote -= m.Log;
        model.OnGameStart -= m.Log;
        model.OnGameEnd -= m.Log;
    }
}