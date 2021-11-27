using System;
using Checkers.Client;

namespace Checkers.Transmission
{
public class Request
    {
    }

public class UserRequest : Request
    {
        public string? Login { get; set; }
    }

    public class SequreRequest : UserRequest
    {
        public string? Password { get; set; }
    }

    public sealed class UserUpdateRequest : SequreRequest
    {
        public string? NewPassword { get; set; }
        public string? NewEmail { get; set; }
        public int? NewPictureId { get; set; }
        public UserUpdateRequest()
        { }
        public UserUpdateRequest(SequreRequest request)
        {
            Login = request.Login;
            Password = request.Password;
        }
    }

    public sealed class UserItemsUpdateRequest : SequreRequest
    {
        public int? NewItemId { get; set; }
        public int? NewSelectedCheckersId { get; set; }
        public int? NewSelectedAnimationsId { get; set; }
        public UserItemsUpdateRequest()
        { }
        public UserItemsUpdateRequest(SequreRequest request)
        {
            Login = request.Login;
            Password = request.Password;
        }
    }

    public sealed class UserFriendsUpdateRequest : SequreRequest
    {
        public string? NewFriend { get; set; }
        public string? DeleteFriend { get; set; }
        public UserFriendsUpdateRequest()
        { }
        public UserFriendsUpdateRequest(SequreRequest request)
        {
            Login = request.Login;
            Password = request.Password;
        }
    }

    public enum ResponseStatus
    {
        Ok,
        Failed
    }

    public class BasicResponse
    {
        public static readonly BasicResponse Failed = new() { Status = ResponseStatus.Failed };
        public ResponseStatus Status { get; set; }
    }


    public sealed class UserAuthorizationResponse : BasicResponse
    {
        public new static readonly UserAuthorizationResponse Failed = new() { Status = ResponseStatus.Failed };
    }

    public sealed class UserGamesGetResponse : BasicResponse
    {
        public new static readonly UserGamesGetResponse Failed = new() { Status = ResponseStatus.Failed };
        public int[]? Games { get; set; }
    }

    public struct UserInfo
    {
        public string Nick { get; set; }
        public int Raiting { get; set; }
        public int PictureId { get; set; }
        public DateTime LastActivity { get; set; }
    }

    public struct GameAction
    {
        public int ActionNumber { get; set; }
        public TimeSpan ActionTime { get; set; }
        public int ActorId { get; set; }
        public string ActionDescription { get; set; }
    }

    public sealed class UserGetResponse : BasicResponse
    {
        public new static readonly UserGetResponse Failed = new() { Status = ResponseStatus.Failed };
        public UserInfo Info { get; set; }
    }

    public sealed class UserDeleteResponse : BasicResponse
    {
        public new static readonly UserDeleteResponse Failed = new() { Status = ResponseStatus.Failed };
    }

    public sealed class UserAchievementsGetResponse : BasicResponse
    {
        public new static readonly UserAchievementsGetResponse Failed = new() { Status = ResponseStatus.Failed };
        public int[]? Achievements { get; set; }
    }

    public sealed class GameGetRespose : BasicResponse
    {
        public new static readonly GameGetRespose Failed = new() { Status = ResponseStatus.Failed };
        public int Id { get; set; }
        public int Player1Id { get; set; }
        public int Player2Id { get; set; }
        public int Player1CheckersId { get; set; }
        public int Player2CheckersId { get; set; }
        public int Player1AnimationsId { get; set; }
        public int Player2AnimationsId { get; set; }
        public DateTime StartTime { get; set; }
        public int WinnerId { get; set; }
        public int Player1RaitingChange { get; set; }
        public int Player2RaitingChange { get; set; }
        public GameAction[]? Actions { get; set; }
    }

    public sealed class UserInfoResponse : BasicResponse
    {
        public new static readonly UserInfoResponse Failed = new() { Status = ResponseStatus.Failed };
        public UserInfo Info { get; set; }
        public string? Email { get; set; }
    }

    public sealed class UserItemsResponse : BasicResponse
    {
        public new static readonly UserItemsResponse Failed = new() { Status = ResponseStatus.Failed };
        public int[]? Items { get; set; }
        public int SelectedCheckersId { get; set; }
        public int SelectedAnimationsId { get; set; }
    }

