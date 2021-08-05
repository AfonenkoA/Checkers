using System;
using System.Collections.Generic;

#nullable disable

namespace Checkers.Data
{
    public partial class PictureOption
    {
        public PictureOption()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
