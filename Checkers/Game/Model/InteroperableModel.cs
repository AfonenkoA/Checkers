namespace Checkers.Game.Model;

public abstract class InteroperableModel : GameModel
{
    public abstract void Move(MoveAction a);
    public abstract void Emote(EmoteAction a);
    public abstract void Surrender(SurrenderAction a);
}