using System;
using System.Collections.Generic;

namespace PizzaStore.Storing
{
    public partial class Order1
    {
        public int StoreOrderId { get; set; }
        public int? StoreId { get; set; }
        public int? OrderId { get; set; }

        public virtual Order Order { get; set; }
        public virtual Store Store { get; set; }
    }
}
