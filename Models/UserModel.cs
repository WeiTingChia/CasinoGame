using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Globalization;

public class UserModel
{
  [BsonId]
  public ObjectId Id { get; set; }
  public string FirstName { get; set; }
  public string LastName { get; set; }
  public string Password { get; set; }
  public DateTime LastLogin { get; set; }
  public int LoginCount { get; set; }
}

public class AuthUserParam
{
  public string FirstName { get; set; }
  public string Password { get; set; }
}