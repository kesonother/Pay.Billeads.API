using System;
using Pay.Billeads.Data.Repositories;
using Pay.Billeads.Models;
using Pay.Billeads.Data.Abstract;

namespace Pay.Billeads.Data.Repositories
{
  public class UserRepository : EntityBaseRepository<User>, IUserRepository
  {
    public UserRepository(PayBilleadsContext context) : base(context) { }

    public bool isEmailUniq(string email)
    {
      var user = this.GetSingle(u => u.Email == email);
      return user == null;
    }

    public bool IsUsernameUniq(string username)
    {
      var user = this.GetSingle(u => u.UserName == username);
      return user == null;
    }
  }
}

