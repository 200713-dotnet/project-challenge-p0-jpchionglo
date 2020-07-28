using System;
using System.Collections.Generic;

namespace PizzaStore.Storing
{
    public partial class Store
    {
        public Store()
        {
            Order = new HashSet<Order>();
            OrderJunction = new HashSet<OrderJunction>();
        }

        public int StoreId { get; set; }
        public string Name { get; set; }
        public int? LoginId { get; set; }

        public virtual Login Login { get; set; }
        public virtual ICollection<Order> Order { get; set; }
        public virtual ICollection<OrderJunction> OrderJunction { get; set; }
    }
}
