using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Entity;

namespace WinFormsClient.Model
{

    internal class NamedItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }


        public DetailedItemModel(DetailedItem item)
        {

        }
    }
    internal class DetailedItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }


        public DetailedItemModel(DetailedItem item)
        {

        }
    }
}
