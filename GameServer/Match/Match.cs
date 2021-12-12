﻿using GameModel;
using GameServer.Repository;
using static System.DateTime;

namespace GameServer.Match;

internal class Match : InteroperableModel,IDisposable
{
    private const string MoveExceptionMessage = "Move action out of turn";

    protected readonly IPlayer Black;
    protected readonly IPlayer White;
    private Side _turn = Side.White;
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
        Black = black;
        White = white;
        _repository = repository;
        Subscribe(black);
        Subscribe(white);
    }


    internal void Run()
    {
        var blackPlayer = Black.PlayerData;
        var whitePlayer = White.PlayerData;
        var start = new GameStartEvent
        {
            Start = _startTime,
            Black = blackPlayer,
            White = whitePlayer
        };
        Black.Send(new YourSideEvent {Side = Side.Black,Time = Time});
        White.Send(new YourSideEvent {Side = Side.White,Time = Time});
        SetTurn(Side.White);
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
            From = a.From,
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
        if (side == Side.White)
            e = new TurnEvent
            {
                Side = Side.White,
                Time = Time
            };
        else
            e = new TurnEvent
            {
                Side = Side.Black,
                Time = Time
            };
        Turn(e);
    }

    public void Dispose()
    {
        Unsubscribe(Black);
        Unsubscribe(White);
    }
}