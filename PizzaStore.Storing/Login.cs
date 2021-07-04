using System;
using System.Collections.Generic;

namespace PizzaStore.Storing
{
    public partial class Login
    {
        public Login()
        {
            Store = new HashSet<Store>();
            User = new HashSet<User>();
        }

        public int LoginId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Store> Store { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}
