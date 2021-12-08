using System;
using System.Linq;
using Checkers.Game.Model;
using static System.DateTime;
using static Checkers.Game.Model.Side;

namespace Checkers.Game.Server;

internal class Match : InteroperableModel,IDisposable
{
    private const string MoveExceptionMessage = "Move action out of turn";

    protected readonly IPlayer _black;
    protected readonly IPlayer _white;
    private Side _turn = White;
    private readonly DateTime _startTime = Now;
    private readonly IEmotionRepository _repository;

    private TimeSpan Time => Now - _startTime;

    private void Subscribe(IPlayer p)
    {
        p.OnEmote += Emote;
        p.OnMove += Move;
        p.OnSurrender += Surrender;
        OnTurn += p.Send;
        OnEmote += p.Send;
        OnException += p.Send;
        OnMove += p.Send;
        OnGameStart += p.Send;
        OnGameEnd += p.Send;
    }

    private void Unsubscribe(IPlayer p)
    {
        p.OnEmote -= Emote;
        p.OnMove -= Move;
        p.OnSurrender += Surrender;
        OnTurn -= p.Send;
        OnEmote -= p.Send;
        OnException -= p.Send;
        OnMove -= p.Send;
        OnGameStart -= p.Send;
        OnGameEnd -= p.Send;
    }

    internal Match(IEmotionRepository repository, IPlayer black, IPlayer white)
    {
        _black = black;
        _white = white;
        _repository = repository;
        Subscribe(black);
        Subscribe(white);
    }


    internal void Run()
    {
        var blackPlayer = _black.PlayerData;
        var whitePlayer = _white.PlayerData;
        var start = new GameStartEvent
        {
            Start = _startTime,
            Black = blackPlayer,
            White = whitePlayer
        };
        _black.Send(new YourSideEvent {Side = Black,Time = Time});
        _white.Send(new YourSideEvent {Side = White,Time = Time});
        SetTurn(White);
        Start(start);
    }

    public override void Move(MoveAction a)
    {
        if (_turn != a.Side)
            Exception(new ExceptionEvent
            {
                Time = Time,
                Message = MoveExceptionMessage
            });
        Move(new MoveEvent
        {
            Time = Time,
            From = a.Form,
            To = a.To,
            Side = a.Side
        });
        SetTurn(!Board.GetAvailableMove(a.To).Any() ? a.Side : a.Side.Inverse());
    }

    public override void Emote(EmoteAction a)
    {
        Emote(new EmoteEvent
        {
            Side = a.Side,
            Emotion = _repository.GetEmotion(a.Id),
            Time = Time
        });
    }

    public override void Surrender(SurrenderAction a)
    {
        End(new GameEndEvent
        {
            Duration = Time,
            Start = _startTime,
            Winner = a.Side.Inverse(),
            WinReason = WinReason.Surrender
        });
    }
    
    private void SetTurn(Side side)
    {
        _turn = side;
        TurnEvent e;
        if (side == White)
            e = new TurnEvent
            {
                Side = White,
                Time = Time
            };
        else
            e = new TurnEvent
            {
                Side = Black,
                Time = Time
            };
        Turn(e);
    }

    public void Dispose()
    {
        Unsubscribe(_black);
        Unsubscribe(_white);
    }
}