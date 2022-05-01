namespace raiding.API.services;

public interface IRaidService
{
    Task<DPS> AddDPS(DPS newDPS);
    Task<Healer> AddHealer(Healer newHealer);
    Task<Raid> AddRaid(Raid newRaid);
    Task<Tank> AddTank(Tank newTank);
    Task DeleteRaid(string raidid);
    Task<List<DPS>> GetAllDPS();
    Task<List<Healer>> GetAllHealers();
    Task<List<Tank>> GetAllTanks();
    Task<DPS> GetDPS(DPS dps);
    Task<List<DPS>> GetDPSByHigherILVL(DPS dps);
    Task<DPS> GetDPSByUsername(DPS dps);
    Task<Healer> GetHealer(Healer healer);
    Task<List<Healer>> GetHealerByHigherILVL(Healer healer);
    Task<Healer> GetHealerByUsername(Healer healer);
    Task<Raid> GetRaid(Raid raid);
    Task<List<Raid>> GetRaids();
    Task<Tank> GetTank(Tank tank);
    Task<List<Tank>> GetTankByHigherILVL(Tank tank);
    Task<Tank> GetTankByUsername(Tank tank);
}

public class RaidService : IRaidService
{
    public readonly IDPSRepository _dpsRepository;
    public readonly IHealerRepository _healerRepository;
    public readonly ITankRepository _tankRepository;
    public readonly IRaidRepository _raidRepository;



    public RaidService(IDPSRepository dpsRepository, IHealerRepository healerRepository, ITankRepository tankRepository, IRaidRepository raidRepository)
    {
        _dpsRepository = dpsRepository;
        _healerRepository = healerRepository;
        _tankRepository = tankRepository;
        _raidRepository = raidRepository;
    }

    //Raid Repo
    public async Task<Raid> AddRaid(Raid newRaid)
    {
        await _raidRepository.AddRaid(newRaid);
        return newRaid;
    }


    public async Task DeleteRaid(string raidid)
    {
        await _raidRepository.DeleteRaid(raidid);
    }

    public async Task<List<Raid>> GetRaids() => await _raidRepository.GetAllRaids();


    public async Task<Raid> GetRaid(Raid raid)
    {
        return await _raidRepository.GetRaid(raid);
    }

    //DPS Repo
    public async Task<DPS> AddDPS(DPS newDPS)
    {
        await _dpsRepository.AddDPS(newDPS);
        return newDPS;
    }

    public async Task<List<DPS>> GetAllDPS() => await _dpsRepository.GetAllDPS();

    public async Task<DPS> GetDPSByUsername(DPS dps) => await _dpsRepository.GetDPSByUsername(dps);
    public async Task<List<DPS>> GetDPSByHigherILVL(DPS dps) => await _dpsRepository.GetDPSByHigherILVL(dps);
    public async Task<DPS> GetDPS(DPS dps) => await _dpsRepository.GetDPS(dps);



    //Healer Repo
    public async Task<Healer> AddHealer(Healer newHealer)
    {
        await _healerRepository.AddHealer(newHealer);
        return newHealer;
    }

    public async Task<List<Healer>> GetAllHealers() => await _healerRepository.GetAllHealers();

    public async Task<Healer> GetHealerByUsername(Healer healer) => await _healerRepository.GetHealerByUsername(healer);
    public async Task<List<Healer>> GetHealerByHigherILVL(Healer healer) => await _healerRepository.GetHealerByHigherILVL(healer);
    public async Task<Healer> GetHealer(Healer healer) => await _healerRepository.GetHealer(healer);


    //Tank Repo
    public async Task<Tank> AddTank(Tank newTank)
    {
        await _tankRepository.AddTank(newTank);
        return newTank;
    }

    public async Task<List<Tank>> GetAllTanks() => await _tankRepository.GetAllTanks();

    public async Task<Tank> GetTankByUsername(Tank tank) => await _tankRepository.GetTankByUsername(tank);
    public async Task<List<Tank>> GetTankByHigherILVL(Tank tank) => await _tankRepository.GetTankByHigherILVL(tank);
    public async Task<Tank> GetTank(Tank tank) => await _tankRepository.GetTank(tank);
}