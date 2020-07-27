using System;
using System.Collections.Generic;

namespace PizzaStore.Storing
{
    public partial class Order
    {
        public Order()
        {
            Order1 = new HashSet<Order1>();
            Pizza = new HashSet<Pizza>();
        }

        public int OrderId { get; set; }
        public string DateOrdered { get; set; }
        public bool? Placed { get; set; }
        public bool? Completed { get; set; }

        public virtual ICollection<Order1> Order1 { get; set; }
        public virtual ICollection<Pizza> Pizza { get; set; }
    }
}
