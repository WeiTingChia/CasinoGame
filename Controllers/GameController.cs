using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Bson;
using System;

[ApiController]
[Route("[controller]/[action]")]
public class UserController : ControllerBase
{
  [HttpPost]
  public IActionResult AddGame(AddGameParam game)
  {
    var db = new MongoCRUD("VotingSys");
    db.InsertRecord("Games", game);
    return Ok();
  }

  [HttpGet]

  public object GetGames()
  {
    string code = "0000";
    var db = new MongoCRUD("VotingSys");
    var games = db.LoadRecords<GameModel>("Games");
    return new
    {
      code,
      data = games
    };
  }

  [HttpPost]
  public IActionResult UpsertGameToCompleted(string id)
  {
    var db = new MongoCRUD("VotingSys");
    var game = db.LoadRecordById<GameModel>("Games", new ObjectId(id));
    game.Status = "Completed";
    db.UpsertRecord("Games", new ObjectId(id), game);
    return Ok();
  }
}