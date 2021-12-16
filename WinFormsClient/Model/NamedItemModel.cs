﻿using Common.Entity;

namespace WinFormsClient.Model;

public class VisualNamedItem
{
    internal int Id { get; }
    internal string Name { get; }
    internal Image Image { get; }

    public VisualNamedItem(NamedItem item, Image img)
    {
        Id = item.Id;
        Name = item.Name;
        Image = img;
    }
}