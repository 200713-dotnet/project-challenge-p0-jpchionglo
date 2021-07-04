namespace PizzaStore.Domain.Models
{
  public class Topping
  {
    //Not good, hardcoded toppings
    // public enum Meats
    // {
    //   Pepperoni,
    //   Ham,
    //   Sausage,
    //   Chicken,
    //   Bacon
    // }

    // public enum Veggies
    // {
    //   Spinach,
    //   Mushroom,
    //   Pineapple,
    //   Onion,
    //   GreenPepper
    // }

    public string Name {get;set;}

    public Topping(){
      Name = "";
    }

    public Topping(string n){
      Name = n;
    }

    public override bool Equals(object obj){

      if (obj == null || !this.GetType().Equals(obj.GetType()))
      {
         return false;
      }
      else {
          Topping topping = (Topping) obj;
          if (Name.Equals(topping.Name)){
            return true;
          }
      }

      return false;

    }

  }
}