    public sealed class UserFriendsResponse : BasicResponse
    {
        public new static readonly UserFriendsResponse Failed = new() { Status = ResponseStatus.Failed };
        public string[]? Friends { get; set; }
    }
}

namespace InGame
{
    public enum EventType
    {
        ConnectionAccept,
        Disconnect,
        GameStart,
        GameEnd,
        YourTurn,
        EnemyTurn,
        Emote,
        Move,
        Remove,
    }
    public class EventArgs
    {
        public EventType Type { get; }
        public EventArgs(EventType type)
        { Type = type; }
        public EventArgs()
        { }
    }

    public sealed class DisconnectEventArgs : EventArgs
    {
        public static readonly DisconnectEventArgs Instance = new();
        public DisconnectEventArgs() : base(EventType.Disconnect) { }
    }

    public sealed class ConnectionAcceptEventArgs : EventArgs
    {
        public static readonly ConnectionAcceptEventArgs Instance = new();
        public ConnectionAcceptEventArgs() : base(EventType.ConnectionAccept)
        { }
    }

    public sealed class GameStartEventArgs : EventArgs
    {
        public string? EnemyNick { get; set; }
        public int EnemyRaiting { get; set; }
        public int EnemyCheckersId { get; set; }
        public int EnemyAnimationsId { get; set; }
        public int EnemyPictureId { get; set; }
        public GameStartEventArgs() : base(EventType.GameStart) { }
    }

    public enum GameResult
    {
        Win,
        Lose
    }
    public sealed class GameEndEventArgs : EventArgs
    {
        public GameResult Result { get; set; }
        public int RaitingChange { get; set; }
        public GameEndEventArgs() : base(EventType.GameEnd)
        { }
    }

    public sealed class YourTurnEventArgs : EventArgs
    {
        public static readonly YourTurnEventArgs Instance = new();
        public YourTurnEventArgs() : base(EventType.YourTurn)
        { }
    }

    public sealed class EnemyTurnEventArgs : EventArgs
    {
        public static readonly EnemyTurnEventArgs Instance = new();
        public EnemyTurnEventArgs() : base(EventType.EnemyTurn)
        { }
    }

    public sealed class RemoveEventArgs : EventArgs
    {
        public Position Position { get; set; }
        public RemoveEventArgs() : base(EventType.Remove) { }
    }

    public sealed class EmoteEventArgs : EventArgs
    {
        public int EmotionId { get; set; }
        public EmoteEventArgs() : base(EventType.Emote) { }
        public EmoteEventArgs(EmoteActionArgs action) : this()
        {
            EmotionId = action.EmotionId;
        }
    }
    public enum ActionType
    {
        Connect,
        Disconnect,
        Move,
        Emote,
        Surrender,
        RequestForGame
    }

    public class ActionArgs
    {

        public ActionType Type { get; set; }
        public ActionArgs(ActionType type)
        {
            Type = type;
        }
        public ActionArgs() { }
    }

    public sealed class DisconnectActionArgs : ActionArgs
    {
        public static readonly DisconnectActionArgs Instance = new();
        public DisconnectActionArgs() : base(ActionType.Disconnect) { }
    }

    public sealed class ConnectAction : ActionArgs
    {
        public ConnectAction() : base(ActionType.Connect)
        { }
        public string? Login { get; set; }
        public string? Password { get; set; }
    }

    public sealed class MoveActionArgs : ActionArgs
    {
        public MoveActionArgs() : base(ActionType.Move) { }
        public Position From { get; set; }
        public Position To { get; set; }
    }
    public sealed class EmoteActionArgs : ActionArgs
    {
        public int EmotionId { get; set; }
        public EmoteActionArgs() : base(ActionType.Emote) { }
    }
    public sealed class SurrenderActionArgs : ActionArgs
    {
        public static readonly SurrenderActionArgs Instance = new();
        public SurrenderActionArgs() : base(ActionType.Surrender) { }
    }

    public sealed class RequestForGameActionArgs : ActionArgs
    {
        public static readonly RequestForGameActionArgs Instance = new();
        public RequestForGameActionArgs() : base(ActionType.RequestForGame) { }
    }
}

