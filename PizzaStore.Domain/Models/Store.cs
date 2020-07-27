using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaStore.Domain.Models
{

  public class Store
  {
    public List<Order> Orders { get; set;}

    public string Name { get; set;}

    public Order CreateOrder ()
    {
      return new Order();
    }

    public Store(){

      Orders = new List<Order>();
      Name = "None";
      
    }

    //Gets Orders List with a filter flag
    //0 = All Orders, 1 = Placed Only, 2 = Completed Only
    public string getOrders(int filter = 0){

      StringBuilder sb = new StringBuilder();

      switch (filter){

        case 0:
          for(int i = 0; i < Orders.Count; i++){
            sb.Append($"===Order #{i}===\n");
            sb.Append(Orders[i].getPizzaList());
            sb.Append("================\n");
          }
          break;
        case 1:
          for(int i = 0; i < Orders.Count; i++){
            if (Orders[i].Placed){
              sb.Append($"===Order #{i}===\n");
              sb.Append(Orders[i].getPizzaList());
              sb.Append("================\n");
            }
          }
          break;
        case 2:
          for(int i = 0; i < Orders.Count; i++){
            if (Orders[i].Completed){
              sb.Append($"===Order #{i}===\n");
              sb.Append(Orders[i].getPizzaList());
              sb.Append("================\n");
            }
          }
          break;

      }

      return sb.ToString();

    }

    //Gets total sales for the week with optional filter flag
    public string getWeeklySales(DateTime fromDate){

      double total = 0;

      foreach (Order order in Orders){

        //Checks if the order date is within the range of fromDate to fromDate + 1 week
        //If so, increment order total
        if (DateTime.Compare(order.DateOrdered,fromDate) >= 0 && DateTime.Compare(order.DateOrdered, fromDate.AddDays(7)) <= 0){

          total += order.getPrice();

        }

      }

      return $"Total Sales from {fromDate.ToString()} to {fromDate.AddDays(7).ToString()}: ${total}\n";

    }

    //Gets total sales for the month
    public string getMonthlySales(DateTime fromDate){

      double total = 0;

      foreach (Order order in Orders){

        //Checks if the order date is within the range of fromDate to fromDate + 1 month
        //If so, increment order total
        if (DateTime.Compare(order.DateOrdered,fromDate) >= 0 && DateTime.Compare(order.DateOrdered, fromDate.AddMonths(1)) <= 0){

          total += order.getPrice();

        }

      }

      return $"Total Sales from {fromDate.ToString()} to {fromDate.AddMonths(1).ToString()}: ${total}\n";

    }

  }

}