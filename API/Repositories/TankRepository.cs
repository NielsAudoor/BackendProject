namespace raiding.API.repositories;

public interface ITankRepository
{
    Task<Tank> AddTank(Tank newTank);
    Task<List<Tank>> GetAllTanks();
    Task<Tank> GetTank(Tank tank);
    Task<List<Tank>> GetTankByHighestArmour(Tank tank);
    Task<Tank> GetTankByUsername(Tank tank);
}

public class TankRepository : ITankRepository
{
    private readonly IMongoContext _context;
    public TankRepository(IMongoContext context)
    {
        _context = context;
    }

    public async Task<Tank> AddTank(Tank newTank)
    {
        await _context.tankCollection.InsertOneAsync(newTank);
        return newTank;
    }
    public async Task<List<Tank>> GetAllTanks()
    {
        return await _context.tankCollection.Find(_ => true).ToListAsync();
    }

    public async Task<Tank> GetTankByUsername(Tank tank)
    {
        var result = await _context.tankCollection.Find<Tank>(_ => _.CharacterName.ToLower() == tank.CharacterName.ToLower()).FirstOrDefaultAsync();
        return result;
    }
    public async Task<Tank> GetTank(Tank tank)
    {
        var result = await _context.tankCollection.Find<Tank>(_ => _.TankId == tank.TankId).FirstOrDefaultAsync();
        return result;
    }
    public async Task<List<Tank>> GetTankByHighestArmour(Tank tank)
    {
        var result = await _context.tankCollection.Find<Tank>(_ => _.armour >= tank.armour).ToListAsync();
        return result;
    }
}