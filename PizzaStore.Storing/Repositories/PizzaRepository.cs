using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaStore.Storing.Repositories{


  public class PizzaRepository{

    private PizzaStoreDBContext db = new PizzaStoreDBContext();

    public void CreatePizza(Domain.Models.Pizza pizza){

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
    
    public List<Domain.Models.Store> ReadAllStore(){

      List<Domain.Models.Store> stores = new List<Domain.Models.Store>();

      foreach(var item in db.Store.ToList()){

        stores.Add(new Domain.Models.Store(){

          Name = item.Name,
          Orders = ReadOrdersUsingStoreID(item.StoreId,item.Name)

        });

      }

      return stores;

    }

    public List<Domain.Models.Order> ReadOrdersUsingStoreID(int sID, string storeName){
      
      List<Domain.Models.Order> orders = new List<Domain.Models.Order>();

      //Find store orders
      foreach(var ord in db.Order.ToList()){

        if (sID == ord.StoreId){

          //Make Pizza, then add it to the List of Pizzas
          //Find order pizzas using junction table Order.Pizza
          foreach(var p in db.PizzaJunction.ToList()){

            if (ord.OrderId == p.OrderId){
              List<PizzaStore.Domain.Models.Pizza> pizzas = new List<PizzaStore.Domain.Models.Pizza>();

              //Find Pizza referenced by Order.Pizza using Pizza.Pizza table
              foreach(var p1 in db.Pizza.ToList()){
                if (p.PizzaId == p1.PizzaId){

                  //Create new Pizza to add to order
                  PizzaStore.Domain.Models.Pizza piz = new PizzaStore.Domain.Models.Pizza();

                  piz.Name = p1.Name;

                  //Add Crust to Pizza
                  foreach (var c in db.Crust.ToList()){
                    if (p1.CrustId == c.CrustId){
                      piz.Crust = new PizzaStore.Domain.Models.Crust(c.Name);
                      break;
                    }
                  }  

                  //Add Size to Pizza
                  foreach (var s in db.Size.ToList()){
                    if (p1.SizeId == s.SizeId){
                      piz.Size = new PizzaStore.Domain.Models.Size(s.Name);
                      break;
                    }
                  }

                  //Find Toppings
                  foreach (var pt in db.PizzaTopping.ToList()){
                    if (pt.PizzaId == p1.PizzaId){

                      //Add Toppings
                      foreach(var top in db.Topping.ToList()){
                        if (top.ToppingId == pt.ToppingId){
                          piz.addTopping(new PizzaStore.Domain.Models.Topping(top.Name));
                          break;
                        }
                      }

                    }

                  }

                  pizzas.Add(piz);

                }

              }

              orders.Add(new Domain.Models.Order(pizzas, Convert.ToDateTime(ord.DateOrdered),(bool)ord.Placed,(bool)ord.Completed,new Domain.Models.Store(){Name = storeName}));

            }

          }

        }
        
      }

      return orders;

    }

    public List<Domain.Models.Pizza> ReadAllPizza(){

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