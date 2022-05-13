using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace VarAstroMasters.Server.Services.UserStarIdentificationService;

public class UserStarIdentificationService : IUserStarIdentificationService
{
    private readonly DataContext _context;
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public UserStarIdentificationService(DataContext context, IAuthService authService, IMapper mapper)
    {
        _context = context;
        _authService = authService;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<List<UserStarIdentification>>> UserIdentificationsListGet()
    {
        var userId = _authService.GetUserId();
        if (userId is null) return ResponseHelper.FailResponse<List<UserStarIdentification>>(Keywords.InvalidToken);
        var data = await _context.UserStarIdentifications
            .Where(usi => usi.UserId == userId)
            .Include(usi => usi.Star)
            .ToListAsync();

        return new ServiceResponse<List<UserStarIdentification>>
        {
            Data = data
        };
    }
}