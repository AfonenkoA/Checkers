#nullable disable

namespace Checkers.Data.Old;

public class Item
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ItemId { get; set; }

    public virtual ItemOption ItemNavigation { get; set; }
    public virtual User User { get; set; }
}