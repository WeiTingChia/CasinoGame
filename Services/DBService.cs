using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;
public class MongoCRUD
{
  private IMongoDatabase db;
  public MongoCRUD(string database)
  {
    const string connectionUri = "mongodb+srv://thomaswei:!QAZ2wsx@cluster0.hzz0r.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";
    var client = new MongoClient(connectionUri);
    db = client.GetDatabase(database);
  }

  public void InsertRecord<T>(string table, T record)
  {
    var collection = db.GetCollection<T>(table);
    collection.InsertOne(record);
  }

  public List<T> LoadRecords<T>(string table)
  {
    var collection = db.GetCollection<T>(table);
    return collection.Find(new BsonDocument()).ToList();
  }

  public T LoadRecordById<T>(string table, ObjectId id)
  {
    var collection = db.GetCollection<T>(table);
    var filter = Builders<T>.Filter.Eq("Id", id);
    return collection.Find(filter).First();
  }
  public T LoadRecordByName<T>(string table, string name)
  {
    var collection = db.GetCollection<T>(table);
    var filter = Builders<T>.Filter.Eq("FirstName", name);
    return collection.Find(filter).FirstOrDefault();
  }
  public T LoadRecordByGameName<T>(string table, string name)
  {
    var collection = db.GetCollection<T>(table);
    var filter = Builders<T>.Filter.Eq("Name", name);
    return collection.Find(filter).FirstOrDefault();
  }

  public void UpsertRecord<T>(string table, ObjectId id, T record)
  {
    var collection = db.GetCollection<T>(table);
    var result = collection.ReplaceOne(
      new BsonDocument("_id", id),
      record,
      new ReplaceOptions { IsUpsert = true });
  }
  public void UpsertRecordByName<T>(string table, string name, T record)
  {
    var collection = db.GetCollection<T>(table);
    var filter = Builders<T>.Filter.Eq("Name", name);
    var result = collection.ReplaceOne(
      filter,
      record,
      new ReplaceOptions { IsUpsert = true });
  }
}