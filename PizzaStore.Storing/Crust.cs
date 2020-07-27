using System;
using System.Collections.Generic;

namespace PizzaStore.Storing
{
    public partial class Crust
    {
        public Crust()
        {
            Pizza1 = new HashSet<Pizza1>();
        }

        public int CrustId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Pizza1> Pizza1 { get; set; }
    }
}
