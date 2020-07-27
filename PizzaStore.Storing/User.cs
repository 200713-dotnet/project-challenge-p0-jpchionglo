using System;
using System.Collections.Generic;

namespace PizzaStore.Storing
{
    public partial class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public int LoginId { get; set; }

        public virtual Login Login { get; set; }
    }
}
