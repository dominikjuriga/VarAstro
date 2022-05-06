﻿using Microsoft.EntityFrameworkCore;
using VarAstroMasters.Server.Data;

namespace VarAstroMasters.Server.Services.StarService;

public class StarService : IStarService
{
    private readonly DataContext _context;

    public StarService(DataContext context)
    {
        _context = context;
    }

    public async Task<ServiceResponse<List<StarDTO>>> GetStarsAsync()
    {
        var data = await _context.Stars.ToListAsync();
        var stars = new List<StarDTO>();
        foreach (var star in data)
            stars.Add(new StarDTO
            {
                Name = star.Name,
                Id = star.Id
            });
        return new ServiceResponse<List<StarDTO>>
        {
            Data = stars
        };
    }

    public async Task<ServiceResponse<StarDTO>> GetStarAsync(int starId)
    {
        var star = await _context.Stars
            .Include(s => s.LightCurves)
            .ThenInclude(lc => lc.User)
            .Include(s => s.StarCatalogs)
            .Include(s => s.StarVariability)
            .FirstOrDefaultAsync(s => s.Id == starId);
        if (star is not null)
        {
            var curves = new List<LightCurveDTO>();
            foreach (var curve in star.LightCurves)
                curves.Add(new LightCurveDTO
                {
                    Id = curve.Id,
                    User = new UserDTO
                    {
                        Id = curve.User.Id,
                        Name = curve.User.UserName
                    }
                });
            return new ServiceResponse<StarDTO>
            {
                Data = new StarDTO
                {
                    Id = star.Id,
                    LightCurves = curves,
                    Name = star.Name,
                    StarCatalogs = star.StarCatalogs,
                    StarVariability = star.StarVariability
                }
            };
        }

        return new ServiceResponse<StarDTO>
        {
            Success = false
        };
    }

    public async Task<ServiceResponse<StarSearchDTO>> Search(string searchQuery)
    {
        var data = await _context.Stars
            .Where(s => s.Name.ToLower().Contains(searchQuery.ToLower()))
            .ToListAsync();
        List<StarDTO> dtos = new();
        foreach (var star in data)
            dtos.Add(new StarDTO
            {
                Id = star.Id,
                Name = star.Name
            });
        var response = new ServiceResponse<StarSearchDTO>
        {
            Data = new StarSearchDTO
            {
                Data = dtos
            }
        };

        return response;
    }
}