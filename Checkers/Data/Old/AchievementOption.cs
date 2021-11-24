using System.Collections.Generic;

#nullable disable

namespace Checkers.Data.Old
{
    public sealed class AchievementOption
    {
        public AchievementOption()
        {
            Achievements = new HashSet<Achievement>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Achievement> Achievements { get; set; }
    }
}
