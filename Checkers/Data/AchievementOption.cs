using System.Collections.Generic;

#nullable disable

namespace Checkers.Data
{
    public partial class AchievementOption
    {
        public AchievementOption()
        {
            Achievements = new HashSet<Achievement>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Achievement> Achievements { get; set; }
    }
}
