using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

public class GameModel
{
  [BsonId]
  public ObjectId Id { get; set; }
  public string Name { get; set; }
  public string CreateDate { get; set; }
  public string EndDate { get; set; }
  public List<PersonalResult> Result { get; set; }
  public string Winner { get; set; }
  public string Loser { get; set; }
  public string Status { get; set; }
}

public class PersonalResult
{
  public string UserName { get; set; }
  public string Result { get; set; }
}

public class AddGameParam
{
  public string Name { get; set; }
  public string CreateDate { get; set; }
  public string EndDate { get; set; }
  public List<PersonalResult> Result { get; set; }
  public string Winner { get; set; }
  public string Loser { get; set; }
  public string Status { get; set; }
}