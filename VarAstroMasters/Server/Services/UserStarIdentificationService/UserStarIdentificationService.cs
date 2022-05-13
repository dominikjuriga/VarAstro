using Microsoft.EntityFrameworkCore;

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

    public async Task<ServiceResponse<List<UserStarIdentificationDTO>>> UserIdentificationsListGet()
    {
        var userId = _authService.GetUserId();
        if (userId is null) return ResponseHelper.FailResponse<List<UserStarIdentificationDTO>>(Keywords.InvalidToken);
        var data = await _context.UserStarIdentifications
            .Where(usi => usi.UserId == userId)
            .Include(usi => usi.Star)
            .ToListAsync();

        List<UserStarIdentificationDTO> dtos = new();
        foreach (var userStarIdentification in data)
            dtos.Add(_mapper.Map<UserStarIdentificationDTO>(userStarIdentification));

        return new ServiceResponse<List<UserStarIdentificationDTO>>
        {
            Data = dtos
        };
    }

    public async Task<ServiceResponse<bool>> UserIdentificationsPost(UserStarIdentificationCreateDTO usi)
    {
        var userId = _authService.GetUserId();
        if (userId is null) return ResponseHelper.FailResponse<bool>(Keywords.InvalidToken);
        var StarExists = _context.Stars.Any(s => s.Id == usi.StarId);
        if (!StarExists) return ResponseHelper.FailResponse<bool>(Keywords.NotFoundMessage);

        var identification = new UserStarIdentification
        {
            UserId = userId,
            StarId = usi.StarId,
            UserIdentification = usi.UserIdentification
        };

        _context.UserStarIdentifications.Add(identification);
        var result = await _context.SaveChangesAsync();
        if (result == 0) return ResponseHelper.FailResponse<bool>(Keywords.PostFailed);
        return new ServiceResponse<bool>
        {
            Data = true,
            Message = Keywords.PostSucceeded
        };
    }
}