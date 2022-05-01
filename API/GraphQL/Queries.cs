namespace raiding.API.GraphQL.queries;

public class Queries
{

    public async Task<List<Raid>> GetRaids([Service] IRaidService raid) => await raid.GetRaids();

    public async Task<List<Healer>> GetHealers([Service] IRaidService raid) => await raid.GetAllHealers();
    public async Task<List<DPS>> Getdps([Service] IRaidService raid) => await raid.GetAllDPS();
    public async Task<List<Tank>> GetTanks([Service] IRaidService raid) => await raid.GetAllTanks();

}