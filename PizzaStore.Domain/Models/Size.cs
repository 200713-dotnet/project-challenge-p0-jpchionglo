namespace PizzaStore.Domain.Models
{
  public class Size
  {
    public string pSize {get;set;}

    public Size(){
      pSize = "";
    }

    public Size(string s){
      pSize = s;
    }

  }
}