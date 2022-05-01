var builder = WebApplication.CreateBuilder(args);
var mongoSettings = builder.Configuration.GetSection("MongoConnection");
builder.Services.Configure<DatabaseSettings>(mongoSettings);

builder.Services.AddTransient<IMongoContext, MongoContext>();
builder.Services.AddTransient<IRaidRepository, RaidRepository>();
builder.Services.AddTransient<ITankRepository, TankRepository>();
builder.Services.AddTransient<IHealerRepository, HealerRepository>();
builder.Services.AddTransient<IDPSRepository, DPSRepository>();
builder.Services.AddTransient<IRaidService, RaidService>();
var app = builder.Build();



app.MapGet("/raid", async (IRaidService raidService) =>
{
    try
    {
        var result = await raidService.GetRaids();
        return result;
    }
    catch (Exception ex)
    {
        Console.Write(ex);
        throw;
    }
});

app.MapPost("/raid", async (IRaidService raidService, Raid raid) =>
{
    try
    {
        DPS founddps = await raidService.GetDPS(raid.dps);
        Tank foundtank = await raidService.GetTank(raid.tank);
        Healer foundhealer = await raidService.GetHealer(raid.healer);
        raid.dps = founddps;
        raid.healer = foundhealer;
        raid.tank = foundtank;
        var result = await raidService.AddRaid(raid);
        return result;
    }
    catch (System.Exception)
    {

        throw;
    }
});

app.MapGet("/dps", async (IRaidService raidService) =>
{
    try
    {
        var result = await raidService.GetAllDPS();
        return result;
    }
    catch (System.Exception)
    {

        throw;
    }
});

app.MapPost("/dps", async (IRaidService raidService, DPS dps) =>
{
    try
    {
        var result = await raidService.AddDPS(dps);
        return result;
    }
    catch (System.Exception)
    {

        throw;
    }
});




app.MapGet("/healer", async (IRaidService raidService) =>
{
    try
    {
        var result = await raidService.GetAllHealers();
        return result;
    }
    catch (System.Exception)
    {

        throw;
    }
});

app.MapPost("/healer", async (IRaidService raidService, Healer healer) =>
{
    try
    {
        var result = await raidService.AddHealer(healer);
        return result;
    }
    catch (System.Exception)
    {
        throw;
    }
});


app.MapGet("/Tank", async (IRaidService raidService) =>
{
    try
    {
        var result = await raidService.GetAllTanks();
        return result;
    }
    catch (System.Exception)
    {

        throw;
    }
});

app.MapPost("/Tank", async (IRaidService raidService, Tank tank) =>
{
    try
    {
        var result = await raidService.AddTank(tank);
        return result;
    }
    catch (System.Exception)
    {
        throw;
    }
});


app.Run("http://0.0.0.0:3000");
