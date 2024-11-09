namespace ImageSaveAndReadMongoDb.Models;

public class FileUpload
{
    public string Employee { get; set; }  // JSON string for Employee data
    public IFormFile File { get; set; }   // File data
}