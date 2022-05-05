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
    public async Task<ActionResult<ServiceResponse<bool>>> AddDeviceAsync(DeviceAdd deviceAdd)
    {
        var sr = await _deviceService.AddDeviceAsync(deviceAdd);
        return Ok(sr);
    }

    [HttpGet("list")]
    public async Task<ActionResult<ServiceResponse<List<DeviceDTO>>>> GetMyDevices()
    {
        var sr = await _deviceService.GetMyDevices();
        return Ok(sr);
    }

    [HttpDelete("{deviceId}")]
    public async Task<ActionResult<ServiceResponse<bool>>> DeleteDevice(int deviceId)
    {
        var sr = await _deviceService.DeleteDevice(deviceId);
        return Ok(sr);
    }

    [HttpPut]
    public async Task<ActionResult<ServiceResponse<bool>>> EditDevice(Device device)
    {
        var sr = await _deviceService.EditDevice(device);
        return Ok(sr);
    }
}