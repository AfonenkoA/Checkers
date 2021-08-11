using System.Collections.Generic;

#nullable disable

namespace Checkers.Data
{
    public partial class EventOption
    {
        public EventOption()
        {
            GamesProgresses = new HashSet<GamesProgress>();
        }

        public int Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<GamesProgress> GamesProgresses { get; set; }
    }
}
