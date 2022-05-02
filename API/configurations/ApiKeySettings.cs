namespace raiding.API.configuration;

public class ApiKeySettings
{
    public string? SecretString { get; set; }
    public string? Issuer { get; set; }
    public string? Audience { get; set; }
}