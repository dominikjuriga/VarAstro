﻿using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace VarAstroMasters.Shared.Models;

public class User : IdentityUser
{
    public DateTime Registered { get; set; } = DateTime.Now;
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [NotMapped] public string Name { get; set; }
    public List<Device> Devices { get; set; }
    public List<Observatory> Observatories { get; set; }
}