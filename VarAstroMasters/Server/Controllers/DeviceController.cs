using Microsoft.AspNetCore.Authorization;

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

    [Authorize]
    [HttpPost("Add")]
    public async Task<ActionResult<ServiceResponse<Device>>> DevicePost(Device device)
    {
        var sr = await _deviceService.DevicePost(device);
        return Ok(sr);
    }

    [HttpGet("list")]
    public async Task<ActionResult<ServiceResponse<List<Device>>>> UserFromTokenDevicesGet()
    {
        var sr = await _deviceService.UserFromTokenDevicesGet();
        return Ok(sr);
    }

    [Authorize]
    [HttpDelete("{deviceId}")]
    public async Task<ActionResult<ServiceResponse<bool>>> DeviceDelete(int deviceId)
    {
        var sr = await _deviceService.DeviceDelete(deviceId);
        return Ok(sr);
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult<ServiceResponse<Device>>> DevicePut(Device device)
    {
        var sr = await _deviceService.DevicePut(device);
        return Ok(sr);
    }
}