using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using InfyKiddoFun.Application.Interfaces;
using InfyKiddoFun.Application.Models.Identity;
using InfyKiddoFun.Domain.Configurations;
using InfyKiddoFun.Domain.Constants;
using InfyKiddoFun.Domain.Entities;
using InfyKiddoFun.Domain.Wrapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace InfyKiddoFun.Application.Features;

public class StudentUserService : IStudentUserService
{
    private readonly UserManager<StudentUser> _userManager;
    private readonly ICurrentUserService _currentUserService;
    private readonly TokenConfiguration _tokenConfiguration;

    public StudentUserService(UserManager<StudentUser> userManager, ICurrentUserService currentUserService, IOptions<TokenConfiguration> tokenConfiguration)
    {
        _userManager = userManager;
        _currentUserService = currentUserService;
        _tokenConfiguration = tokenConfiguration.Value;
    }

    public async Task<IResult<LoginResponse>> LoginAsync(LoginRequest request)
    {
        
        try
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return await Result<LoginResponse>.FailAsync("User Not Found.");
            }
            if (!user.EmailConfirmed)
            {
                return await Result<LoginResponse>.FailAsync("E-Mail not confirmed.");
            }
            var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!passwordValid)
            {
                return await Result<LoginResponse>.FailAsync("Invalid Credentials.");
            }
            user.RefreshToken = GenerateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            await _userManager.UpdateAsync(user);

            var token = GenerateJwt(user);
            var response = new LoginResponse { Token = token, RefreshToken = user.RefreshToken, RefreshTokenExpiryTime = user.RefreshTokenExpiryTime };
            return await Result<LoginResponse>.SuccessAsync(response);
        }
        catch (Exception e)
        {
            return await Result<LoginResponse>.FailAsync(e.Message);
        }
    }

    public async Task<IResult<LoginResponse>> RefreshTokenAsync(RefreshTokenRequest request)
    {
        try
        {
            if (request is null)
            {
                throw new Exception("Invalid Client Token.");
            }
            var userPrincipal = GetPrincipalFromExpiredToken(request.Token);
            var userName = userPrincipal.FindFirstValue(ApplicationClaimTypes.UserName)!;
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
                throw new Exception("User Not Found.");
            if (user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                throw new Exception("Invalid Client Token.");
            var token = GenerateJwt(user);
            user.RefreshToken = GenerateRefreshToken();
            await _userManager.UpdateAsync(user);

            var response = new LoginResponse { Token = token, RefreshToken = user.RefreshToken, RefreshTokenExpiryTime = user.RefreshTokenExpiryTime };
            return await Result<LoginResponse>.SuccessAsync(response);
        }
        catch (Exception e)
        {
            return await Result<LoginResponse>.FailAsync(e.Message);
        }
    }

    public async Task<IResult> RegisterAsync(StudentRegisterRequest request)
    {
        try
        {
            if(request.Password != request.ConfirmPassword)
                return await Result.FailAsync("Password and Confirm Password do not match.");
            var user = new StudentUser
            {
                UserName = request.UserName,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                AboutMe = request.AboutMe,
                AgeGroup = request.AgeGroup,
                PreferredSubjects = request.Subjects,
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                return await Result.FailAsync(result.Errors.Select(x => x.Description).ToList());
            }
            return await Result.SuccessAsync("Student Registered Successfully!");
        }
        catch (Exception e)
        {
            return await Result.FailAsync(e.Message);
        }
    }

    public async Task<IResult> UpdateInfoAsync(UpdateStudentInfoRequest request)
    {
        try
        {
            if(string.IsNullOrWhiteSpace(_currentUserService.UserId))
                return await Result.FailAsync("User Id is required.");
            var user = await _userManager.FindByIdAsync(_currentUserService.UserId);
            if (user == null)
                return await Result.FailAsync("User Not Found.");
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.PhoneNumber = request.PhoneNumber;
            user.AboutMe = request.AboutMe;
            user.AgeGroup = request.AgeGroup;
            user.PreferredSubjects = request.Subjects;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return await Result.FailAsync(result.Errors.Select(x => x.Description).ToList());
            }
            return await Result.SuccessAsync("Student Info Updated Successfully!");
        }
        catch (Exception e)
        {
            return await Result.FailAsync(e.Message);
        }
    }

    public async Task<IResult> UpdatePasswordAsync(UpdatePasswordRequest request)
    {
        try
        {
            if(string.IsNullOrWhiteSpace(_currentUserService.UserId))
                return await Result.FailAsync("User Id is required.");
            var user = await _userManager.FindByIdAsync(_currentUserService.UserId);
            if (user == null)
                return await Result.FailAsync("User Not Found.");
            if(request.NewPassword != request.ConfirmPassword)
                return await Result.FailAsync("New Password and Confirm Password do not match.");
            if (request.OldPassword == request.NewPassword)
                return await Result.FailAsync("Old Password and New Password cannot be same.");
            var result = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
            if (!result.Succeeded)
            {
                return await Result.FailAsync(result.Errors.Select(x => x.Description).ToList());
            }
            return await Result.SuccessAsync("Password Updated Successfully!");
        }
        catch (Exception e)
        {
            return await Result.FailAsync(e.Message);
        }
    }

    private string GenerateJwt(AppUser user)
    {
        var claims = new List<Claim>
        {
            new(ApplicationClaimTypes.Id, user.Id),
            new(ApplicationClaimTypes.Email, user.Email!),
            new(ApplicationClaimTypes.UserName, user.UserName!),
            new(ApplicationClaimTypes.FirstName, user.FirstName),
            new(ApplicationClaimTypes.LastName, user.LastName),
            new(ApplicationClaimTypes.PhoneNumber, user.PhoneNumber!),
            new(ApplicationClaimTypes.Role, Roles.Student),
        };
        return GenerateEncryptedToken(GetSigningCredentials(), claims);
    }

    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private static string GenerateEncryptedToken(SigningCredentials signingCredentials, IEnumerable<Claim> claims)
    {
        var token = new JwtSecurityToken(claims: claims, expires: DateTime.UtcNow.AddDays(2), signingCredentials: signingCredentials);
        var tokenHandler = new JwtSecurityTokenHandler();
        var encryptedToken = tokenHandler.WriteToken(token);
        return encryptedToken;
    }

    private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfiguration.Secret)),
            ValidateIssuer = false,
            ValidateAudience = false,
            RoleClaimType = ApplicationClaimTypes.Role,
            ClockSkew = TimeSpan.Zero
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return principal;
    }

    private SigningCredentials GetSigningCredentials()
    {
        var secret = Encoding.UTF8.GetBytes(_tokenConfiguration.Secret);
        return new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256);
    }
}