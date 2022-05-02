using Xunit;

namespace raiding.API.tests;

public class IntegrationtTest
{


    [Fact]
    public async void Should_return_Healers()
    {
        var application = Helper.CreateApi();
        var client = application.CreateClient();
        var results = await client.GetAsync("/healer");
        Console.Write(results);
        results.StatusCode.Should().Be(HttpStatusCode.OK);
        var healers = await results.Content.ReadFromJsonAsync<List<Healer>>();
        Assert.NotNull(healers);
        Assert.IsType<List<Healer>>(healers);

    }
    [Fact]
    public async Task Should_Post_Healer()
    {
        Healer healer = new Healer();
        healer.HealerId = "626a4a123e1270433723b9ff";
        var body = new Healer()
        {
            CharacterName = "Matsumoto",
            HealingPerSecond = 9000,
            ILevel = 250
        };

        var application = Helper.CreateApi();
        var client = application.CreateClient();
        var results = await client.PostAsJsonAsync("/healer", body);

        results.StatusCode.Should().Be(HttpStatusCode.Created);
        var healerResult = await results.Content.ReadFromJsonAsync<Healer>();
        Assert.NotNull(healerResult);
        Assert.IsType<Healer>(healerResult);

    }
}