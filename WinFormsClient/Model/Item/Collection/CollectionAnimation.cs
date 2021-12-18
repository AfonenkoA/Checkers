namespace WinFormsClient.Model.Item;

public sealed class CollectionAnimation : VisualAnimation, ISelectable
{

    public CollectionAnimation(VisualAnimation animation, bool isSelected) : base(animation)
    {
        IsSelected = isSelected;
    }

    public bool IsSelected { get; }
}