using System;
using Pay.Billeads.ViewModel;

namespace Pay.Billeads.Services.Abstraction
{
  public interface IAuthService
  {
    string HashPassword(string password);
    bool VerifyPassword(string actualPassword, string hashedPassword);
    AuthData GetAuthData(string id);
  }
}

