using System;
using System.Collections.Generic;

namespace PizzaStore.Storing
{
    public partial class Size
    {
        public Size()
        {
            Pizza1 = new HashSet<Pizza1>();
        }

        public int SizeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Pizza1> Pizza1 { get; set; }
    }
}
