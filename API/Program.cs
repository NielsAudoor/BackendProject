using Microsoft.OpenApi.Models;
var builder = WebApplication.CreateBuilder(args);
var mongoSettings = builder.Configuration.GetSection("MongoConnection");
builder.Services.Configure<DatabaseSettings>(mongoSettings);
var apiKeySettings = builder.Configuration.GetSection("AuthenticationSettings");

builder.Services.Configure<ApiKeySettings>(apiKeySettings);


builder.Services.AddMvc();
builder.Services.AddTransient<IMongoContext, MongoContext>();
builder.Services.AddTransient<IRaidRepository, RaidRepository>();
builder.Services.AddTransient<ITankRepository, TankRepository>();
builder.Services.AddTransient<IHealerRepository, HealerRepository>();
builder.Services.AddTransient<IDPSRepository, DPSRepository>();
builder.Services.AddTransient<IRaidService, RaidService>();
builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();

builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<DPSValidator>());
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<HealerValidator>());
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<RaidValidator>());
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<TankValidator>());


builder.Services
    .AddGraphQLServer()
    .AddQueryType<Queries>()
    .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true)
    .AddMutationType<Mutation>();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
  {
      config.SwaggerDoc("v1", new OpenApiInfo() { Title = "Raid API", Version = "v1" });
  });


builder.Services.AddAuthorization(options =>
{

});

builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["AuthenticationSettings:Issuer"],
            ValidAudience = builder.Configuration["AuthenticationSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(builder.Configuration["AuthenticationSettings:SecretString"]))
        };
    }
);

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.MapGraphQL();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Raid API");
});


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


app.MapPost("/raid", [Authorize] async (IRaidService raidService, Raid raid) =>
{
    try
    {
        DPS founddps = await raidService.GetDPS(raid.dps);
        Tank foundtank = await raidService.GetTank(raid.tank);
        Healer foundhealer = await raidService.GetHealer(raid.healer);
        raid.dps = founddps;
        raid.healer = foundhealer;
        raid.tank = foundtank;
        var validation = new RaidValidator();
        var valresult = validation.Validate(raid);
        if (valresult.Errors.Count() > 0 && valresult != null) return Results.BadRequest(valresult.Errors);
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
        var validation = new DPSValidator();
        var valresult = validation.Validate(dps);
        if (valresult.Errors.Count() > 0 && valresult != null) return Results.BadRequest(valresult.Errors);
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

app.MapGet("/dps/damage/{amount}", async (IRaidService raidService, int amount) =>
{
    try
    {
        DPS dps = new DPS();
        dps.DamagePerSecond = amount;
        var result = await raidService.GetDPSByHighestDamage(dps);
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
        return Results.Ok(result);
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
        var validation = new HealerValidator();
        var valresult = validation.Validate(healer);
        if (valresult.Errors.Count() > 0 && valresult != null) return Results.BadRequest(valresult.Errors);
        var result = await raidService.AddHealer(healer);
        return Results.Created($"/healer/{result.HealerId}", result);
    }
    catch (System.Exception)
    {
        throw;
    }
});

app.MapGet("/healer/healing/{amount}", async (IRaidService raidService, int amount) =>
{
    try
    {
        Healer healer = new Healer();
        healer.HealingPerSecond = amount;
        var result = await raidService.GetHealerByHealingAmount(healer);
        return Results.Ok(result);
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
        var validation = new TankValidator();
        var valresult = validation.Validate(tank);
        if (valresult.Errors.Count() > 0 && valresult != null) return Results.BadRequest(valresult.Errors);
        var result = await raidService.AddTank(tank);
        return Results.Created($"/tank/{result.TankId}", result);
    }
    catch (System.Exception)
    {
        throw;
    }
});

app.MapGet("/tank/armour/{amount}", async (IRaidService raidService, int amount) =>
{
    try
    {
        Tank tank = new Tank();
        tank.armour = amount;
        var result = await raidService.GetTankByHighestArmour(tank);
        return Results.Ok(result);
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


app.MapPost("/authenticate", (IAuthenticationService authenticationService, IOptions<ApiKeySettings> authSettings, AuthenticationRequestBody authenticationRequestBody) =>
{
    var user = authenticationService.ValidateUser(authenticationRequestBody.username, authenticationRequestBody.password);
    if (user == null)
    {
        return Results.Unauthorized();
    }
    String x = (authSettings.Value.SecretString!);
    var test = System.Text.Encoding.ASCII.GetBytes(x);
    var securityKey = new SymmetricSecurityKey(test);

    var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    var claimsForToken = new List<Claim>();
    claimsForToken.Add(new Claim("sub", "1"));
    claimsForToken.Add(new Claim("given_name", user.name));
    claimsForToken.Add(new Claim("city", user.city));

    var jwtSecurityToken = new JwtSecurityToken(
        authSettings.Value.Issuer,
        authSettings.Value.Audience,
        claimsForToken,
        DateTime.UtcNow,
        DateTime.UtcNow.AddHours(1),
        signingCredentials
    );

    var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

    return Results.Ok(tokenToReturn);
});


app.Run("http://0.0.0.0:3000");
// app.Run();
// public partial class Program { }