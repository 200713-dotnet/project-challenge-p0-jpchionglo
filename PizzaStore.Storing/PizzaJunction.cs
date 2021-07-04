using System;
using System.Collections.Generic;

namespace PizzaStore.Storing
{
    public partial class PizzaJunction
    {
        public int PizzaOrderId { get; set; }
        public int? OrderId { get; set; }
        public int? PizzaId { get; set; }

        public virtual Order Order { get; set; }
        public virtual Pizza Pizza { get; set; }
    }
}
