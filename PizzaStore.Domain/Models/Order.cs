using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaStore.Domain.Models
{

  public class Order
  {

    public List<Pizza> Pizzas { get; set;}

    public DateTime DateOrdered {get; private set;}

    public bool Placed {get; private set;}

    public bool Completed {get; private set;}

    public Order(){

      Pizzas = new List<Pizza>();
      Placed = false;
      Completed = false;
      DateOrdered = new DateTime();

    }

    public void PlaceOrder()
    {

      Placed = true;
      DateOrdered = DateTime.Now;
    }

    public void CompleteOrder()
    {

      Completed = true;

    }


    public Pizza CreatePizza()
    {
      if (Pizzas.Count >= 50){
        throw new Exception("Cart overload! Tried to create a new pizza but there is already a maximum amount of 50.");
      } 
      else if (getPrice() > 246) {
        throw new Exception("Cart overload! Tried to create a new pizza but it would exceed the price of 250.");
      }
      else {
        Pizza za = new Pizza();
        Pizzas.Add(za);
        return za;
      }
    }

    public double getPrice(){

      double total = 0;

      foreach (Pizza p in Pizzas){

        total += p.getPrice();

      }

      return total;

    }

    public void removePizza(int pizzaIndex){

      if (pizzaIndex < 0 || pizzaIndex > Pizzas.Count - 1){

        throw new Exception("Pizza out of bounds! Tried to remove a pizza index not in the range of pizzas.");

      }
      else {
        
        Pizzas.RemoveAt(pizzaIndex);

      }

    }

    //Might not use and just directly edit pizza using reference in the console app
    public void changePizza(int pizzaIndex, Pizza pizza){

      if (pizzaIndex < 0 || pizzaIndex > Pizzas.Count - 1){

        throw new Exception("Pizza out of bounds! Tried to edit a pizza not in the range of pizzas.");

      }
      else {

        removePizza(pizzaIndex);
        Pizzas.Insert(pizzaIndex, pizza);

      }
      


    }

    public string getPizzaList(){

      StringBuilder sb = new StringBuilder();

      for(int i = 0; i < Pizzas.Count; i++){

        sb.Append($"-----Pizza #{i+1} ({Pizzas[i].Name})-----\n");
        sb.Append($"Size: {Pizzas[i].Size.pSize}  Crust: {Pizzas[i].Crust.Name}\n");
        sb.Append($"Toppings:\n");
        foreach (Topping top in Pizzas[i].Toppings){

          sb.Append($"  - {top.Name}\n");

        }
        sb.Append($"Price: ${Pizzas[i].getPrice()}\n");

      }

      sb.Append("--------------------\n");

      return sb.ToString();

    } 

    public string getPizzaDetails(int pizzaIndex){

      if (pizzaIndex < 0 || pizzaIndex > Pizzas.Count - 1){

        throw new Exception("Pizza out of bounds! Tried to view pizza details of a pizza not in the list.");

      }
      else {

        StringBuilder sb = new StringBuilder();

        sb.Append($"-----Pizza #{pizzaIndex} ({Pizzas[pizzaIndex].Name})-----\n");
        sb.Append($"Size: {Pizzas[pizzaIndex].Size}  Crust: {Pizzas[pizzaIndex].Crust}\n");
        sb.Append($"Toppings:\n");
        foreach (Topping top in Pizzas[pizzaIndex].Toppings){

          sb.Append($"  - {top.Name}\n");

        }
        sb.Append($"Price: {Pizzas[pizzaIndex].getPrice()}\n");
        sb.Append("--------------------\n");
        
        return sb.ToString();

      }

      

    }

  }

}