using System;
using System.Collections.Generic;

#nullable disable

namespace Checkers.Data
{
    public partial class Item
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ItemId { get; set; }

        public virtual ItemOption ItemNavigation { get; set; }
        public virtual User User { get; set; }
    }
}
