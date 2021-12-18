using WinFormsClient.Model.Item;

namespace WinFormsClient.Model;

public sealed class Collection
{
    public IEnumerable<CollectionAnimation> Animations;
    public IEnumerable<CollectionCheckersSkin> Skins;

    public Collection(IEnumerable<CollectionAnimation> animations,
        IEnumerable<CollectionCheckersSkin> skins)
    {
        Animations = animations;
        Skins = skins;
    }
}