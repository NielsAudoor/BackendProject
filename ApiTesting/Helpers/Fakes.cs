namespace raiding.API.fakes;


public class FakeRaidRepository : IRaidRepository
{
    public Task<Raid> AddRaid(Raid newRaid)
    {
        throw new NotImplementedException();
    }

    public Task DeleteRaid(string raidid)
    {
        throw new NotImplementedException();
    }

    public Task<List<Raid>> GetAllRaids()
    {
        throw new NotImplementedException();
    }

    public Task<Raid> GetRaid(Raid raid)
    {
        Raid testValue = new Raid()
        {
            RaidId = "626edc4de266f3571dbf4f3e"
        };
        return Task.FromResult(testValue);
    }
}

public class FakeHealerRepository : IHealerRepository
{
    public static List<Healer> _healers = new List<Healer>();
    public Task<Healer> AddHealer(Healer newHealer)
    {
        _healers.Add(newHealer);
        return Task.FromResult(newHealer);
    }

    public Task<List<Healer>> GetAllHealers()
    {
        return Task.FromResult(_healers);
    }

    public Task<Healer> GetHealer(Healer healer)
    {
        Healer testValue = new Healer()
        {
            HealerId = "626edc4de266f3571dbf4f3e"
        };
        return Task.FromResult(testValue);
    }

    public Task<List<Healer>> GetHealerByHealingAmount(Healer healer)
    {
        throw new NotImplementedException();
    }

    public Task<Healer> GetHealerByUsername(Healer healer)
    {
        throw new NotImplementedException();
    }
}