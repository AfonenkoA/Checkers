using System;
using System.IO;
using Checkers.Data.Entity;
using Checkers.Game.Model;
using static System.DateTime;
using static Checkers.CommunicationProtocol;

namespace Checkers.Game.Server.Repository;

internal sealed class Repository : IGameRepository, IPlayerRepository
{
    public Emotion GetEmotion(int id) => new();

    public void SaveGame(GameData game) => File.WriteAllText($"{Now}",Serialize(game));

    public PlayerInfo GetInfo(Credential c) => new();
}