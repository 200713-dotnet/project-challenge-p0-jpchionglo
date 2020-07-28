using System.Collections.Generic;
using System.Linq;

namespace PizzaStore.Storing.Repositories{


  public class PizzaRepository{

    private PizzaStoreDBContext db = new PizzaStoreDBContext();

    public void Create(Domain.Models.Pizza pizza){

      var newPizza = new Pizza();

      newPizza.Crust = new Crust() {Name = pizza.Crust.Name};
      newPizza.Size = new Size() {Name = pizza.Size.pSize};
      newPizza.Name = pizza.Name;

      db.Pizza.Add(newPizza);
      db.SaveChanges();

    }

    public void AddOrder(Domain.Models.Order order, int storeID, int userID){

      var newOrder = new Order();
      newOrder.DateOrdered = order.DateOrdered.ToString();
      newOrder.UserId = userID;
      newOrder.StoreId = storeID;
      newOrder.Placed = order.Placed;
      newOrder.Completed = order.Completed;
      db.Order.Add(newOrder);
      db.SaveChanges();

      var newOrderJunction = new OrderJunction();
      newOrderJunction.Order = newOrder;
      newOrderJunction.StoreId = storeID;
      

      foreach (var pizza in order.Pizzas){
      
        var newPizza = new Pizza();

        newPizza.Crust = new Crust() {Name = pizza.Crust.Name};
        newPizza.Size = new Size() {Name = pizza.Size.pSize};
        newPizza.Name = pizza.Name;

        db.Pizza.Add(newPizza);
        db.SaveChanges();

        foreach (var top in pizza.Toppings){

          foreach (var dbtop in db.Topping.ToList()){

            if (top.Name.Equals(dbtop.Name)){

              PizzaTopping ptop = new PizzaTopping();
              ptop.PizzaId = newPizza.PizzaId;
              ptop.ToppingId = dbtop.ToppingId;
              db.PizzaTopping.Add(ptop);
              db.SaveChanges();

            }

          }

          // PizzaTopping ptop = new PizzaTopping();
          // ptop.Pizza = newPizza;
          // ptop.Topping = new Topping() {Name = top.Name};
          // db.PizzaTopping.Add(ptop);

        }

        var pj = new PizzaJunction();
        pj.Order = newOrder;
        pj.Pizza = newPizza;
        db.PizzaJunction.Add(pj);
        db.SaveChanges();

      }

      db.SaveChanges();

    }

    public List<Domain.Models.Pizza> ReadAll(){

      List<Domain.Models.Pizza> pizzas = new List<Domain.Models.Pizza>();

      foreach(var item in db.Pizza.ToList()){
        
        pizzas.Add(new Domain.Models.Pizza(){

          Name = item.Name,
          Crust = new Domain.Models.Crust() {Name = item.Crust.Name},
          Size = new Domain.Models.Size() {pSize = item.Size.Name}

        });

      }

      return pizzas;

    }

    public void Update(){

      

    }

    //public void Delete();


  }


}