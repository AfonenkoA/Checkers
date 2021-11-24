using System.Collections.Generic;

#nullable disable

namespace Checkers.Data.Old
{
    public sealed class PictureOption
    {
        public PictureOption()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
