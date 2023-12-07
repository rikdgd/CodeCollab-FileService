using Microsoft.AspNetCore.Mvc;
using CodeCollab_FileService.Models;
using CodeCollab_FileService.Services;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CodeCollab_FileService.Controllers;


[ApiController]
[Route("/files")]
public class FileController : ControllerBase
{
    private CodeFileService _service = new CodeFileService();
    
    
    [HttpGet("GetFileContent", Name = "GetFileContent")]
    public string GetFileContent()
    {
        Task<string> fileTask = GetFileContent("/home/rik/Documents/Fontys/semester 6/code/CodeCollab-backend/codecollab-backend/Controllers/FileController.cs");
        string fileContent = fileTask.Result;
        
        return fileContent;
    }
    
    
    [HttpPost("SaveFile", Name = "SaveFile")]
    public IActionResult SaveFile([FromBody] CodeFile codeFile)
    {
        bool succes = _service.SaveFile(codeFile);

        if (!succes) return BadRequest("Failed to save file.");    
        return Ok("success");
    }
    
    
    [HttpPost("UploadFile", Name = "UploadFile")]
    public async Task<IActionResult> UploadFile(IFormFile file, long userId, long workspaceId)
    {
        if (file == null || file.Length < 1)
        {
            return BadRequest("Uploaded file doesn't contain any content.");
        }

        CodeFile codeFile = new CodeFile(file.Name, userId: userId, workspaceId: workspaceId);

        try
        {
            using (var streamReader = new StreamReader(file.OpenReadStream()))
            {
                var fileContent = await streamReader.ReadToEndAsync();
                codeFile.fileContent = fileContent;
            }

            bool succes = _service.SaveFile(codeFile);

            if (!succes) return BadRequest("Failed to save file.");
            return Ok($"File was received successfully:\n\n{codeFile.fileContent}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return BadRequest("Failed to read file contents.");
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