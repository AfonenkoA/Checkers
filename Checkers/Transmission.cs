using System;
using Checkers.Client;

namespace Checkers.Transmission
{
    public class Request
    {
    }

    public class GameGetRequest : Request
    {
        public int gameID { get; set; }
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
            Password= request.Password;
        }

        public UserCreationRequest()
        {
        }
    }

    public class UserUpdateRequest : SequreRequest
    {
        public string? NewPassword { get; set; }
        public string? NewEmail { get; set; }
        public int? NewPictureID { get; set; }
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

    public class UserItemsUpdateRequest : SequreRequest
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

    public class Response
    {
        public ResponseStatus Status { get; set; }
    }


    public class UserAuthorizationResponse : Response
    { }

    public class UserGamesGetResponse : Response
    {
        public int[]? Games { get;set;  }
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

    public class UserGetResponse : Response
    {
        public UserInfo Info { get; set; }
    }

    public class UserDeleteResponse : Response
    { }

    public class UserAchievementsGetResponse : Response
    {
        public int[]? Achievements { get; set; }
    }

    public class GameGetRespose : Response
    {
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

    public class UserInfoResponse : Response
    {
        public UserInfo Info { get; set; }
        public string? Email { get; set; }
    }

    public class UserItemsResponse : Response
    {
        public int[]? Items { get; set; }
        public int SelectedCheckersID { get; set; }
        public int SelectedAnimationsID { get; set; }
    }

    public class UserFriendsResponse : Response
    {
        public string[]? Friends { get; set; }
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
        public class Event
        {
            public EventType Type { get; set; }
            public Event(EventType type)
            { Type = type; }
            public Event()
            {}
        }

        public class DisconnectEvent : Event
        {
            public static readonly DisconnectEvent Instance = new();
            public DisconnectEvent() : base(EventType.Disconnect) { }
        }

        public class ConnectionAcceptEvent : Event
        {
            public static readonly ConnectionAcceptEvent Instance = new();
            public ConnectionAcceptEvent() : base(EventType.ConnectionAccept)
            { }
        }

        public class GameStartEvent : Event
        {
            public string? EnemyNick { get; set; }
            public int EnemyRaiting { get; set; }
            public int EnemyCheckersID { get; set; }
            public int EnemyAnimationsID { get; set; }
            public int EnemyPictureID { get; set; }
            public GameStartEvent() : base(EventType.GameStart) { }
        }

        public enum GameResult
        {
            Win,
            Lose
        }
        public class GameEndEvent : Event
        {
            public GameResult Result { get; set; }
            public int RaitingChange { get; set; }
            public GameEndEvent() : base(EventType.GameEnd)
            {}
        }

        public class YourTurnEvent : Event
        {
            public static readonly YourTurnEvent Instance = new();
            public YourTurnEvent() : base(EventType.YourTurn)
            { }
        }

        public class EnemyTurnEvent : Event
        {
            public static readonly EnemyTurnEvent Instance = new();
            public EnemyTurnEvent() : base(EventType.EnemyTurn)
            { }
        }

        public class MoveEvent : Event
        {
            public Position From {  get; set; }
            public Position To { get; set; }
            public MoveEvent() : base(EventType.Move)
            {}
            public MoveEvent(MoveAction action) : this()
            {
                From = action.From;
                To = action.To;
            }
        }

        public class RemoveEvent : Event
        {
            public Position Position { get; set; }
            public RemoveEvent() : base(EventType.Remove) { }
        }

        public class EmoteEvent : Event
        {
            public int EmotionID { get; set; }
            public EmoteEvent() : base(EventType.Emote) { }
            public EmoteEvent(EmoteAction action) : this()
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

        public class Action
        {

            public ActionType Type { get; set; }
            public Action(ActionType type)
            {
                Type = type;
            }
            public Action() { }
        }

        public class DisconnectAction : Action
        {
            public static readonly DisconnectAction Instance = new();
            public DisconnectAction() : base(ActionType.Disconnect) { }
        }

        public class ConnectAction : Action
        {
            public ConnectAction() : base(ActionType.Connect)
            { }
            public string? Login {  get; set; }
            public string? Password {  get; set; }
        }

        public class MoveAction : Action
        {
            public MoveAction() : base(ActionType.Move) { }
            public Position From { get; set; }
            public Position To { get; set; }
        }
        public class EmoteAction : Action
        {
            public int EmotionID { get; set; }
            public EmoteAction() : base(ActionType.Emote) { }
        }
        public class SurrenderAction : Action
        {
            public static readonly SurrenderAction Instance = new();
            public SurrenderAction() : base(ActionType.Surrender) { }
        }

        public class RequestForGameAction : Action
        {
            public static readonly RequestForGameAction Instance = new();
            public RequestForGameAction() : base(ActionType.RequestForGame) { }
        }
    }
}
