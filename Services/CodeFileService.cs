using CodeCollab_FileService.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CodeCollab_FileService.Services;

public class CodeFileService
{
    private readonly string? _connectionString;
    private readonly string _databaseName = "CodeCollab-testing";
    private readonly string _collectionName = "CodeFiles";

    public CodeFileService()
    {
        try
        {
            _connectionString = Environment.GetEnvironmentVariable("MONGODB_URI");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public bool SaveFile(CodeFile codeFile)
    {
        try
        {
            var client = new MongoClient(_connectionString);
            var collection = client.GetDatabase(_databaseName).GetCollection<BsonDocument>(_collectionName);
            BsonDocument codeFileData = codeFile.ToBsonDocument();
        
            collection.InsertOne(codeFileData);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }
}