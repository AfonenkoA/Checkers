using WinFormsClient.Model.Item;

namespace WinFormsClient.Model;

public sealed class Collection
{
    public readonly IEnumerable<CollectionAnimation> Animations;
    public readonly IEnumerable<CollectionCheckersSkin> Skins;

    public Collection(IEnumerable<CollectionAnimation> animations,
        IEnumerable<CollectionCheckersSkin> skins)
    {
        Animations = animations;
        Skins = skins;
    }
}