using InfyKiddoFun.Domain.Configurations;

namespace InfyKiddoFun.API.Extensions;

public static class ConfigurationExtensions
{
    public static TokenConfiguration GetTokenConfiguration(this IConfiguration configuration)
    {
        return configuration.GetSection(nameof(TokenConfiguration)).Get<TokenConfiguration>();
    }
}