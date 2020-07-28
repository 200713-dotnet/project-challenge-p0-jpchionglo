using System;
using System.Collections.Generic;

namespace PizzaStore.Storing
{
    public partial class User
    {
        public User()
        {
            Order = new HashSet<Order>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int LoginId { get; set; }

        public virtual Login Login { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}
