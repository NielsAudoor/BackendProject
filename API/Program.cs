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
        return Results.Created($"/raid/{result.RaidId}", result);
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
        return Results.Created($"/dps/{result.DPSId}", result);
    }
    catch (System.Exception)
    {

        throw;
    }
});
app.MapGet("/dps/{dpsId}", async (IRaidService raidService, string dpsId) =>
{
    try
    {
        DPS dps = new DPS();
        dps.DPSId = dpsId;
        var result = await raidService.GetDPS(dps);
        return Results.Ok(result);
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
        return Results.Created($"/healer/{result.HealerId}", result);
    }
    catch (System.Exception)
    {
        throw;
    }
});

app.MapGet("/healer/{healerId}", async (IRaidService raidService, string healerId) =>
{
    try
    {
        Healer healer = new Healer();
        healer.HealerId = healerId;
        var result = await raidService.GetHealer(healer);
        return Results.Ok(result);
    }
    catch (System.Exception)
    {
        throw;
    }
});


app.MapDelete("/raid/{raidId}", async (IRaidService raidService, string raidId) =>
{
    try
    {
        await raidService.DeleteRaid(raidId);
    }
    catch (System.Exception)
    {
        throw;
    }
});

app.MapGet("/raid/{raidId}", async (IRaidService raidService, string raidId) =>
{
    try
    {
        Raid raid = new Raid();
        raid.RaidId = raidId;
        var result = await raidService.GetRaid(raid);
        return Results.Ok(result);
    }
    catch (System.Exception)
    {
        throw;
    }
});


app.MapGet("/tank", async (IRaidService raidService) =>
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

app.MapPost("/tank", async (IRaidService raidService, Tank tank) =>
{
    try
    {
        var result = await raidService.AddTank(tank);
        return Results.Created($"/tank/{result.TankId}", result);
    }
    catch (System.Exception)
    {
        throw;
    }
});
app.MapGet("/tank/{tankId}", async (IRaidService raidService, string tankId) =>
{
    try
    {
        Tank tank = new Tank();
        tank.TankId = tankId;
        var result = await raidService.GetTank(tank);
        return Results.Ok(result);
    }
    catch (System.Exception)
    {
        throw;
    }
});


app.Run("http://0.0.0.0:3000");
