using GameModel;

namespace GameClient;

public sealed class ClientGameModel : InteroperableModel
{

    public override void Move(MoveAction a)
    {
        throw new NotImplementedException();
    }

    public override void Emote(EmoteAction a)
    {
        throw new NotImplementedException();
    }

    public override void Surrender(SurrenderAction a)
    {
        throw new NotImplementedException();
    }

}