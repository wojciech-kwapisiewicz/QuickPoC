using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;

namespace FilesUploadBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public UploadController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet("upload")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload()
        {
            var boundary = HeaderUtilities.RemoveQuotes(MediaTypeHeaderValue.Parse(Request.ContentType).Boundary).Value;
            var reader = new MultipartReader(boundary, Request.Body);

            var section = await reader.ReadNextSectionAsync();
            while (section != null)
            {
                if (ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out var contentDisposition))
                {
                    if (contentDisposition.DispositionType.Equals("form-data") && contentDisposition.FileName.HasValue)
                    {
                        var filePath = Path.Combine("C:\\temp\\uploads", contentDisposition.FileName.Value);

                        using (var targetStream = new FileStream(filePath, FileMode.Create))
                        {
                            await section.Body.CopyToAsync(targetStream);
                        }
                    }
                }

                section = await reader.ReadNextSectionAsync();
            }

            return Ok("Files uploaded successfully.");
        }
    }
}
