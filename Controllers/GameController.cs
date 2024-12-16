using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Bson;
using System;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Authorize]
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
  public IActionResult UpsertGameToCompleted(string gameName)
  {
    var db = new MongoCRUD("VotingSys");
    var game = db.LoadRecordByGameName<GameModel>("Games", gameName);
    game.Status = "Completed";
    db.UpsertRecordByName("Games", gameName, game);
    return Ok();
  }

  [HttpPost]
  public object UpdateGame(UpdateGameParam gameParam)
  {
    var db = new MongoCRUD("VotingSys");
    var gameModel = db.LoadRecordByGameName<GameModel>("Games", gameParam.GameName);
    var answer = new PersonalResult() { UserName = gameParam.UserName, Result = gameParam.UserAnswer };
    List<PersonalResult> originalResult = gameModel.Result;
    if (gameModel.Result.Count == 3)
    {
      gameModel.Status = "Completed";
    }
    gameModel.Result = originalResult.Append(answer).ToList();
    db.UpsertRecord("Games", gameModel.Id, gameModel);

    return new
    {
      code = "0000",
      data = "ok"
    };
  }
}