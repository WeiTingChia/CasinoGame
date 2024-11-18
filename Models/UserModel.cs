using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

public class UserModel
{
  [BsonId]
  public ObjectId Id { get; set; }
  public string FirstName { get; set; }
  public string LastName { get; set; }
  public string Password { get; set; }
}