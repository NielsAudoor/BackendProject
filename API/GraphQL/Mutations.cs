namespace raiding.API.GraphQL.Mutation;

public class Mutation
{
    public async Task<AddRaidPayload> AddRaid([Service] IRaidService raidService, AddRaidInput input)
    {
        var raid = new Raid()
        {
            Name = input.name,
            Description = input.description,
            healer = input.healer,
            dps = input.dps,
            tank = input.tank
        };
        var result = await raidService.AddRaid(raid);
        return new AddRaidPayload(result);

    }

    public async Task<AddHealerPayload> AddHealer([Service] IRaidService raidService, AddHealerInput input)
    {
        var healer = new Healer()
        {
            CharacterName = input.CharacterName,
            ILevel = input.ILevel,
            HealingPerSecond = input.HealingPerSecond
        };
        var result = await raidService.AddHealer(healer);
        return new AddHealerPayload(result);

    }
    public async Task<AddDPSPayload> AddDPS([Service] IRaidService raidService, AddDPSInput input)
    {
        var dps = new DPS()
        {
            CharacterName = input.CharacterName,
            ILevel = input.ILevel,
            DamagePerSecond = input.DamagePerSecond
        };
        var result = await raidService.AddDPS(dps);
        return new AddDPSPayload(result);

    }
    public async Task<AddTankPayload> AddTank([Service] IRaidService raidService, AddTankInput input)
    {
        var tank = new Tank()
        {
            CharacterName = input.CharacterName,
            ILevel = input.ILevel,
            armour = input.armour
        };
        var result = await raidService.AddTank(tank);
        return new AddTankPayload(result);
    }
}