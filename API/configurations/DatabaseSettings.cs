namespace raiding.API.configuration;

public class DatabaseSettings
{
    public string? ConnectionString { get; set; }
    public string? DatabaseName { get; set; }
    public string? RaidCollection { get; set; }
    public string? DPSCollection { get; set; }
    public string? HealerCollection { get; set; }
    public string? TankCollection { get; set; }
}