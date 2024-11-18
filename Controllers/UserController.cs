using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Bson;

using System;

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
    var users = db.LoadRecords<UserModel>("Users");
    return new
    {
      code,
      data = users
    };
  }

  [HttpPost]
  public IActionResult UpsertUser(string id)
  {
    var db = new MongoCRUD("VotingSys");
    var user = db.LoadRecordById<UserModel>("Users", new ObjectId(id));
    user.Password = "123456";
    db.UpsertRecord("Users", new ObjectId(id), user);
    return Ok();
  }
}


