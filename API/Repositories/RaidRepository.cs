

public interface IRaidRepository
{
    Task<Raid> AddRaid(Raid newRaid);
    Task DeleteRaid(string raidid);
    Task<List<Raid>> GetAllRaids();
    Task<Raid> GetRaid(Raid raid);
}

public class RaidRepository : IRaidRepository
{
    private readonly IMongoContext _context;
    public RaidRepository(IMongoContext context)
    {
        _context = context;
    }

    public async Task<Raid> AddRaid(Raid newRaid)
    {
        await _context.RaidCollection.InsertOneAsync(newRaid);
        return newRaid;
    }

    public async Task DeleteRaid(string raidid)
    {
        try
        {
            var filter = Builders<Raid>.Filter.Eq("RaidId", raidid);
            var result = await _context.RaidCollection.DeleteOneAsync(filter);
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public async Task<List<Raid>> GetAllRaids()
    {
        return await _context.RaidCollection.Find(_ => true).ToListAsync();
    }

    public async Task<Raid> GetRaid(Raid raid)
    {
        return await _context.RaidCollection.Find<Raid>(_ => _.RaidId == raid.RaidId).FirstAsync();
    }
}