namespace raiding.API.models;

public class Raid
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? RaidId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public Healer? healer { get; set; }
    public Tank? tank { get; set; }
    public DPS? dps { get; set; }
}