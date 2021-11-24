using System;
using Checkers.Client;

namespace Checkers.Transmission
{
public class Request
    {
    }

    public class GameGetRequest : Request
    {
        public int GameID { get; set; }
    }

    public class UserRequest : Request
    {
        public string? Login { get; set; }
        public UserRequest() { }
    }

    public class SequreRequest : UserRequest
    {
        public string? Password { get; set; }
        public SequreRequest() : base()
        { }
    }

    public class UserAuthorizationRequest : SequreRequest
    {
        public UserAuthorizationRequest()
        {
        }

        public UserAuthorizationRequest(SequreRequest request) : this()
        {
            Login = request.Login;
            Password = request.Password;
        }
    }

    public class UserInfoRequest : SequreRequest
    {
        public UserInfoRequest()
        {
        }

        public UserInfoRequest(SequreRequest request) : this()
        {
            Login = request.Login;
            Password = request.Password;
        }
    }

    public class UserCreationRequest : SequreRequest
    {
        public string? Email { get; set; }
        public string? Nick { get; set; }

        public UserCreationRequest(SequreRequest request) : this()
        {
            Login = request.Login;
            Password = request.Password;
        }

        public UserCreationRequest()
        {
        }
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
    public class UserDeleteRequest : SequreRequest
    {
        public UserDeleteRequest() { }
        public UserDeleteRequest(SequreRequest request)
        {
            Login = request.Login;
            Password = request.Password;
        }
    }

    public sealed class UserItemsUpdateRequest : SequreRequest
    {
        public int? NewItemID { get; set; }
        public int? NewSelectedCheckersID { get; set; }
        public int? NewSelectedAnimationsID { get; set; }
        public UserItemsUpdateRequest()
        { }
        public UserItemsUpdateRequest(SequreRequest request)
        {
            Login = request.Login;
            Password = request.Password;
        }
    }
    public class UserFriendsGetRequest : SequreRequest
    {
        public UserFriendsGetRequest()
        { }
        public UserFriendsGetRequest(SequreRequest request)
        {
            Login = request.Login;
            Password = request.Password;
        }
    }
    public class UserFriendsUpdateRequest : SequreRequest
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

    public class UserAchievementsGetRequest : UserRequest
    {
        public UserAchievementsGetRequest()
        { }
    }
    public class UserGamesGetRequest : UserRequest
    {
        public UserGamesGetRequest()
        { }
    }

    public enum ResponseStatus
    {
        OK,
        FAILED
    }

    public class BasicResponse
    {
        public static readonly BasicResponse Failed = new() { Status = ResponseStatus.FAILED };
        public ResponseStatus Status { get; set; }
    }


    public sealed class UserAuthorizationResponse : BasicResponse
    {
        public new static readonly UserAuthorizationResponse Failed = new() { Status = ResponseStatus.FAILED };
    }

    public sealed class UserGamesGetResponse : BasicResponse
    {
        public new static readonly UserGamesGetResponse Failed = new() { Status = ResponseStatus.FAILED };
        public int[]? Games { get; set; }
    }

    public struct UserInfo
    {
        public string Nick { get; set; }
        public int Raiting { get; set; }
        public int PictureID { get; set; }
        public DateTime LastActivity { get; set; }
    }

    public struct GameAction
    {
        public int ActionNumber { get; set; }
        public TimeSpan ActionTime { get; set; }
        public int ActorID { get; set; }
        public string ActionDescription { get; set; }
    }

    public sealed class UserGetResponse : BasicResponse
    {
        public new static readonly UserGetResponse Failed = new() { Status = ResponseStatus.FAILED };
        public UserInfo Info { get; set; }
    }

    public sealed class UserDeleteResponse : BasicResponse
    {
        public new static readonly UserDeleteResponse Failed = new() { Status = ResponseStatus.FAILED };
    }

    public sealed class UserAchievementsGetResponse : BasicResponse
    {
        public new static readonly UserAchievementsGetResponse Failed = new() { Status = ResponseStatus.FAILED };
        public int[]? Achievements { get; set; }
    }

    public sealed class GameGetRespose : BasicResponse
    {
        public new static readonly GameGetRespose Failed = new() { Status = ResponseStatus.FAILED };
        public int ID { get; set; }
        public int Player1ID { get; set; }
        public int Player2ID { get; set; }
        public int Player1CheckersID { get; set; }
        public int Player2CheckersID { get; set; }
        public int Player1AnimationsID { get; set; }
        public int Player2AnimationsID { get; set; }
        public DateTime StartTime { get; set; }
        public int WinnerID { get; set; }
        public int Player1RaitingChange { get; set; }
        public int Player2RaitingChange { get; set; }
        public GameAction[]? Actions { get; set; }
    }

    public sealed class UserInfoResponse : BasicResponse
    {
        public new static readonly UserInfoResponse Failed = new() { Status = ResponseStatus.FAILED };
        public UserInfo Info { get; set; }
        public string? Email { get; set; }
    }

    public sealed class UserItemsResponse : BasicResponse
    {
        public new static readonly UserItemsResponse Failed = new() { Status = ResponseStatus.FAILED };
        public int[]? Items { get; set; }
        public int SelectedCheckersID { get; set; }
        public int SelectedAnimationsID { get; set; }
    }

    public sealed class UserFriendsResponse : BasicResponse
    {
        public new static readonly UserFriendsResponse Failed = new() { Status = ResponseStatus.FAILED };
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

    public class DisconnectEventArgs : EventArgs
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

    public class RemoveEventArgs : EventArgs
    {
        public Position Position { get; set; }
        public RemoveEventArgs() : base(EventType.Remove) { }
    }

    public class EmoteEventArgs : EventArgs
    {
        public int EmotionID { get; set; }
        public EmoteEventArgs() : base(EventType.Emote) { }
        public EmoteEventArgs(EmoteActionArgs action) : this()
        {
            EmotionID = action.EmotionID;
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
        public int EmotionID { get; set; }
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

