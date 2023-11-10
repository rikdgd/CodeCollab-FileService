using Microsoft.AspNetCore.Mvc;



namespace CodeCollab_FileService.Controllers;

[ApiController]
[Route("/files")]
public class FileController
{
    [HttpGet(Name = "GetFile")]
    public string GetFile()
    {
        Task<string> fileTask = GetFileContent("/home/rik/Projects/semester 6/CodeCollab/CodeCollab-FileService/Controllers/WeatherForecastController.cs");
        string fileContent = fileTask.Result;
        
        return fileContent;
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