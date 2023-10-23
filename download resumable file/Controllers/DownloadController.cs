using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using System.IO;

[Route("api/download")]
public class DownloadController : ControllerBase
{
    private readonly IFileProvider _fileProvider;

    public DownloadController(IFileProvider fileProvider)
    {
        _fileProvider = fileProvider;
    }

    [HttpGet("{fileName}")]
    public IActionResult Download(string fileName, long? start)
    {
        var filePath = Path.Combine("D:\\", fileName);

        if (!System.IO.File.Exists(filePath))
        {
            return NotFound();
        }

        var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

        if (start.HasValue)
        {
            fileStream.Seek(start.Value, SeekOrigin.Begin);
        }

        var fileLength = fileStream.Length - start.GetValueOrDefault();
        var response = File(fileStream, "application/octet-stream", enableRangeProcessing: true);
        response.EnableRangeProcessing = true;

        Response.Headers.Add("Accept-Ranges", "bytes");

        if (start.HasValue)
        {
            Response.Headers.Add("Content-Range", new StringValues($"{start}-{fileLength - 1}/{fileStream.Length}"));
            
        }
        Response.Headers.Add("Content-Length", fileLength.ToString());

        return response;
    }
}