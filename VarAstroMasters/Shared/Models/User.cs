using Microsoft.AspNetCore.Identity;

namespace VarAstroMasters.Shared.Models;

public class User : IdentityUser
{
    public DateTime Registered { get; set; } = DateTime.Now;
    public List<Device> Devices { get; set; }
}