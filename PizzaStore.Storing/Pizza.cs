using System;
using System.Collections.Generic;

namespace PizzaStore.Storing
{
    public partial class Pizza
    {
        public Pizza()
        {
            PizzaJunction = new HashSet<PizzaJunction>();
            PizzaTopping = new HashSet<PizzaTopping>();
        }

        public int PizzaId { get; set; }
        public int? CrustId { get; set; }
        public int? SizeId { get; set; }
        public string Name { get; set; }

        public virtual Crust Crust { get; set; }
        public virtual Size Size { get; set; }
        public virtual ICollection<PizzaJunction> PizzaJunction { get; set; }
        public virtual ICollection<PizzaTopping> PizzaTopping { get; set; }
    }
}
