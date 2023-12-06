using Microsoft.AspNetCore.Mvc;
using CodeCollab_FileService.Models;

using MongoDB.Bson;
using MongoDB.Driver;

namespace CodeCollab_FileService.Controllers;


[ApiController]
[Route("/files")]
public class FileController : ControllerBase
{
    [HttpGet(Name = "GetFileContent")]
    public string GetFileContent()
    {
        Task<string> fileTask = GetFileContent("/home/rik/Documents/Fontys/semester 6/code/CodeCollab-backend/codecollab-backend/Controllers/FileController.cs");
        string fileContent = fileTask.Result;
        
        return fileContent;
    }
    
    
    [HttpPost(Name = "SaveFile")]
    public IActionResult SaveFile([FromBody] CodeFile codeFile)
    {
        try
        {
            string? connString = Environment.GetEnvironmentVariable("MONGODB_URI");
            string databaseName = "CodeCollab-testing";
            string collectionName = "CodeFiles";

            var client = new MongoClient(connString);
            var collection = client.GetDatabase(databaseName).GetCollection<BsonDocument>(collectionName);
            BsonDocument codeFileData = codeFile.ToBsonDocument();
        
            collection.InsertOne(codeFileData);

            return Ok("success");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return BadRequest("Could not save code file.");
        }
    }
    

    private async Task<string> GetFileContent(string filePath)
    {
        string fileContent = "";
        
        try
        {
            StreamReader reader = new StreamReader(filePath);
            fileContent = await reader.ReadToEndAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        return fileContent;
    }
}