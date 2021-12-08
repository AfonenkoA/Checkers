namespace Checkers.Game.Model;

public interface IGameController
{
    void Move(MoveAction a);
    void Emote(EmoteAction a);
    void Surrender();
}