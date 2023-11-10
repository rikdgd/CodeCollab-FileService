using Microsoft.AspNetCore.Mvc;
using CodeCollab_FileService.Models;

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
    
    // [Route("saveFile")]
    [HttpPost]
    public IActionResult SaveFile([FromBody] CodeFile codeFile)
    {
        // List<string> allowedFileTypes = new List<string>()
        // {
        //     "text/cs",
        //     "audio/mpeg"
        // };

        Console.WriteLine(codeFile.fileName);
        Console.WriteLine(codeFile.fileContent);

        return Ok("success");
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