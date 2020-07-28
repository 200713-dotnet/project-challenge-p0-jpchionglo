using System;
using System.Collections.Generic;
using PizzaStore.Domain.Models;
using System.Linq;

namespace PizzaStore.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new PizzaStore.Storing.PizzaStoreDBContext();

            Console.WriteLine("Welcome to the PizzaStore Application!");
            bool running = true;
            
            while (running){

              Console.WriteLine("Please select an option:");
              Console.WriteLine("1. User Login");
              Console.WriteLine("2. Store Login");
              Console.WriteLine("3. Exit");

              string input = Console.ReadLine();
              
              switch (input){

                case "1":

                  User user = new User();
                  Console.WriteLine("Enter the username: ");
                  string username = Console.ReadLine();
                  Console.WriteLine("Enter the password: ");
                  string password = Console.ReadLine();
                  int uID = 0;
                  foreach (var un in db.Login.ToList()){
                  
                    if (un.Username.Equals(username) && un.Password.Equals(password)){
                      uID = un.LoginId;
                      
                      foreach (var us in db.User.ToList()){

                        if (uID == us.LoginId){
                          user.Name.firstName 
                        }

                      }

                    }
                  
                  }


                  user = UserConsole(user, uID, db);
                  running = false;
                  break;

                case "2":
                  Store store = new Store();
                  //store login
                  //StoreConsole(store);
                  running = false;
                  break;

                case "3":
                  running = false;
                  break;
                
                default:
                  break;

              }

            }

            
        }

        public static User UserConsole(User user, int uID , PizzaStore.Storing.PizzaStoreDBContext db){

          Store location = new Store();
          bool running = true;

          Console.WriteLine($"\n------------------------\nWelcome, {user.Name.firstName}!\n------------------------\n");
          Console.WriteLine($"Current Location: {location.Name}");
          
          if (location.Name.Equals("None")){
            location = SelectStore(new Store(), db);
            user = updateUser(user, uID, db, location);
          }

          while(running){
            Console.WriteLine("\n=====Main Menu=====");
            Console.WriteLine($"Current Location: {location.Name}\n");
            Console.WriteLine("1. List Locations");
            Console.WriteLine("2. Select Location");
            Console.WriteLine("3. View Pricing Info");
            Console.WriteLine("4. Place an Order");
            Console.WriteLine("5. See Order History");
            Console.WriteLine("6. Exit");

            string input = Console.ReadLine();

            switch (input){
              
              case "1":
              
                for(int j = 0; j < db.Store.ToList().Count; j++){

                  Console.WriteLine($"{j+1}. {db.Store.ToList()[j]}");

                }

                break;

              case "2":

                location = SelectStore(location, db);
                user = updateUser(user, uID, db, location);
                break;

              case "3":

                Console.WriteLine("===Pricing Information===");
                Console.WriteLine("Preset Pizzas are also priced based on this information\n");

                Console.WriteLine("Size Base Prices:");
                Console.WriteLine(" Small: $4");
                Console.WriteLine(" Medium: $7");
                Console.WriteLine(" Large: $9\n");

                Console.WriteLine("Topping Prices:");
                Console.WriteLine(" 1 Topping: $0.50");
                Console.WriteLine(" 2 Toppings: $0.75");
                Console.WriteLine(" 3 Toppings: $1");
                Console.WriteLine(" 4+ Toppings: $1 + $0.50 per additional topping after 3\n");

                Console.WriteLine("Crust Prices:");
                Console.WriteLine(" Hand Tossed: Free");
                Console.WriteLine(" Thin: Free");
                Console.WriteLine(" Stuffed: $0.75\n");

                break;

              case "4":

                PlaceOrder(user, uID, db);
                break;
              
              case "5":

                int i;
                for (i = 0; i < user.Orders.Count; i++){
                  Console.WriteLine($"Order #{i+1}:");
                  Console.WriteLine($"  Date Ordered: {user.Orders[i].DateOrdered.ToString()}");
                  Console.WriteLine($"  Number of Pizzas: {user.Orders[i].Pizzas.Count}");
                  Console.WriteLine($"  Store: {user.Orders[i].store.Name}\n");
                  
                }

                bool running2 = true;

                while(running2){

                  Console.WriteLine($"Type the order number you wish to view or type back to go back.");
                  
                  input = Console.ReadLine();

                  switch (input){

                    case "back":
                      running2 = false;
                      break;
                    
                    default:

                      Console.WriteLine($"-- Store: {user.Orders[Convert.ToInt32(input) - 1].store.Name} --");
                      Console.WriteLine(user.Orders[Convert.ToInt32(input) - 1].getPizzaList());
                      break;

                  }

                }

                break;

              case "6":
                running = false;
                break;

              default:
                break;

            }

          }

          return user;

        }

        public void StoreConsole(Store store){



        }

        // public Store SelectStore(){

        //   Console.WriteLine("Please Select a Store:");

        //   for (int i = 0; i < db.Store.ToList(); i++){
        //     Console.WriteLine($"{i}. {db.Store.ToList()[i].Name}");
        //   }

        //   int input = Convert.ToInt32(Console.ReadLine());

        //   return db.Store.ToList()[input];

        // }

        public static User updateUser(User user, int uID, PizzaStore.Storing.PizzaStoreDBContext db, Store store){

          //Find user
          foreach (var us in db.User.ToList()){

            if (uID == us.LoginId){
              user.Name.firstName = us.Name;
              
              //Find user orders
              foreach(var ord in db.Order.ToList()){
                if (uID == ord.UserId){
                  //Make Pizza, then add it to the List of Pizzas
                  //Find order pizzas using junction table Order.Pizza
                  foreach(var p in db.Pizza.ToList()){

                    if (ord.OrderId == p.OrderId){
                      List<Pizza> pizzas = new List<Pizza>();
                      //Find Pizza referenced by Order.Pizza using Pizza.Pizza table
                      foreach(var p1 in db.Pizza1.ToList()){
                        if (p.PizzaId == p1.PizzaId){

                          //Create new Pizza to add to order
                          Pizza piz = new Pizza();

                          piz.Name = p1.Name;

                          //Add Crust to Pizza
                          foreach (var c in db.Crust.ToList()){
                            if (p1.CrustId == c.CrustId){
                              piz.Crust = new Crust(c.Name);
                              break;
                            }
                          }  

                          //Add Size to Pizza
                          foreach (var s in db.Size.ToList()){
                            if (p1.SizeId == s.SizeId){
                              piz.Size = new Size(s.Name);
                              break;
                            }
                          }

                          //Find Toppings
                          foreach (var pt in db.PizzaTopping.ToList()){
                            if (pt.PizzaId == p1.PizzaId){

                              //Add Toppings
                              foreach(var top in db.Topping.ToList()){
                                if (top.ToppingId == pt.ToppingId){
                                  piz.addTopping(new Topping(top.Name));
                                  break;
                                }
                              }

                            }

                          }

                          pizzas.Add(piz);

                        }

                      }

                      user.Orders.Add(new Order(pizzas, Convert.ToDateTime(ord.DateOrdered),(bool)ord.Placed,(bool)ord.Completed, store));

                    }

                  }

                }
                
              }
              
              
            }

          }

          return user;

        }

        public static Store SelectStore(Store store, PizzaStore.Storing.PizzaStoreDBContext db){

          Console.WriteLine("Choose a Store: ");
          int i;
          for(i = 0; i < db.Store.ToList().Count; i++){

            Console.WriteLine($"{i+1}. {db.Store.ToList()[i].Name}");

          }

          Console.WriteLine($"{i+1}. Cancel");

          int input = Convert.ToInt32(Console.ReadLine());


          if (input < i+1 && input > 0){

           store.Name = db.Store.ToList()[i-1].Name;
            
          }

          return store;

        }

        public static void PlaceOrder(User user, int uID, PizzaStore.Storing.PizzaStoreDBContext db){

          Order order = new Order();
          
          bool running = true;

          while(running){

            Console.WriteLine("=== Editing Order ===");
            Console.WriteLine("1. Choose Preset Pizza");
            Console.WriteLine("2. Create Custom Pizza");
            Console.WriteLine("3. View Order");
            Console.WriteLine("4. Edit Pizza");
            Console.WriteLine("5. Remove Pizza");
            Console.WriteLine("6. Checkout");
            Console.WriteLine("7. Exit (Order will be lost)");

            string input = Console.ReadLine();
                
            switch (input){

              case "1":
                Console.WriteLine("=== Choose Preset Pizza ===");
                Console.WriteLine("1. Meat Lovers (Hand tossed, Pepperoni, Ham, Sausage)");
                Console.WriteLine("2. Fred Special (Hand tossed, Ham, Pineapple)");
                Console.WriteLine("3. Jeremy Special (Thin, Bacon, Pineapple)");
                Console.WriteLine("4. Deluxe (Hand Tossed, Pepperoni, Sausage, Mushroom, Green Pepper, Onion)");

                string input2 = Console.ReadLine();
                Pizza pizza;
                
                switch (input2){

                  case "1": // Meat Lovers
                    if (order.Pizzas.Count < 50){
                      pizza = order.CreatePizza();
                      pizza.Name = "Meat Lover's";
                      pizza.addTopping(new Topping("pepperoni"));
                      pizza.addTopping(new Topping("ham"));
                      pizza.addTopping(new Topping("sausage"));
                      pizza.Crust = new Crust("handtossed");

                      Console.WriteLine("--Choose a size--");
                      Console.WriteLine("1. Small");
                      Console.WriteLine("2. Medium");
                      Console.WriteLine("3. Large");

                      string input5 = Console.ReadLine();

                      switch (input5){

                        case "1":
                          pizza.Size = new Size("small");
                          break;
                        case "2":
                          pizza.Size = new Size("medium");
                          break;
                        case "3":
                          pizza.Size = new Size("large");
                          break;

                      }
                      
                    }
                    else {
                      Console.WriteLine("You have reached the maximum number of pizzas. The pizza has not been added.");
                    }
                    
                    break;

                  case "2": //Fred Special
                    
                    if (order.Pizzas.Count < 50){
                      pizza = order.CreatePizza();
                      pizza.Name = "Fred Special";
                      pizza.addTopping(new Topping("ham"));
                      pizza.addTopping(new Topping("pineapple"));
                      pizza.Crust = new Crust("handtossed");

                      Console.WriteLine("--Choose a size--");
                      Console.WriteLine("1. Small");
                      Console.WriteLine("2. Medium");
                      Console.WriteLine("3. Large");

                      string input6 = Console.ReadLine();

                      switch (input6){

                        case "1":
                          pizza.Size = new Size("small");
                          break;
                        case "2":
                          pizza.Size = new Size("medium");
                          break;
                        case "3":
                          pizza.Size = new Size("large");
                          break;

                      }
                      
                    }
                    else {
                      Console.WriteLine("You have reached the maximum number of pizzas. The pizza has not been added.");
                    }

                    break;

                  case "3": // Jeremy Special
                    
                    if (order.Pizzas.Count < 50){
                      pizza = order.CreatePizza();
                      pizza.Name = "Jeremy Special";
                      pizza.addTopping(new Topping("bacon"));
                      pizza.addTopping(new Topping("pineapple"));
                      pizza.Crust = new Crust("handtossed");

                      Console.WriteLine("--Choose a size--");
                      Console.WriteLine("1. Small");
                      Console.WriteLine("2. Medium");
                      Console.WriteLine("3. Large");

                      string input7 = Console.ReadLine();

                      switch (input7){

                        case "1":
                          pizza.Size = new Size("small");
                          break;
                        case "2":
                          pizza.Size = new Size("medium");
                          break;
                        case "3":
                          pizza.Size = new Size("large");
                          break;

                      }
                      
                    }
                    else {
                      Console.WriteLine("You have reached the maximum number of pizzas. The pizza has not been added.");
                    }

                    break;
                  
                  case "4": // Deluxe
                    
                    if (order.Pizzas.Count < 50){
                      pizza = order.CreatePizza();
                      pizza.Name = "Deluxe";
                      pizza.addTopping(new Topping("bacon"));
                      pizza.addTopping(new Topping("pineapple"));
                      pizza.Crust = new Crust("handtossed");

                      Console.WriteLine("--Choose a size--");
                      Console.WriteLine("1. Small");
                      Console.WriteLine("2. Medium");
                      Console.WriteLine("3. Large");

                      string input8 = Console.ReadLine();

                      switch (input8){

                        case "1":
                          pizza.Size = new Size("small");
                          break;
                        case "2":
                          pizza.Size = new Size("medium");
                          break;
                        case "3":
                          pizza.Size = new Size("large");
                          break;

                      }
                      
                    }
                    else {
                      Console.WriteLine("You have reached the maximum number of pizzas. The pizza has not been added.");
                    }

                    break;
                  
                  default:
                    break;

                }
                break;

              case "2": //Create Custom Pizza
                
                Pizza tempPizza = new Pizza(new List<Topping>(), new Crust("handtossed"), new Size("medium"), "Custom");


                Console.WriteLine("===Current Pizza===");

                tempPizza = EditPizza(tempPizza);

                if (tempPizza.Size.pSize.Equals("") || tempPizza.Crust.Name.Equals("") || tempPizza.Toppings.Count == 0){

                  Console.WriteLine("Custom Pizza creation unsuccessful.");

                } 
                else {

                  order.Pizzas.Add(tempPizza);
                  Console.WriteLine("Custom Pizza creation successful!");

                }
                break;

              case "3": //View Order
                Console.WriteLine(order.getPizzaList());
                Console.WriteLine($"Total: ${order.getPrice()}");
                break;

              case "4": //Edit Pizza

                Console.WriteLine("Choose a pizza to edit:");
                int i;
                for (i = 0; i <order.Pizzas.Count; i++){

                  Console.WriteLine($"{i+1}. {order.Pizzas[i].Name}");

                }

                Console.WriteLine($"{i+1}. Cancel");

                int input3 = Convert.ToInt32(Console.ReadLine());

                if (input3 < i+1 && input3 > 0){

                  order.Pizzas[i-1] = EditPizza(order.Pizzas[i-1]);
                  
                }

                break;

              case "5": //Remove Pizza

                Console.WriteLine("Choose a pizza to remove:");
                int j;
                for (j = 0; j <order.Pizzas.Count; j++){

                  Console.WriteLine($"{j+1}. {order.Pizzas[j].Name}");

                }

                Console.WriteLine($"{j+1}. Cancel");

                int input4 = Convert.ToInt32(Console.ReadLine());

                //j = number of pizzas
                if (input4-1 < j && input4 > 0){

                  order.Pizzas.RemoveAt(input4-1);
                  Console.WriteLine($"Pizza #{input4} removed.");

                }

                break;

              case "6": //Checkout

                

                if (order.Pizzas.Count == 0){
                  Console.WriteLine("Order was empty, so it was canceled.");
                } 
                else {
                  
                  //Add to database
                  //addOrderToDatabase(uID, order, db);

                  order.PlaceOrder();
                  user.Orders.Add(order);
                  
                }
                running = false;
                break;
            
              case "7": //Exit

                Console.WriteLine("Order Canceled.");
                running = false;
                break;
                
              default:
                break;

            }

          }

        }

        public static void addOrderToDataBase(int uID, Order order, PizzaStore.Storing.PizzaStoreDBContext db){

          

        }

        public static Pizza EditPizza(Pizza p){

          List<Topping> toppings = new List<Topping>();
          toppings.AddRange(p.Toppings);
          Crust crust = new Crust(p.Crust.Name);
          Size size = new Size(p.Size.pSize);
          string name = p.Name;
          
          Pizza pizza = new Pizza(toppings, crust, size, name);

          while(true){
            
            Console.WriteLine(pizza.getDetails());

            Console.WriteLine($"== Editing ");
            Console.WriteLine("1. Add Topping");
            Console.WriteLine("2. Remove Topping");
            Console.WriteLine("3. Change Size");
            Console.WriteLine("4. Change Crust");
            Console.WriteLine("5. Cancel Changes");
            Console.WriteLine("6. Save Changes");

            string input = Console.ReadLine();

            switch (input){

              case "1": // Add Topping

                if (pizza.Toppings.Count >= 5){

                  Console.WriteLine("You cannot add more toppings (Max 5).");

                }

                else {

                  Console.WriteLine("Choose a topping:");

                  Console.WriteLine("- Meat");
                  Console.WriteLine(" 1. Pepperoni");
                  Console.WriteLine(" 2. Ham");
                  Console.WriteLine(" 3. Sausage");
                  Console.WriteLine(" 4. Chicken");
                  Console.WriteLine(" 5. Bacon");

                  Console.WriteLine("- Veggies");
                  Console.WriteLine(" 6. Spinach");
                  Console.WriteLine(" 7. Mushroom");
                  Console.WriteLine(" 8. Pineapple");
                  Console.WriteLine(" 9. Onion");
                  Console.WriteLine(" 10. Green Pepper");

                  string topInput = Console.ReadLine();
                  string topToAdd = "";

                  switch (topInput){

                    case "1":
                      topToAdd = "pepperoni";
                      break;
                    case "2":
                      topToAdd = "ham";
                      break;
                    case "3":
                      topToAdd = "sausage";
                      break;
                    case "4":
                      topToAdd = "chicken";
                      break;
                    case "5":
                      topToAdd = "bacon";
                      break;
                    case "6":
                      topToAdd = "spinach";
                      break;
                    case "7":
                      topToAdd = "mushroom";
                      break;
                    case "8":
                      topToAdd = "pineapple";
                      break;
                    case "9":
                      topToAdd = "onion";
                      break;
                    case "10":
                      topToAdd = "green pepper";
                      break;

                  }

                  if (pizza.Toppings.Contains(new Topping(topToAdd))){
                    Console.WriteLine("Topping already exists.");
                  } else {
                    pizza.addTopping(new Topping(topToAdd));
                  }

                }
    
                break;

                case "2": //Remove Topping
                
                  Console.WriteLine("Choose a topping to remove:");
                  int i;
                  for (i = 0; i < pizza.Toppings.Count; i++){

                    Console.WriteLine($"{i+1}. {pizza.Toppings[i].Name}");

                  }

                  Console.WriteLine($"{i+1}. Cancel");

                  int input3 = Convert.ToInt32(Console.ReadLine());

                  if (input3 < i+1 && input3 > 0){

                    pizza.Toppings.RemoveAt(input3-1);
                    

                  }

                  break;

                case "3": //Change Size

                  Console.WriteLine("What size would you like to change it into?");
                  Console.WriteLine("1. Small");
                  Console.WriteLine("2. Medium");
                  Console.WriteLine("3. Large");
                  Console.WriteLine("4. Cancel");

                  string input4 = Console.ReadLine();

                  switch (input4) {

                    case "1":
                      pizza.Size = new Size("small");
                      break;
                    case "2":
                      pizza.Size = new Size("medium");
                      break;
                    case "3":
                      pizza.Size = new Size("large");
                      break;

                  }

                  break;

                case "4": //Change Crust

                  Console.WriteLine("What crust would you like to change it into?");
                  Console.WriteLine("1. Hand Tossed");
                  Console.WriteLine("2. Thin");
                  Console.WriteLine("3. Stuffed");
                  Console.WriteLine("4. Cancel");

                  string input5 = Console.ReadLine();

                  switch (input5) {

                    case "1":
                      pizza.Crust = new Crust("handtossed");
                      break;
                    case "2":
                      pizza.Crust = new Crust("thin");
                      break;
                    case "3":
                      pizza.Crust = new Crust("stuffed");
                      break;

                  }

                  break;

                case "5": //Cancel Changes

                  return p;

                case "6": //Save Changes

                  if (pizza.Toppings.Count < 2){
                    Console.WriteLine("Cannot create a pizza with less than 2 toppings.");
                  }
                  else {
                    return pizza;
                  }
                  break;

            }

          }

        }

    }
}
