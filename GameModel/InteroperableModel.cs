namespace GameModel;

public abstract class InteroperableModel : Model
{
    public abstract void Move(MoveAction a);
    public abstract void Emote(EmoteAction a);
    public abstract void Surrender(SurrenderAction a);
}