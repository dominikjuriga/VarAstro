using Microsoft.EntityFrameworkCore;

namespace VarAstroMasters.Server.Services.UserStarIdentificationService;

public class UserStarIdentificationService : IUserStarIdentificationService
{
    private readonly IAuthService _authService;
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public UserStarIdentificationService(DataContext context, IAuthService authService, IMapper mapper)
    {
        _context = context;
        _authService = authService;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<List<UserStarIdentificationDTO>>> UserIdentificationsListGet()
    {
        // Verify the request has a token
        var userId = _authService.GetUserId();
        if (userId is null) return ResponseHelper.FailResponse<List<UserStarIdentificationDTO>>(Keywords.InvalidToken);

        // Get data for the token
        var data = await _context.UserStarIdentifications
            .Where(usi => usi.UserId == userId)
            .Include(usi => usi.Star)
            .ToListAsync();

        // Map the models to DTOs
        List<UserStarIdentificationDTO> dtos = new();
        foreach (var userStarIdentification in data)
            dtos.Add(_mapper.Map<UserStarIdentificationDTO>(userStarIdentification));

        // Return response
        return new ServiceResponse<List<UserStarIdentificationDTO>>
        {
            Data = dtos
        };
    }

    public async Task<ServiceResponse<bool>> UserIdentificationsPost(UserStarIdentificationCreateDTO usi)
    {
        // verify the request has token
        var userId = _authService.GetUserId();
        if (userId is null) return ResponseHelper.FailResponse<bool>(Keywords.InvalidToken);

        // verify the star exists
        var StarExists = _context.Stars.Any(s => s.Id == usi.StarId);
        if (!StarExists) return ResponseHelper.FailResponse<bool>(Keywords.NotFoundMessage);

        // Create the model from request
        var identification = new UserStarIdentification
        {
            UserId = userId,
            StarId = usi.StarId,
            UserIdentification = usi.UserIdentification
        };

        // Try to save the model and return response
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