using VarAstroMasters.Server.Services.DeviceService;

namespace VarAstroMasters.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DeviceController : ControllerBase
{
    private readonly IDeviceService _deviceService;

    public DeviceController(IDeviceService deviceService)
    {
        _deviceService = deviceService;
    }

    [HttpPost("Add")]
    public async Task<ActionResult<ServiceResponse<bool>>> DevicePost(DeviceAdd deviceAdd)
    {
        var sr = await _deviceService.DevicePost(deviceAdd);
        return Ok(sr);
    }

    [HttpGet("list")]
    public async Task<ActionResult<ServiceResponse<List<DeviceDTO>>>> UserFromTokenDevicesGet()
    {
        var sr = await _deviceService.UserFromTokenDevicesGet();
        return Ok(sr);
    }

    [HttpDelete("{deviceId}")]
    public async Task<ActionResult<ServiceResponse<bool>>> DeviceDelete(int deviceId)
    {
        var sr = await _deviceService.DeviceDelete(deviceId);
        return Ok(sr);
    }

    [HttpPut]
    public async Task<ActionResult<ServiceResponse<bool>>> DeviceEdit(DeviceEdit device)
    {
        var sr = await _deviceService.DeviceEdit(device);
        return Ok(sr);
    }
}