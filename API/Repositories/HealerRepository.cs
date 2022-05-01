namespace raiding.API.repositories;

public interface IHealerRepository
{
    Task<Healer> AddHealer(Healer newHealer);
    Task<List<Healer>> GetAllHealers();
    Task<List<Healer>> GetHealerByHigherILVL(Healer healer);
    Task<Healer> GetHealerByUsername(Healer healer);
}

public class HealerRepository : IHealerRepository
{
    private readonly IMongoContext _context;
    public HealerRepository(IMongoContext context)
    {
        _context = context;
    }

    public async Task<Healer> AddHealer(Healer newHealer)
    {
        await _context.healerCollection.InsertOneAsync(newHealer);
        return newHealer;
    }
    public async Task<List<Healer>> GetAllHealers()
    {
        return await _context.healerCollection.Find(_ => true).ToListAsync();
    }

    public async Task<Healer> GetHealerByUsername(Healer healer)
    {
        var result = await _context.healerCollection.Find<Healer>(_ => _.CharacterName.ToLower() == healer.CharacterName.ToLower()).FirstOrDefaultAsync();
        return result;
    }

    public async Task<List<Healer>> GetHealerByHigherILVL(Healer healer)
    {
        var result = await _context.healerCollection.Find<Healer>(_ => _.ILevel >= healer.ILevel).ToListAsync();
        return result;
    }
}