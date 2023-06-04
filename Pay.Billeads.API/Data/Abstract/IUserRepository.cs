using System;
using Pay.Billeads.Models;

namespace Pay.Billeads.Data.Abstract
{
  public interface IUserRepository : IEntityBaseRepository<User>
  {
    bool IsUsernameUniq(string username);
    bool isEmailUniq(string email);
  }
}

