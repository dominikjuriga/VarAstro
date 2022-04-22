﻿namespace VarAstroMasters.Client.Services.AuthService;

public interface IAuthService
{
    Task<ServiceResponse<string>> Register(UserRegister userRegister);
    Task<ServiceResponse<string>?> LogIn(UserLogin userLogin);
}