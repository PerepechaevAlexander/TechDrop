using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace TechDrop.Logic.Configurations;

/// <summary>
/// Настройки аутентификации на основе JWT-токенов
/// </summary>
public class AuthSettings
{
    /// <summary>
    /// Название секции настроек в файле appsettings.json
    /// </summary>
    private const string SectionName = nameof(AuthSettings);
    
    /// <summary>
    /// Наименование издателя JWT-токена
    /// </summary>
    public string Issuer { get; private set; }

    /// <summary>
    /// Наименование потребителя JWT-токена
    /// </summary>
    public string Audience { get; private set; }

    /// <summary>
    /// Ключ шифрации - на основе него генерируется ключ безопасности,
    /// необходимый для генерации токена доступа
    /// </summary>
    private readonly string Key;

    /// <summary>
    /// Создание экземпляра класса <see cref="AuthSettings"/>
    /// </summary>
    public AuthSettings(IConfiguration configuration)
    {
        var section = configuration.GetSection(SectionName);
        
        Issuer = section.GetSection(nameof(Issuer)).Value!;
        Audience = section.GetSection(nameof(Audience)).Value!;
        Key = section.GetSection(nameof(Key)).Value!;
    }

    /// <summary>
    /// Получить ключ безопасности для генерации JWT-токена
    /// </summary>
    /// <returns><see cref="SymmetricSecurityKey"/></returns>
    public SymmetricSecurityKey GetSymmetricSecurityKey() => new(Encoding.UTF8.GetBytes(Key));
}