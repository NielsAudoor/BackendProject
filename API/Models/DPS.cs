namespace raiding.API.models;

public class DPS
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? DPSId { get; set; }
    public string? CharacterName { get; set; }
    public int? ILevel { get; set; }
    public int? DamagePerSecond { get; set; }
}