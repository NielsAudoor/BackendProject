namespace raiding.API.models;


public class Tank
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? TankId { get; set; }
    public string? CharacterName { get; set; }
    public int? ILevel { get; set; }
    public int? armour { get; set; }
}