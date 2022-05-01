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

app.MapGet("/", () => "Hello World!");


// app.MapGet("/events", async () =>
// {
// });

app.MapGet("/raids", async (IRaidService raidService) =>
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

app.MapPost("/raids", async (IRaidService raidService, Raid raid) =>
{
    try
    {
        DPS founddps = await raidService.GetDPSByUsername(raid.dps);
        Tank foundtank = await raidService.GetTankByUsername(raid.tank);
        Healer foundhealer = await raidService.GetHealerByUsername(raid.healer);
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


app.Run("http://0.0.0.0:3000");
