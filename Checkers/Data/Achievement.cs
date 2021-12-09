#nullable disable

namespace Checkers.Data;

public class Achievement
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int AchievementId { get; set; }

    public virtual AchievementOption AchievementNavigation { get; set; }
    public virtual User User { get; set; }
}