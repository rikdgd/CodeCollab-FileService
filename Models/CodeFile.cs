using MongoDB.Bson.Serialization.Attributes;

namespace CodeCollab_FileService.Models;

public class CodeFile
{
    public string fileName { get; private set; }
    public string fileContent { get; private set; }
    
    [BsonElement("user_id")]
    public long userId { get; private set; }
    
    [BsonElement("workspace_id")]
    public long workspaceId { get; private set; }
    
    
    public CodeFile(string fileName, string fileContent, long userId = 0, long workspaceId = 0)
    {
        this.fileName = fileName;
        this.fileContent = fileContent;
        this.userId = userId;
        this.workspaceId = workspaceId;
    }
}
