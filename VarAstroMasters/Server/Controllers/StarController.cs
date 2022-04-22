using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Authorization;
using NuGet.Protocol;
using VarAstroMasters.Shared.DTO;

namespace VarAstroMasters.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StarController : ControllerBase
{
    private readonly IStarService _starService;
    private readonly IWebHostEnvironment _env;

    public StarController(IStarService starService, IWebHostEnvironment env)
    {
        _starService = starService;
        _env = env;
    }

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<StarDTO>>>> GetStarsAsync()
    {
        var response = await _starService.GetStarsAsync();
        return Ok(response);
    }
    
    [HttpGet("{starId}")]
    public async Task<ActionResult<ServiceResponse<StarDTO>>> GetStarAsync(int starId)
    {
        var response = await _starService.GetStarAsync(starId);
        return Ok(response);
    }

    [HttpPost("fileUpload")]
    public async Task<ActionResult<ServiceResponse<string>>> UploadFile([FromForm] IEnumerable<IFormFile> files)
    {
        foreach (IFormFile file in files)
        {
            var uploadResult = new UploadResult();
            var untrustedFileName = file.FileName;
            uploadResult.FileName = untrustedFileName;
            var path = Path.Combine(_env.ContentRootPath,
                _env.EnvironmentName, "unsafe_uploads",
                untrustedFileName);
            await using FileStream fs = new(path, FileMode.Create);
            await file.CopyToAsync(fs);
            var content = await ReadAsStringAsync(file);
            var responseGut = new ServiceResponse<string>
            {
                Data = JsonSerializer.Serialize(content),
            };
            return Ok(responseGut);
        }

        var response = new ServiceResponse<string>
        {
            Data = "ego jebo",
            Success = false
        };
        return BadRequest(response);
    }

    private async Task<StarJsonObj> ReadAsStringAsync(IFormFile file)
    {
        var resObj = new StarJsonObj
        {
            Meta = new List<string>(),
            Data = new List<string>()
        };
        using (var reader = new StreamReader(file.OpenReadStream()))
        {
            while (reader.Peek() >= 0)
            {
                var line = await reader.ReadLineAsync();
                if (line != null)
                {
                    if (line.StartsWith("#"))
                        resObj.Meta.Add(line);
                    else
                        resObj.Data.Add(line);
                }
            }
        }

        return resObj;
    }

    public class UploadResult
    {
        public bool Uploaded { get; set; }
        public string? FileName { get; set; }
        public string? StoredFileName { get; set; }
        public int ErrorCode { get; set; }
    }


    public class StarJsonObj
    {
        public List<string> Meta { get; set; }
        public List<string> Data { get; set; }
    }
}