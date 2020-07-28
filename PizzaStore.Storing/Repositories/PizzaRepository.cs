using System.Collections.Generic;
using System.Linq;

namespace PizzaStore.Storing.Repositories{


  public class PizzaRepository{

    private PizzaStoreDBContext db = new PizzaStoreDBContext();

    public void Create(Domain.Models.Pizza pizza){

      var newPizza = new Pizza1();

      newPizza.Crust = new Crust() {Name = pizza.Crust.Name};
      newPizza.Size = new Size() {Name = pizza.Size.pSize};
      newPizza.Name = pizza.Name;

      db.Pizza1.Add(newPizza);
      db.SaveChanges();

    }

    public List<Domain.Models.Pizza> ReadAll(){

      List<Domain.Models.Pizza> pizzas = new List<Domain.Models.Pizza>();

      foreach(var item in db.Pizza1.ToList()){
        
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