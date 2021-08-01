using System;
using Checkers.Client;

namespace Checkers.Transmission
{
    public enum RequestType
    {
        GameGetRequest,
        UserCreationRequest,
        UserGetRequest,
        UserLoginRequest,
        UserUpdateRequest,
        UserDeleteRequest,
        UserItemsGetRequest,
        UserItemsUpdateRequest,
        UserAchievementsGetRequest,
        UserFriendsGetRequest,
        UserFriendsUpdateRequest,
        UserGamesGetRequest
    }
    public class Request
    {
        public RequestType Type { get; set; }
        public Request(RequestType type)
        {
            Type = type;
        }
        public Request()
        { }
    }

    public class GameGetRequest : Request
    {
        public int gameID { get; set; }
    }

    public class UserRequest : Request
    {
        public string? Login { get; set; }
        public UserRequest(RequestType type) : base(type)
        {
        }
        public UserRequest() { }
    }

    public class SequreRequest : UserRequest
    {
        public string? Password { get; set; }
        public SequreRequest(RequestType type) : base(type)
        { }
        public SequreRequest() : base()
        { }
    }

    public class UserCreationRequest : SequreRequest
    {
        public string? Email { get; set; }
        public string? Nick { get; set; }
        public UserCreationRequest() : base(RequestType.UserCreationRequest)
        {
        }
        public UserCreationRequest(SequreRequest request) : base(RequestType.UserCreationRequest) 
        {
            Login = request.Login;
            Password= request.Password;
        }
    }

    public class UserLoginRequest : SequreRequest
    {
        public UserLoginRequest() : base(RequestType.UserLoginRequest)
        { }
        public UserLoginRequest(SequreRequest request) : base(RequestType.UserCreationRequest)
        {
            Login = request.Login;
            Password = request.Password;
        }
    }

    public class UserUpdateRequest : SequreRequest
    {
        public string? NewPassword { get; set; }
        public string? NewEmail { get; set; }
        public int? NewPictureID { get; set; }
        public UserUpdateRequest() : base(RequestType.UserUpdateRequest)
        { }
        public UserUpdateRequest(SequreRequest request) : base(RequestType.UserCreationRequest)
        {
            Login = request.Login;
            Password = request.Password;
        }
    }
    public class UserDeleteRequest : SequreRequest
    {
        public UserDeleteRequest() : base(RequestType.UserDeleteRequest) { }
        public UserDeleteRequest(SequreRequest request) : base(RequestType.UserCreationRequest)
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
        public UserItemsUpdateRequest() : base(RequestType.UserItemsUpdateRequest)
        { }
        public UserItemsUpdateRequest(SequreRequest request) : base(RequestType.UserCreationRequest)
        {
            Login = request.Login;
            Password = request.Password;
        }
    }
    public class UserFriendsGetRequest : SequreRequest
    {
        public UserFriendsGetRequest() : base(RequestType.UserFriendsGetRequest)
        { }
        public UserFriendsGetRequest(SequreRequest request) : base(RequestType.UserCreationRequest)
        {
            Login = request.Login;
            Password = request.Password;
        }
    }
    public class UserFriendsUpdateRequest : SequreRequest
    {
        public int? NewFriendID { get; set; }
        public int? DeleteFriendID { get; set; }
        public UserFriendsUpdateRequest() : base(RequestType.UserFriendsUpdateRequest)
        { }
        public UserFriendsUpdateRequest(SequreRequest request) : base(RequestType.UserCreationRequest)
        {
            Login = request.Login;
            Password = request.Password;
        }
    }
    public class UserAchievementsGetRequest : UserRequest
    {
        public UserAchievementsGetRequest() : base(RequestType.UserAchievementsGetRequest)
        { }
    }
    public class UserGamesGetRequest : UserRequest
    {
        public UserGamesGetRequest() : base(RequestType.UserGamesGetRequest)
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
        public TimeOnly ActionTime { get; set; }
        public int ActorID { get; set; }
        public string ActionDescription { get; set; }
    }

    public class UserSequreResonse : Response
    {
        public string? Login { get; set; }
        public string? Password { get; set; }
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

    public class GameGetRespose
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

    public class UserInfoResponse : UserSequreResonse
    {
        public UserInfo Info { get; set; }
        public string? Email { get; set; }
    }
    public class UserLoginResponse : UserInfoResponse
    { }
    public class UserUpdateResponse : UserInfoResponse
    { }
    public class UserItemsResponse : UserSequreResonse
    {
        public int[]? Items { get; set; }
        public int SelectedCheckersID { get; set; }
        public int SelectedAnimationsID { get; set; }
    }

    public class UserItemsGetResponse : UserItemsResponse
    { }
    public class UserItemsUpdateResponse : UserItemsResponse
    { }

    public class UserFriendsResponse : UserSequreResonse
    {
        public int[]? Friends { get; set; }
    }

    public class UserFriendsGetResponse
    { }
    public class UserFriendsUpdateResponse
    { }

    namespace InGame
    {
        public enum EventType
        {
            ConnectionAccept,
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
