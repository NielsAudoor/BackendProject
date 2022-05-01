namespace raiding.API.repositories;

public interface IDPSRepository
{
    Task<DPS> AddDPS(DPS newDPS);
    Task<List<DPS>> GetAllDPS();
    Task<DPS> GetDPS(DPS dps);
    Task<List<DPS>> GetDPSByHigherILVL(DPS dps);
    Task<DPS> GetDPSByUsername(DPS dps);
}

public class DPSRepository : IDPSRepository
{
    private readonly IMongoContext _context;
    public DPSRepository(IMongoContext context)
    {
        _context = context;
    }

    public async Task<DPS> AddDPS(DPS newDPS)
    {
        await _context.dpsCollection.InsertOneAsync(newDPS);
        return newDPS;
    }
    public async Task<List<DPS>> GetAllDPS()
    {
        return await _context.dpsCollection.Find(_ => true).ToListAsync();
    }

    public async Task<DPS> GetDPSByUsername(DPS dps)
    {
        var result = await _context.dpsCollection.Find<DPS>(_ => _.CharacterName.ToLower() == dps.CharacterName.ToLower()).FirstOrDefaultAsync();
        return result;
    }

    public async Task<DPS> GetDPS(DPS dps)
    {
        var result = await _context.dpsCollection.Find<DPS>(_ => _.DPSId == dps.DPSId).FirstOrDefaultAsync();
        return result;
    }

    public async Task<List<DPS>> GetDPSByHigherILVL(DPS dps)
    {
        var result = await _context.dpsCollection.Find<DPS>(_ => _.ILevel >= dps.ILevel).ToListAsync();
        return result;
    }
}