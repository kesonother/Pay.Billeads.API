using System;
using Microsoft.EntityFrameworkCore;
using Pay.Billeads.Models;

namespace Pay.Billeads.Data
{
  public static class DbInitializer
  {
    public static void Initialize(PayBilleadsContext context)
    {
      context.Database.EnsureCreated();

      //check for users
      if (context.Users.Any())
      {
        return; //if user is not empty, DB has been seed
      }

      var user = new User { UserName = "kesonother@gmail.com", Password = "je suis papa" };
      context.Users.Add(user);
      context.SaveChanges();
    }
  }
}

