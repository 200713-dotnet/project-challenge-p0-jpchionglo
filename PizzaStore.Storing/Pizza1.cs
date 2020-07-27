using System;
using System.Collections.Generic;

namespace PizzaStore.Storing
{
    public partial class Pizza1
    {
        public Pizza1()
        {
            Pizza = new HashSet<Pizza>();
        }

        public int PizzaId { get; set; }
        public int? CrustId { get; set; }
        public int? SizeId { get; set; }
        public string Name { get; set; }

        public virtual Crust Crust { get; set; }
        public virtual Size Size { get; set; }
        public virtual ICollection<Pizza> Pizza { get; set; }
    }
}
