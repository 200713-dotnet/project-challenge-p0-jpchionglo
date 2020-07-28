namespace PizzaStore.Domain.Models
{
  public class Name
  {
    public string firstName{get;set;}

    public string lastName{get;set;}
    
    public Name(){

      firstName = "";
      lastName = "";

    }

    public Name(string fname, string lname){

      firstName = fname;
      lastName = lname;

    }
  }
}