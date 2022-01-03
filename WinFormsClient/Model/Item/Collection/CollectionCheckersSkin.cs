﻿namespace WinFormsClient.Model.Item.Collection;

public sealed class CollectionCheckersSkin : VisualCheckersSkin, ISelectable
{

    public CollectionCheckersSkin(VisualCheckersSkin skin, bool isSelected) : base(skin)
    {
        IsSelected = isSelected;
    }
    public bool IsSelected { get; }
}