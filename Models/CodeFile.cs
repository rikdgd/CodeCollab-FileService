namespace CodeCollab_FileService.Models;

public class CodeFile
{
    public string fileName { get; private set; }
    public string fileContent { get; private set; }
    public long userId { get; private set; }
    public long workspaceId { get; private set; }
    
    
    public CodeFile(string fileName, string fileContent, long userId = 0, long workspaceId = 0)
    {
        this.fileName = fileName;
        this.fileContent = fileContent;
        this.userId = userId;
        this.workspaceId = workspaceId;
    }
}
