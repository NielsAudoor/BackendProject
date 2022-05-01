namespace raiding.API.models;

public class Healer
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? HealerId { get; set; }
    public string? CharacterName { get; set; }
    public int? ILevel { get; set; }
    public int? HealingPerSecond { get; set; }
}