using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Bson;

using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace VotingSysApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class UserController : ControllerBase
{
  [HttpPost]
  public IActionResult AddUser(string FirstName, string LastName)
  {
    var user = new
    {
      FirstName,
      LastName
    };
    var db = new MongoCRUD("VotingSys");
    db.InsertRecord("Users", user);
    return Ok();
  }


  [HttpGet]
  public object GetUsers()
  {
    string code = "0000";
    var db = new MongoCRUD("VotingSys");
    var users = db.LoadRecords<UserModel>("Users").Select(u => new
    {
      u.Id,
      u.FirstName,
      u.LastName,
      u.LastLogin,
      u.LoginCount
    });
    return new
    {
      code,
      data = users
    };
  }
  [HttpPost]
  public async Task<object> AuthUser(AuthUserParam user)
  {
    var db = new MongoCRUD("VotingSys");
    var userRecord = db.LoadRecordByName<UserModel>("Users", user.FirstName);

    if (userRecord != null && userRecord.Password == user.Password)
    {
      var claims = new List<Claim>
      {
      new Claim(ClaimTypes.Name, userRecord.FirstName),
      // new Claim(ClaimTypes.Role, "Admin")
      };

      var authProperties = new AuthenticationProperties
      { };
      var claimsIdentity = new ClaimsIdentity(
                  claims, CookieAuthenticationDefaults.AuthenticationScheme);
      var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

      await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            claimsPrincipal,
            authProperties);

      userRecord.LastLogin = DateTime.Now;
      // if (userRecord.LoginCount == null) userRecord.LoginCount = 1;
      userRecord.LoginCount += 1;
      db.UpsertRecord("Users", userRecord.Id, userRecord);
      return new
      {
        code = "0000",
        data = "ok"
      };
    }
    return new
    {
      code = "0001",
      data = "User not found or password incorrect"
    };
  }


  [HttpPost]
  public IActionResult UpsertUser(string id, string password)
  {
    var db = new MongoCRUD("VotingSys");
    var user = db.LoadRecordById<UserModel>("Users", new ObjectId(id));
    user.Password = password;
    db.UpsertRecord("Users", new ObjectId(id), user);
    return Ok();
  }
}


