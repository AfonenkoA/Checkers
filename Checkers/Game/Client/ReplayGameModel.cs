using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkers.Data.Entity;
using Checkers.Game.Model;
using EmoteEvent = Checkers.Game.Model.EmoteEvent;
using GameStartEvent = Checkers.Game.Model.GameStartEvent;

namespace Checkers.Game.Client;

internal class ReplayGameModel : GameModel
{
    private readonly Data.Entity.Game _gameData;

    public ReplayGameModel(Data.Entity.Game game)
    {
        _gameData = game;
    }
    public override async Task Run()
    {
        Start(new GameStartEvent());
        var previousTime = TimeSpan.Zero;
        var actions = new List<StoredEvent>();
        actions.AddRange(_gameData.Moves);
        actions.AddRange(_gameData.Emotions);
        var orderedActions = actions.OrderBy(a => a.Time);
        foreach (var action in orderedActions)
        {
            await Task.Delay(action.Time - previousTime);
            previousTime = action.Time;
            switch (action)
            {
                case StoredMoveEvent move:
                    Move(move.Actor, move.From, move.To);
                    break;
                case StoredEmoteEvent emote:
                    Emote(new EmoteEvent
                    {
                        Side = emote.Actor,
                        Emotion = emote.Emotion
                    });
                    break;
            }
        }
    }
}