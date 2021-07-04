using System;
using System.Collections.Generic;

namespace PizzaStore.Storing
{
    public partial class Order
    {
        public Order()
        {
            OrderJunction = new HashSet<OrderJunction>();
            PizzaJunction = new HashSet<PizzaJunction>();
        }

        public int OrderId { get; set; }
        public string DateOrdered { get; set; }
        public int? UserId { get; set; }
        public int? StoreId { get; set; }
        public bool? Placed { get; set; }
        public bool? Completed { get; set; }

        public virtual Store Store { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<OrderJunction> OrderJunction { get; set; }
        public virtual ICollection<PizzaJunction> PizzaJunction { get; set; }
    }
}
