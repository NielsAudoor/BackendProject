namespace raiding.API.context;



public interface IMongoContext
{
    IMongoClient Client { get; }
    IMongoDatabase Database { get; }
    IMongoCollection<Raid> RaidCollection { get; }
    IMongoCollection<DPS> dpsCollection { get; }
    IMongoCollection<Healer> healerCollection { get; }
    IMongoCollection<Tank> tankCollection { get; }
}

public class MongoContext : IMongoContext
{
    private readonly MongoClient _client;
    private readonly IMongoDatabase _database;
    private readonly DatabaseSettings _settings;

    public IMongoClient Client
    {
        get
        {
            return _client;
        }
    }
    public IMongoDatabase Database => _database;
    public MongoContext(IOptions<DatabaseSettings> dbOptions)
    {
        _settings = dbOptions.Value;
        _client = new MongoClient(_settings.ConnectionString);
        _database = _client.GetDatabase(_settings.DatabaseName);
    }
    public IMongoCollection<Raid> RaidCollection
    {
        get
        {
            return _database.GetCollection<Raid>(_settings.RaidCollection);
        }
    }

    public IMongoCollection<DPS> dpsCollection
    {
        get
        {
            return _database.GetCollection<DPS>(_settings.DPSCollection);
        }
    }

    public IMongoCollection<Healer> healerCollection
    {
        get
        {
            return _database.GetCollection<Healer>(_settings.HealerCollection);
        }
    }

    public IMongoCollection<Tank> tankCollection
    {
        get
        {
            return _database.GetCollection<Tank>(_settings.TankCollection);
        }
    }

}