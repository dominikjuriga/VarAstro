﻿using System.ComponentModel.DataAnnotations;

namespace VarAstroMasters.Shared.Models;

public class UserStarIdentification
{
    [Required][Key] public int StarId { get; set; }
    [Required][Key] public string UserId { get; set; } = string.Empty;
    public string Identification { get; set; } = string.Empty;
}