using System.Collections.Generic;
using System.Text;

namespace PizzaStore.Domain.Models
{

  public class Pizza
  {

    public List<Topping> Toppings { get;}

    public Crust Crust { get; set;}

    public Size Size { get; set;}

    public string Name {get;set;}

    public Pizza (){

      Toppings = new List<Topping>();
      Crust = new Crust("Hand Tossed");
      Size = new Size("Medium");
      Name = "Custom";

    }

    public Pizza(List<Topping> toppings, Crust crust, Size size, string name){

      if (toppings.Count >= 6){
        //Should never get here unless failed to handle in console app
        throw new System.Exception("Topping Overload! Too many toppings added to pizza.");
      }
      else {
        Toppings = toppings;
        Crust = crust;
        Size = size;
        Name = name;
      }

    }

    public void addTopping(Topping top){

      if (Toppings.Count >= 5){
        //Should never get here unless failed to handle in console app
        throw new System.Exception("Topping Overload! Attempted to add a topping to a pizza with already 5 toppings.");
      } 
      else {
        Toppings.Add(top);
      }

    }

    public double getPrice(){

      double total = 0;

      //Size
      switch (Size.pSize){

        case "small":
          total += 4;
          break;
        
        case "medium":
          total += 7;
          break;
        
        case "large":
          total += 9;
          break;

        default:
          break;

      }

      //Crust
      switch (Crust.Name){

        case "stuffed":
          total += 0.75;
          break;

        default:
          break;

      }

      //Toppings
      switch (Toppings.Count){
        //Should never hit case 0 or 1, 2 toppings required on every pizza
        case 0:
          break;

        case 1:
          total += 0.5;
          break;
        
        case 2:
          total += 0.75;
          break;
        
        case 3:
          total += 1;
          break;

        // Toppings.Count > 3
        default:
          total += 1 + 0.5*(Toppings.Count - 3);
          break;

      }

      return total;

    }

    public string getDetails(){

      StringBuilder sb = new StringBuilder();

      sb.Append($"Size: {Size.pSize}  Crust: {Crust.Name}\n");
      sb.Append($"Toppings:\n");
      foreach (Topping top in Toppings){

        sb.Append($"  - {top.Name}\n");

      }
      sb.Append($"Price: {getPrice()}\n");
      
      return sb.ToString();

    }

  }

}