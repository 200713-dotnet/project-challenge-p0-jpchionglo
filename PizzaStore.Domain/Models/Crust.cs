namespace PizzaStore.Domain.Models
{
  public class Crust
  {
    public string Name {get;set;}

    public Crust(){
      Name = "";
    }

    public Crust(string n){
      Name = n;
    }

  }
}